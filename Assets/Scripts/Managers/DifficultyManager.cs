using Escapa.Utility;
using UnityEngine;

namespace Escapa.Managers
{
    public static class DifficultyManager
    {
        public const int DifficultiesCount = 5;

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
        public static int AddLevel() => Level == (DifficultiesCount - 1) ? Level = 0 : Level++;
        public static void SaveLevel() => PlayerPrefs.SetInt(PlayerPrefKeys.Level, _level.Value);

        private static void LoadDifficulty()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Difficulty).text;
            _difficulty = JsonUtility.FromJson<DifficultyRules>(json);
            _difficulty.Levels[DifficultiesCount - 1].Count = PlayerPrefs.GetInt(PlayerPrefKeys.EnemiesCount, 0);
            _difficulty.Levels[DifficultiesCount - 1].MaxSpeed = PlayerPrefs.GetFloat(PlayerPrefKeys.MaxSpeed, 0.1f);
            _difficulty.Levels[DifficultiesCount - 1].MinSpeed = PlayerPrefs.GetFloat(PlayerPrefKeys.MinSpeed, 0.1f);
        }

        private static void SaveDifficulty()
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.EnemiesCount, _difficulty.Levels[DifficultiesCount - 1].Count);
            PlayerPrefs.SetFloat(PlayerPrefKeys.MaxSpeed, _difficulty.Levels[DifficultiesCount - 1].MaxSpeed);
            PlayerPrefs.SetFloat(PlayerPrefKeys.MinSpeed, _difficulty.Levels[DifficultiesCount - 1].MinSpeed);
        }
    }
}
