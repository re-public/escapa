using Escapa.Managers;
using Escapa.Units;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Controllers
{
    public sealed class GameController : MonoBehaviour
    {
        private IPlayer _player;
        private ISocialController _socialController;
        private bool _isGameStarted;

        private void Awake()
        {
            _player = GameObject.FindWithTag(Tags.Player).GetComponent<IPlayer>();
            _socialController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISocialController>();
        }

        private void OnEnable()
        {
            _player.Die += OnPlayerDie;
            _player.MousePressed += OnPlayerPressed;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                ScoreManager.StopCount();

                SceneManager.LoadSceneAsync((int) GameScenes.Menu, LoadSceneMode.Single);
            }

            if (ScoreManager.CurrentRecord > 18f && DifficultyManager.Level == Difficulties.Insane)
                _socialController.CompleteAchievement(Achievements.BlackHawk);

            if (_player.IdleTime > 5f)
                _socialController.CompleteAchievement(Achievements.Zen);
            else if (_player.MovingTime > 10f)
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

        private void OnPlayerDie()
        {
            if (!_isGameStarted) return;

            ScoreManager.StopCount();
            _socialController.SendScore();

            _isGameStarted = false;

            SceneManager.LoadSceneAsync((int) GameScenes.End, LoadSceneMode.Single);
        }

        private void OnPlayerPressed()
        {
            _player.MousePressed -= OnPlayerPressed;

            _isGameStarted = true;

            ScoreManager.StartCount();
        }
    }
}
