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
        
        private IPlayer _player;

        private float? _idleTime;
        private float? _movingTime;

        private void Awake() => _player = GameObject.FindWithTag(Tags.Player).GetComponent<IPlayer>();

        private void OnEnable()
        {
            _player.Died += OnPlayerDie;
            _player.Moved += OnPlayerMoved;
            _player.Pressed += OnPlayerPressed;
            _player.Stopped += OnPlayerStopped;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                ScoreManager.StopCount();

            if (ScoreManager.CurrentTime > 18f && DifficultyManager.Current.difficulty == Difficulties.Insane)
                SocialManager.CompleteAchievement(GooglePlayIds.achievement_black_hawk);

            if (_idleTime.HasValue && ScoreManager.CurrentTime - _idleTime.Value > 5f)
                SocialManager.CompleteAchievement(GooglePlayIds.achievement_zen);
            else if (_movingTime.HasValue && ScoreManager.CurrentTime - _movingTime > 10f)
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
            _player.Died -= OnPlayerDie;
            _player.Moved -= OnPlayerMoved;
            _player.Pressed -= OnPlayerPressed;
            _player.Stopped -= OnPlayerStopped;
        }

        private void OnPlayerDie()
        {
            ScoreManager.StopCount();
            SocialManager.SendScore();

            SceneManager.LoadSceneAsync((int) GameScenes.End);
        }

        private void OnPlayerMoved()
        {
            if (!_movingTime.HasValue)
            {
                _movingTime = ScoreManager.CurrentTime;
                _idleTime = null;
            }
        }

        private void OnPlayerPressed()
        {
            _player.Pressed -= OnPlayerPressed;

            ScoreManager.StartCount();
            GameStarted?.Invoke();
        }

        private void OnPlayerStopped()
        {
            if (!_idleTime.HasValue)
            {
                _idleTime = ScoreManager.CurrentTime;
                _movingTime = null;
            }
        }
    }
}
