using Escapa.Events;
using Escapa.Managers;
using Escapa.Units;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class GameController : MonoBehaviour, IGameController
    {
        public event GameEvent GameInitialized;
        public event GameEvent GameStarted;
        public event GameEvent GameEnded;
        
        private IPlayer _player;
        private ISocialController _socialController;
        private ISystemController _systemController;

        private float? _idleTime;
        private float? _movingTime;

        private void Awake()
        {
            _socialController = GetComponent<ISocialController>();
            _systemController = GetComponent<ISystemController>();
        }

        private void OnEnable()
        {
            _systemController.SceneLoaded += OnSceneLoaded;
            _systemController.SceneUnloaded += OnSceneUnloaded;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                ScoreManager.StopCount();

                _systemController.GoToScene(GameScenes.Menu);
            }

            if (ScoreManager.CurrentRecord > 18f && DifficultyManager.Level == Difficulties.Insane)
                _socialController.CompleteAchievement(Achievements.BlackHawk);

            if (_idleTime.HasValue && ScoreManager.CurrentRecord - _idleTime.Value > 5f)
                _socialController.CompleteAchievement(Achievements.Zen);
            else if (_movingTime.HasValue && ScoreManager.CurrentRecord - _movingTime > 10f)
                _socialController.CompleteAchievement(Achievements.MovesLikeJagger);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                _socialController.CompleteAchievement(Achievements.PanicButton);

                OnPlayerDie();
            }
        }

        private void OnDisable()
        {
            _systemController.SceneLoaded -= OnSceneLoaded;
            _systemController.SceneUnloaded -= OnSceneUnloaded;
        }

        private void OnPlayerDie()
        {
            ScoreManager.StopCount();
            _socialController.SendScore();
            
            _systemController.GoToScene(GameScenes.End);
        }

        private void OnPlayerMoved()
        {
            if (!_movingTime.HasValue)
            {
                _movingTime = ScoreManager.CurrentRecord;
                _idleTime = null;
            }
        }

        private void OnPlayerPressed()
        {
            _player.Pressed -= OnPlayerPressed;

            ScoreManager.StartCount();
            GameStarted?.Invoke(new GameEventArgs(ScoreManager.CurrentRecord, DifficultyManager.Difficulty));
        }

        private void OnPlayerStopped()
        {
            if (!_idleTime.HasValue)
            {
                _idleTime = ScoreManager.CurrentRecord;
                _movingTime = null;
            }
        }

        private void OnSceneLoaded(SystemEventArgs e)
        {
            if (e.Scene == GameScenes.Game)
            {
                _player = GameObject.FindWithTag(Tags.Player).GetComponent<IPlayer>();
                _player.Died += OnPlayerDie;
                _player.Moved += OnPlayerMoved;
                _player.Pressed += OnPlayerPressed;
                _player.Stopped += OnPlayerStopped;
                
                GameInitialized?.Invoke(new GameEventArgs(ScoreManager.CurrentRecord, DifficultyManager.Difficulty));
            }
            else if (e.Scene == GameScenes.End)
            {
                GameEnded?.Invoke(new GameEventArgs(ScoreManager.LastTime, DifficultyManager.Difficulty));
            }
        }

        private void OnSceneUnloaded(SystemEventArgs e)
        {
            if (e.Scene == GameScenes.Game)
            {
                _player.Died -= OnPlayerDie;
                _player.Moved -= OnPlayerMoved;
                _player.Pressed -= OnPlayerPressed;
                _player.Stopped -= OnPlayerStopped;
            }
        }
    }
}
