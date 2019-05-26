using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class ScoreManager
    {
        private static float _startTime;
        private static readonly float[] _highScores = Load();

        public static float CurrentTime => _startTime > float.Epsilon ? Time.realtimeSinceStartup - _startTime : 0f;

        public static float LastTime { get; private set; }

        public static bool IsHighScore { get; private set; }

        public static void StartCount()
        {
            IsHighScore = false;
            _startTime = Time.realtimeSinceStartup;
        }

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

        public static float CurrentHigh
        {
            get => _highScores[(int)DifficultyManager.Current.difficulty];
            private set => _highScores[(int)DifficultyManager.Current.difficulty] = value;
        }

        public static void Save()
        {
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score0, _highScores[0]);
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score1, _highScores[1]);
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score2, _highScores[2]);
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score3, _highScores[3]);
        }

        private static float[] Load()
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
