using Escapa.Utility;
using UnityEngine;

namespace Escapa.Managers
{
    public static class DifficultyManager
    {
        private const int _difficultiesCount = 5;
        public const int MaxEnemyCountForSetup = 6;
        public const int MinEnemyCountForSetup = 1;
        public const float MaxEnemySpeedForSetup = 8f;
        public const float MinEnemySpeedForSetup = 2f;

        public static bool CurrentLevelIsCustom => Level == _difficultiesCount - 1;
        public static bool CurrentLevelIsMin => Difficulty.Count == MinEnemyCountForSetup && Difficulty.MaxSpeed == MinEnemySpeedForSetup;
        public static bool CurrentLevelIsMax => Difficulty.Count == MaxEnemyCountForSetup && Difficulty.MinSpeed == MaxEnemySpeedForSetup;

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
        public static int AddLevel() => Level == (_difficultiesCount - 1) ? Level = 0 : Level++;
        public static void SaveLevel() => PlayerPrefs.SetInt(PlayerPrefKeys.Level, _level.Value);

        private static void LoadDifficulty()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Difficulty).text;
            _difficulty = JsonUtility.FromJson<DifficultyRules>(json);
            _difficulty.Levels[_difficultiesCount - 1].Count = PlayerPrefs.GetInt(PlayerPrefKeys.EnemiesCount, MinEnemyCountForSetup);
            _difficulty.Levels[_difficultiesCount - 1].MaxSpeed = PlayerPrefs.GetFloat(PlayerPrefKeys.MaxSpeed, MaxEnemySpeedForSetup);
            _difficulty.Levels[_difficultiesCount - 1].MinSpeed = PlayerPrefs.GetFloat(PlayerPrefKeys.MinSpeed, MinEnemySpeedForSetup);
        }

        private static void SaveDifficulty()
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.EnemiesCount, _difficulty.Levels[_difficultiesCount - 1].Count);
            PlayerPrefs.SetFloat(PlayerPrefKeys.MaxSpeed, _difficulty.Levels[_difficultiesCount - 1].MaxSpeed);
            PlayerPrefs.SetFloat(PlayerPrefKeys.MinSpeed, _difficulty.Levels[_difficultiesCount - 1].MinSpeed);
        }
    }
}
