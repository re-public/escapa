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

        private float? idleTime;
        private float? movingTime;

        private void Awake() => player = GameObject.FindWithTag(Tags.Player).GetComponent<IPlayer>();

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
                ScoreManager.StopCount();

            if (ScoreManager.CurrentTime > ScoreManager.BlackHawkTime && DifficultyManager.Current.difficulty == Difficulties.Insane)
                SocialManager.CompleteAchievement(GooglePlayIds.achievement_black_hawk);

            if (idleTime.HasValue && ScoreManager.CurrentTime - idleTime.Value > ScoreManager.ZenTime)
                SocialManager.CompleteAchievement(GooglePlayIds.achievement_zen);
            else if (movingTime.HasValue && ScoreManager.CurrentTime - movingTime > ScoreManager.JaggerTime)
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
            ScoreManager.StopCount();
            SocialManager.SendScore();

            SceneManager.LoadScene((int)GameScenes.End);
        }

        private void OnPlayerMoved()
        {
            if (!movingTime.HasValue)
            {
                movingTime = ScoreManager.CurrentTime;
                idleTime = null;
            }
        }

        private void OnPlayerPressed()
        {
            player.Pressed -= OnPlayerPressed;

            ScoreManager.StartCount();
            GameStarted?.Invoke();
        }

        private void OnPlayerStopped()
        {
            if (!idleTime.HasValue)
            {
                idleTime = ScoreManager.CurrentTime;
                movingTime = null;
            }
        }
    }
}
