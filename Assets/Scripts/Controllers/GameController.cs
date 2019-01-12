using Escapa.Managers;
using Escapa.Units;
using Escapa.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class GameController : MonoBehaviour, ISceneController
    {
        public GameObject Enemy;

        public void PrepareScene()
        {
            for (var i = 0; i < _edges.childCount; i++)
            {
                var position = Vector2.zero;

                if (i == 0)
                    position.x = -Screen.width * Camera.main.orthographicSize / Screen.height;
                else if (i == 1)
                    position.y = Camera.main.orthographicSize;
                else if (i == 2)
                    position.x = Screen.width * Camera.main.orthographicSize / Screen.height;
                else if (i == 3)
                    position.y = -Camera.main.orthographicSize;

                _edges.GetChild(i).position = position;
            }

            for (var i = 0; i < DifficultyManager.Difficulty.Count; i++)
            {
                Vector2 position = Vector2.zero;

                if (i == 0)
                    position = new Vector2(-1.5f, 2.5f);
                else if (i == 1)
                    position = new Vector2(1.5f, 2.5f);
                else if (i == 2)
                    position = new Vector2(1.5f, -2.5f);
                else if (i == 3)
                    position = new Vector2(-1.5f, -2.5f);

                _enemies.Add((GameObject)Instantiate(Enemy, position, Quaternion.identity));
                _enemies[i].GetComponent<IColoredUnit>().Color = StyleManager.CurrentTheme.Enemy;
            }
            _player.GetComponent<IColoredUnit>().Color = StyleManager.CurrentTheme.Player;
        }

        private Transform _edges;
        private List<GameObject> _enemies;
        private GameObject _player;
        private ISystemController _systemController;

        private void Awake()
        {
            _edges = GameObject.FindWithTag(Tags.Edges).transform;
            _enemies = new List<GameObject>(4);
            _player = GameObject.FindWithTag(Tags.Player);
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();

            _player.GetComponent<IPlayer>().Die += OnPlayerDie;
            _player.GetComponent<IPlayer>().MousePressed += OnPlayerPressed;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                _systemController.GoToScene(GameScenes.Menu);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                OnPlayerDie();
        }

        private void OnPlayerDie()
        {
            ScoreManager.Finish();
            AnalyticsManager.SendGameOverEvent();

            _systemController.GoToScene(GameScenes.End);
        }

        private void OnPlayerPressed()
        {
            _player.GetComponent<IPlayer>().MousePressed -= OnPlayerPressed;

            foreach (var enemy in _enemies)
                enemy.GetComponent<IEnemy>().AddForce();

            ScoreManager.Start();
            AnalyticsManager.SendGameStartEvent();
        }
    }
}
