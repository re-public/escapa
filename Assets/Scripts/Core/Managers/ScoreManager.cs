using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    /// <summary>
    /// Manager for controlling scores.
    /// </summary>
    public static class ScoreManager
    {
        private static float _startTime;
        private static float[] _highScores;

        /// <summary>
        /// Current game time.
        /// </summary>
        public static float CurrentTime => _startTime > float.Epsilon ? Time.realtimeSinceStartup - _startTime : 0f;

        /// <summary>
        /// Last game time.
        /// </summary>
        public static float LastTime { get; private set; }

        /// <summary>
        /// Is current time is new high score.
        /// </summary>
        public static bool IsHighScore { get; private set; }

        /// <summary>
        /// Start time counter.
        /// </summary>
        public static void StartCount()
        {
            IsHighScore = false;
            _startTime = Time.realtimeSinceStartup;
        }

        /// <summary>
        /// Stop time counter.
        /// </summary>
        public static void StopCount()
        {
            LastTime = CurrentTime;
            _startTime = 0f;

            if (LastTime > CurrentHigh)
            {
                IsHighScore = true;
                CurrentHigh = LastTime;
            }
        }

        /// <summary>
        /// Current high score.
        /// </summary>
        public static float CurrentHigh
        {
            get
            {
                if (_highScores == null)
                {
                    _highScores = LoadScores();
                }

                return _highScores[(int) DifficultyManager.Current.difficulty];
            }
            private set
            {
                if (_highScores == null)
                {
                    _highScores = LoadScores();
                }

                _highScores[(int) DifficultyManager.Current.difficulty] = value;
            }
        }

        /// <summary>
        /// Save all records in memory.
        /// </summary>
        public static void SaveScores()
        {
            if(_highScores == null) return;
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score0, _highScores[0]);
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score1, _highScores[1]);
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score2, _highScores[2]);
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score3, _highScores[3]);
        }
        
        private static float[] LoadScores()
        {
            return new []
            {
                PlayerPrefs.GetFloat(PlayerPrefKeys.Score0, 0f),
                PlayerPrefs.GetFloat(PlayerPrefKeys.Score1, 0f),
                PlayerPrefs.GetFloat(PlayerPrefKeys.Score2, 0f),
                PlayerPrefs.GetFloat(PlayerPrefKeys.Score3, 0f)
            };
        }
    }
}
