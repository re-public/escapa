using Escapa.Utility;
using System;
using UnityEngine;

namespace Escapa.Managers
{
    public static class DifficultyManager
    {
        [Serializable]
        private class DifficultyRules
        {
            public LevelRules[] Levels;
        }

        [Serializable]
        public struct LevelRules
        {
            public int Count;
            public float MinSpeed;
            public float MaxSpeed;
        }

        private static DifficultyRules _difficulty = null;
        public static LevelRules Difficulty
        {
            get
            {
                if (_difficulty == null) LoadDifficulty();
                return _difficulty.Levels[Level];
            }
        }

        private static int? _level = null;
        public static int Level
        {
            get => _level ?? (_level = PlayerPrefs.GetInt(PlayerPrefKeys.Level, 0)).Value;
            private set => _level = value;
        }
        public static int AddLevel() => Level == 3 ? Level = 0 : Level++;
        public static void SaveLevel() => PlayerPrefs.SetInt(PlayerPrefKeys.Level, _level.Value);

        private static void LoadDifficulty()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Difficulty).text;
            _difficulty = JsonUtility.FromJson<DifficultyRules>(json);
        }
    }
}
