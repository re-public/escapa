using Escapa.Core.Events;
using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Core.Controllers
{
    public sealed class GameController : MonoBehaviour, IGameController
    {
        public event GameEvent GameStarted;

        private IPlayer player;
        private IDifficultyController _difficulty;
        private IScoreController _score;

        private float? idleTime;
        private float? movingTime;

        private void Awake()
        {
            player = GameObject.FindWithTag(Tags.Player).GetComponent<IPlayer>();
            _difficulty = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
            _score = GameObject.FindWithTag(Tags.ScoreController).GetComponent<IScoreController>();
        }

        private void OnEnable()
        {
            player.Died += OnPlayerDie;
            player.Moved += OnPlayerMoved;
            player.Pressed += OnPlayerPressed;
            player.Stopped += OnPlayerStopped;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                _score.StopCount(_difficulty.Current.Difficulty);

            if (_score.CurrentTime > _score.BlackHawkTime && _difficulty.Current.Difficulty == Difficulties.Insane)
                SocialManager.CompleteAchievement(GooglePlayIds.achievement_black_hawk);

            if (idleTime.HasValue && _score.CurrentTime - idleTime.Value > _score.ZenTime)
                SocialManager.CompleteAchievement(GooglePlayIds.achievement_zen);
            else if (movingTime.HasValue && _score.CurrentTime - movingTime > _score.JaggerTime)
                SocialManager.CompleteAchievement(GooglePlayIds.achievement_moves_like_jagger);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SocialManager.CompleteAchievement(GooglePlayIds.achievement_panic_button);

                OnPlayerDie();
            }
        }

        private void OnDisable()
        {
            player.Died -= OnPlayerDie;
            player.Moved -= OnPlayerMoved;
            player.Pressed -= OnPlayerPressed;
            player.Stopped -= OnPlayerStopped;
        }

        private void OnPlayerDie()
        {
            player.Died -= OnPlayerDie;
            _score.StopCount(_difficulty.Current.Difficulty);
            SocialManager.SendScore(_difficulty.Current.Difficulty, (long)_score.LastTime * 1000);

            SceneManager.LoadScene((int)GameScenes.End);
        }

        private void OnPlayerMoved()
        {
            if (!movingTime.HasValue)
            {
                movingTime = _score.CurrentTime;
                idleTime = null;
            }
        }

        private void OnPlayerPressed()
        {
            player.Pressed -= OnPlayerPressed;

            _score.StartCount();
            GameStarted?.Invoke();
        }

        private void OnPlayerStopped()
        {
            if (!idleTime.HasValue)
            {
                idleTime = _score.CurrentTime;
                movingTime = null;
            }
        }
    }
}
