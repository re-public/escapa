﻿using Escapa.Core.Events;
using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Core.Controllers
{
    public sealed class GameController : MonoBehaviour, IGameController
    {
        public event GameEvent GameStarted;

        private IPlayer player;
        private IDifficultyController _difficultyController;
        private ISocialController _social;
        private IScoreController _score;

        private Level _currentLevel;
        private float? idleTime;
        private float? movingTime;

        private void Awake()
        {
            player = GameObject.FindWithTag(Tags.Player).GetComponent<IPlayer>();
            _difficultyController = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
            _score = GameObject.FindWithTag(Tags.ScoreController).GetComponent<IScoreController>();
            _social = GameObject.FindWithTag(Tags.SocialController).GetComponent<ISocialController>();
        }

        private void OnEnable()
        {
            _difficultyController.Changed += OnDifficultyChanged;

            player.Died += OnPlayerDie;
            player.Moved += OnPlayerMoved;
            player.Pressed += OnPlayerPressed;
            player.Stopped += OnPlayerStopped;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                _score.StopCount(_currentLevel.Difficulty);

            if (_score.CurrentTime > _score.BlackHawkTime && _currentLevel.Difficulty == Difficulties.Insane)
                _social.CompleteAchievement(GooglePlayIds.achievement_black_hawk);

            if (idleTime.HasValue && _score.CurrentTime - idleTime.Value > _score.ZenTime)
                _social.CompleteAchievement(GooglePlayIds.achievement_zen);
            else if (movingTime.HasValue && _score.CurrentTime - movingTime > _score.JaggerTime)
                _social.CompleteAchievement(GooglePlayIds.achievement_moves_like_jagger);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                _social.CompleteAchievement(GooglePlayIds.achievement_panic_button);

                OnPlayerDie();
            }
        }

        private void OnDisable()
        {
            _difficultyController.Changed -= OnDifficultyChanged;

            player.Died -= OnPlayerDie;
            player.Moved -= OnPlayerMoved;
            player.Pressed -= OnPlayerPressed;
            player.Stopped -= OnPlayerStopped;
        }

        private void OnDifficultyChanged(object sender, DifficultyEventArgs e)
        {
            _currentLevel = e.Level;
        }

        private void OnPlayerDie()
        {
            player.Died -= OnPlayerDie;
            _score.StopCount(_currentLevel.Difficulty);
            _social.SendScore(_currentLevel.Difficulty, (long)_score.LastTime * 1000);

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
