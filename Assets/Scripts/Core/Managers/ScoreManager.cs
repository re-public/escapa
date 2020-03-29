using Escapa.Assets.Scripts.Utility;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class ScoreManager
    {
        public static float CurrentTime => startTime > float.Epsilon ? Time.realtimeSinceStartup - startTime : 0f;

        public static float LastTime { get; private set; }

        public static bool IsHighScore { get; private set; }

        /// <summary>
        /// The time after which we give the achievement "Black hawk"
        /// </summary>
        public static float BlackHawkTime => times.BlackHawk;

        /// <summary>
        /// The time after which we give the achievement "Zen"
        /// </summary>
        public static float ZenTime => times.Zen;

        /// <summary>
        /// The time after which we give the achievement "Moves Like Jagger"
        /// </summary>
        public static float JaggerTime => times.Jagger;

        public static void StartCount()
        {
            IsHighScore = false;
            startTime = Time.realtimeSinceStartup;
        }

        public static void StopCount()
        {
            LastTime = CurrentTime;
            startTime = 0f;

            if (LastTime > CurrentHigh)
            {
                IsHighScore = true;
                CurrentHigh = LastTime;
            }
        }

        public static float CurrentHigh
        {
            get => highScores[(int)DifficultyManager.Current.difficulty];
            private set => highScores[(int)DifficultyManager.Current.difficulty] = value;
        }

        public static void Save()
        {
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score0, highScores[0]);
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score1, highScores[1]);
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score2, highScores[2]);
            PlayerPrefs.SetFloat(PlayerPrefKeys.Score3, highScores[3]);
        }

        private static float startTime;
        private static readonly float[] highScores = Load();
        private static readonly TimeConfig times = LoadConfig();

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

        private static TimeConfig LoadConfig()
        {
            var json = Resources.Load<TextAsset>($"{ResourceKeys.Achievements}").text;
            return JsonUtility.FromJson<AchievementsConfig>(json).Time;
        }
    }
}
