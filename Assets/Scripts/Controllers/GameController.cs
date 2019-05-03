using Escapa.Managers;
using Escapa.Units;
using Escapa.Utility;
using System.Collections.Generic;
using Escapa.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Controllers
{
    public sealed class GameController : MonoBehaviour
    {
        public GameObject enemy;

        private IMainCamera _camera;
        private Transform _edges;
        private List<GameObject> _enemies;
        private IPlayer _player;
        private ISocialController _socialController;
        private bool _isGameStarted;

        private void Awake()
        {
            _camera = GameObject.FindWithTag(Tags.MainCamera).GetComponent<IMainCamera>();
            _edges = GameObject.FindWithTag(Tags.Edges).transform;
            _enemies = new List<GameObject>(4);
            _player = GameObject.FindWithTag(Tags.Player).GetComponent<IPlayer>();
            _socialController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISocialController>();
        }

        private void OnEnable()
        {
            _player.Die += OnPlayerDie;
            _player.MousePressed += OnPlayerPressed;
        }

        private void Start()
        {
            for (var i = 0; i < _edges.childCount; i++)
            {
                var position = Vector2.zero;

                if (i == 0)
                    position.x = -Screen.width * _camera.UnitsPerPixel / 2f;
                else if (i == 1)
                    position.y = Screen.height * _camera.UnitsPerPixel / 2f;
                else if (i == 2)
                    position.x = Screen.width * _camera.UnitsPerPixel / 2f;
                else if (i == 3)
                    position.y = -Screen.height * _camera.UnitsPerPixel / 2f;

                _edges.GetChild(i).position = position;
            }

            for (var i = 0; i < DifficultyManager.Difficulty.Count; i++)
            {
                var position = Vector2.zero;

                if (i == 0)
                    position = new Vector2(-1.5f, 2.5f);
                else if (i == 1)
                    position = new Vector2(1.5f, 2.5f);
                else if (i == 2)
                    position = new Vector2(1.5f, -2.5f);
                else if (i == 3)
                    position = new Vector2(-1.5f, -2.5f);
                else if (i == 4)
                    position = new Vector2(0f, 3f);
                else if (i == 5)
                    position = new Vector2(0f, -3f);

                _enemies.Add(Instantiate(enemy, position, Quaternion.identity));
            }

            //Style
            _player.Color = StyleManager.CurrentTheme.Player;
            for (var i = 0; i < DifficultyManager.Difficulty.Count; i++)
                _enemies[i].GetComponent<IEnemy>().Color = StyleManager.CurrentTheme.Enemy;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                ScoreManager.StopCount();

                SceneManager.LoadSceneAsync((int) GameScenes.Menu, LoadSceneMode.Single);
            }

            if (ScoreManager.CurrentRecord > 18f && DifficultyManager.Level == 3)
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

            foreach (var enemyObj in _enemies)
                enemyObj.GetComponent<IEnemy>().AddForce();

            _isGameStarted = true;

            ScoreManager.StartCount();

            if (DifficultyManager.CurrentLevelIsCustom)
            {
                if (DifficultyManager.CurrentLevelIsMin)
                    _socialController.CompleteAchievement(Achievements.TakeItEasy);
                else if (DifficultyManager.CurrentLevelIsMax)
                    _socialController.CompleteAchievement(Achievements.Hothead);
            }
        }
    }
}
