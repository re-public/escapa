using System;
using Escapa.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Managers
{
    public static class DifficultyManager
    {
        private const int DifficultiesCount = 5;
        public const int MaxEnemyCountForSetup = 6;
        public const int MinEnemyCountForSetup = 1;
        public const float MaxEnemySpeedForSetup = 8f;
        public const float MinEnemySpeedForSetup = 2f;

        public static bool CurrentLevelIsCustom => Level == DifficultiesCount - 1;
        public static bool CurrentLevelIsMin => Difficulty.Count == MinEnemyCountForSetup && Math.Abs(Difficulty.MaxSpeed - MinEnemySpeedForSetup) < float.Epsilon;
        public static bool CurrentLevelIsMax => Difficulty.Count == MaxEnemyCountForSetup && Math.Abs(Difficulty.MinSpeed - MaxEnemySpeedForSetup) < float.Epsilon;

        private static DifficultyRules _difficulty;
        public static LevelRules Difficulty
        {
            get
            {
                if (_difficulty == null) LoadDifficulty();
                return _difficulty.Levels[Level];
            }
        }

        private static int? _level;
        public static int Level
        {
            get => _level ?? (_level = PlayerPrefs.GetInt(PlayerPrefKeys.Level, 0)).Value;
            private set
            {
                _level = value;
                DifficultyChanged?.Invoke(Level);
            }
        }

        public static void AddLevel()
        {
            Level = Level == DifficultiesCount - 1 ? 0 : Level + 1;
        }

        public static void SaveLevel() => PlayerPrefs.SetInt(PlayerPrefKeys.Level, Level);

        public static event DifficultyEvent DifficultyChanged;
        
        public static void SaveDifficulty()
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.EnemiesCount, _difficulty.Levels[DifficultiesCount - 1].Count);
            PlayerPrefs.SetFloat(PlayerPrefKeys.MaxSpeed, _difficulty.Levels[DifficultiesCount - 1].MaxSpeed);
            PlayerPrefs.SetFloat(PlayerPrefKeys.MinSpeed, _difficulty.Levels[DifficultiesCount - 1].MinSpeed);
        }

        private static void LoadDifficulty()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Difficulty).text;
            _difficulty = JsonUtility.FromJson<DifficultyRules>(json);
            _difficulty.Levels[DifficultiesCount - 1].Count = PlayerPrefs.GetInt(PlayerPrefKeys.EnemiesCount, MinEnemyCountForSetup);
            _difficulty.Levels[DifficultiesCount - 1].MaxSpeed = PlayerPrefs.GetFloat(PlayerPrefKeys.MaxSpeed, MaxEnemySpeedForSetup);
            _difficulty.Levels[DifficultiesCount - 1].MinSpeed = PlayerPrefs.GetFloat(PlayerPrefKeys.MinSpeed, MinEnemySpeedForSetup);
        }
    }
}
