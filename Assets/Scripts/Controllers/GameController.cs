using Escapa.Managers;
using Escapa.Units;
using Escapa.Utility;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class GameController : MonoBehaviour, ISceneController
    {
        public GameObject Enemy;

        private char[] currentRecordBuffer = { '0', '0', '0', '0', '.', '0', '0', '\0' };

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
                else if (i == 4)
                    position = new Vector2(0f, 3f);
                else if (i == 5)
                    position = new Vector2(0f, -3f);

                _enemies.Add(Instantiate(Enemy, position, Quaternion.identity));
            }

            _timeText.text = string.Empty;
        }

        public void StyleScene()
        {
            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;
            _player.Color = StyleManager.CurrentTheme.Player;
            _timeText.color = StyleManager.CurrentTheme.TextAlfa;
            for (var i = 0; i < DifficultyManager.Difficulty.Count; i++)
                _enemies[i].GetComponent<IEnemy>().Color = StyleManager.CurrentTheme.Enemy;
        }

        private Transform _edges;
        private List<GameObject> _enemies;
        private IPlayer _player;
        private ISystemController _systemController;
        private TextMeshPro _timeText;
        private bool _isGameStarted;

        private void Awake()
        {
            _edges = GameObject.FindWithTag(Tags.Edges).transform;
            _enemies = new List<GameObject>(4);
            _player = GameObject.FindWithTag(Tags.Player).GetComponent<IPlayer>();
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
            _timeText = GameObject.FindWithTag(Tags.TimeText).GetComponent<TextMeshPro>();

            _player.Die += OnPlayerDie;
            _player.MousePressed += OnPlayerPressed;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                ScoreManager.StopCount();

                _systemController.GoToScene(GameScenes.Menu);
            }

            UpdateCurrentRecordBuffer(ScoreManager.CurrentRecord);

            _timeText.SetText(new string(currentRecordBuffer));

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
            AnalyticsManager.SendGameOverEvent();
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
            AnalyticsManager.SendGameStartEvent();

            if (DifficultyManager.CurrentLevelIsCustom)
            {
                if (DifficultyManager.CurrentLevelIsMin)
                    SocialManager.CompleteAchievement(Achievements.TakeItEasy);
                else if (DifficultyManager.CurrentLevelIsMax)
                    SocialManager.CompleteAchievement(Achievements.Hothead);
            }
        }

        private void UpdateCurrentRecordBuffer(float value)
        {
            int firstPart = (int)value;
            int secondPart = (int)((value - firstPart) * 100);
            if (secondPart < 10)
                secondPart *= 10;

            int i = currentRecordBuffer.Length - 1;

            currentRecordBuffer[i] = '\0';
            --i;

            if (secondPart == 0)
            {
                currentRecordBuffer[i] = '0';
                currentRecordBuffer[i - 1] = '0';
                currentRecordBuffer[i - 2] = '.';
                i -= 3;
            }
            else
            {
                do
                {
                    currentRecordBuffer[i] = (char)(secondPart % 10 + '0');
                    secondPart /= 10;
                    --i;
                }
                while (secondPart > 0);

                currentRecordBuffer[i] = '.';
                --i;
            }

            do
            {
                currentRecordBuffer[i] = (char)(firstPart % 10 + '0');
                firstPart /= 10;
                --i;
            }
            while (firstPart > 0);

            for (int j = 0; j < currentRecordBuffer.Length - i - 1; ++j)
                currentRecordBuffer[j] = currentRecordBuffer[i + j + 1];
        }
    }
}
