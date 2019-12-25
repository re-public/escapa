using Escapa.Core.Events;
using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Core.Controllers
{
    public sealed class GameController : MonoBehaviour, IGameController
    {
        public event GameEvent GameStarted;

        private IPlayer _player;
        private IDifficultyController _difficultyController;
        private ISocialController _socialController;
        private IScoreController _scoreController;

        private Level _currentLevel;
        private float? _idleTime;
        private float? _movingTime;

        private void Awake()
        {
            _player = GameObject.FindWithTag(Tags.Player).GetComponent<IPlayer>();
            _difficultyController = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
            _scoreController = GameObject.FindWithTag(Tags.ScoreController).GetComponent<IScoreController>();
            _socialController = GameObject.FindWithTag(Tags.SocialController).GetComponent<ISocialController>();
        }

        private void OnEnable()
        {
            _difficultyController.Changed += OnDifficultyChanged;

            _player.Died += OnPlayerDie;
            _player.Moved += OnPlayerMoved;
            _player.Pressed += OnPlayerPressed;
            _player.Stopped += OnPlayerStopped;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                _scoreController.StopCount(_currentLevel.Difficulty);

            if (_scoreController.CurrentTime > _scoreController.BlackHawkTime && _currentLevel.Difficulty == Difficulties.Insane)
                _socialController.CompleteAchievement(GooglePlayIds.achievement_black_hawk);

            if (_idleTime.HasValue && _scoreController.CurrentTime - _idleTime.Value > _scoreController.ZenTime)
                _socialController.CompleteAchievement(GooglePlayIds.achievement_zen);
            else if (_movingTime.HasValue && _scoreController.CurrentTime - _movingTime > _scoreController.JaggerTime)
                _socialController.CompleteAchievement(GooglePlayIds.achievement_moves_like_jagger);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                _socialController.CompleteAchievement(GooglePlayIds.achievement_panic_button);

                OnPlayerDie();
            }
        }

        private void OnDisable()
        {
            _difficultyController.Changed -= OnDifficultyChanged;

            _player.Died -= OnPlayerDie;
            _player.Moved -= OnPlayerMoved;
            _player.Pressed -= OnPlayerPressed;
            _player.Stopped -= OnPlayerStopped;
        }

        private void OnDifficultyChanged(object sender, DifficultyEventArgs e)
        {
            _currentLevel = e.Level;
        }

        private void OnPlayerDie()
        {
            _player.Died -= OnPlayerDie;
            _scoreController.StopCount(_currentLevel.Difficulty);
            _socialController.SendScore(_currentLevel.Difficulty, (long)_scoreController.LastTime * 1000);

            SceneManager.LoadScene((int)GameScenes.End);
        }

        private void OnPlayerMoved()
        {
            if (!_movingTime.HasValue)
            {
                _movingTime = _scoreController.CurrentTime;
                _idleTime = null;
            }
        }

        private void OnPlayerPressed()
        {
            _player.Pressed -= OnPlayerPressed;

            _scoreController.StartCount();
            GameStarted?.Invoke();
        }

        private void OnPlayerStopped()
        {
            if (!_idleTime.HasValue)
            {
                _idleTime = _scoreController.CurrentTime;
                _movingTime = null;
            }
        }
    }
}
