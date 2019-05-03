using Escapa.Managers;
using Escapa.Units;
using Escapa.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class GameController : MonoBehaviour
    {
        public GameObject Enemy;

        private Transform _edges;
        private List<GameObject> _enemies;
        private IPlayer _player;
        private ISystemController _systemController;
        private bool _isGameStarted;

        private void Awake()
        {
            _edges = GameObject.FindWithTag(Tags.Edges).transform;
            _enemies = new List<GameObject>(4);
            _player = GameObject.FindWithTag(Tags.Player).GetComponent<IPlayer>();
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();

            _player.Die += OnPlayerDie;
            _player.MousePressed += OnPlayerPressed;
        }

        private void Start()
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
                else if (i == 4)
                    position = new Vector2(0f, 3f);
                else if (i == 5)
                    position = new Vector2(0f, -3f);

                _enemies.Add((GameObject)Instantiate(Enemy, position, Quaternion.identity));
            }

            //Style
            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;
            _player.Color = StyleManager.CurrentTheme.Player;
            for (var i = 0; i < DifficultyManager.Difficulty.Count; i++)
                _enemies[i].GetComponent<IEnemy>().Color = StyleManager.CurrentTheme.Enemy;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                ScoreManager.StopCount();

                _systemController.GoToScene(GameScenes.Menu);
            }

            if (ScoreManager.CurrentRecord > 18f && DifficultyManager.Level == 3)
                SocialManager.CompleteAchievement(Achievements.BlackHawk);

            if (_player.IdleTime > 5f)
                SocialManager.CompleteAchievement(Achievements.Zen);
            else if (_player.MovingTime > 10f)
                SocialManager.CompleteAchievement(Achievements.MovesLikeJagger);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SocialManager.CompleteAchievement(Achievements.PanicButton);

                OnPlayerDie();
            }
        }

        private void OnPlayerDie()
        {
            if (!_isGameStarted) return;

            ScoreManager.StopCount();
            SocialManager.SendScore();

            _isGameStarted = false;

            _systemController.GoToScene(GameScenes.End);
        }

        private void OnPlayerPressed()
        {
            _player.MousePressed -= OnPlayerPressed;

            foreach (var enemy in _enemies)
                enemy.GetComponent<IEnemy>().AddForce();

            _isGameStarted = true;

            ScoreManager.StartCount();

            if (DifficultyManager.CurrentLevelIsCustom)
            {
                if (DifficultyManager.CurrentLevelIsMin)
                    SocialManager.CompleteAchievement(Achievements.TakeItEasy);
                else if (DifficultyManager.CurrentLevelIsMax)
                    SocialManager.CompleteAchievement(Achievements.Hothead);
            }
        }
    }
}
