using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    public class ScoreController : MonoBehaviour, IScoreController
    {
        public float CurrentTime => _startTime > float.Epsilon ? Time.realtimeSinceStartup - _startTime : 0f;

        public float LastTime { get; private set; }

        public bool IsHighScore { get; private set; }

        /// <summary>
        /// The time after which we give the achievement "Black hawk"
        /// </summary>
        public float BlackHawkTime => Achievements.BlackHawk;

        /// <summary>
        /// The time after which we give the achievement "Zen"
        /// </summary>
        public float ZenTime => Achievements.Zen;

        /// <summary>
        /// The time after which we give the achievement "Moves Like Jagger"
        /// </summary>
        public float JaggerTime => Achievements.Jagger;

        public void StartCount()
        {
            IsHighScore = false;
            _startTime = Time.realtimeSinceStartup;
        }

        public void StopCount(Difficulties difficulty)
        {
            LastTime = CurrentTime;
            _startTime = 0f;

            if (LastTime > GetHigh(difficulty))
            {
                IsHighScore = true;
                SetHigh(difficulty, LastTime);
            }
        }

        public float GetHigh(Difficulties difficulty) => _highScores[(int)difficulty];

        public void Save()
        {
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score0, _highScores[0]);
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score1, _highScores[1]);
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score2, _highScores[2]);
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score3, _highScores[3]);
        }

        [SerializeField]
        private AchievementsConfig Achievements;

        private float _startTime;
        private float[] _highScores;

        private void Awake() => _highScores = Load();

        // If user wants to quit using Task Manager.
        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                Save();
            }
        }

        // If user wants to quit the usual way.
        private void OnApplicationQuit() => Save();

        private float SetHigh(Difficulties difficulty, float score) => _highScores[(int)difficulty] = score;

        private float[] Load()
        {
            return new[]
            {
                PlayerPrefs.GetFloat(PlayerPrefKeys.Score0, 0f),
                PlayerPrefs.GetFloat(PlayerPrefKeys.Score1, 0f),
                PlayerPrefs.GetFloat(PlayerPrefKeys.Score2, 0f),
                PlayerPrefs.GetFloat(PlayerPrefKeys.Score3, 0f)
            };
        }
    }
}
