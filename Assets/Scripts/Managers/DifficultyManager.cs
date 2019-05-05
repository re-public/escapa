using Escapa.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Managers
{
    public static class DifficultyManager
    {
        private static DifficultyRules _difficulty;
        public static LevelRules Difficulty
        {
            get
            {
                if (_difficulty == null) LoadDifficulty();
                return _difficulty.Levels[(int) Level];
            }
        }

        private static Difficulties _level = (Difficulties) PlayerPrefs.GetInt(PlayerPrefKeys.Level, 0);
        public static Difficulties Level
        {
            get => _level;
            private set
            {
                _level = value;
                DifficultyChanged?.Invoke(Level);
            }
        }

        public static void AddLevel()
        {
            Level = Level == Difficulties.Insane ? 0 : Level + 1;
        }

        public static void SaveLevel() => PlayerPrefs.SetInt(PlayerPrefKeys.Level, (int) Level);

        public static event DifficultyEvent DifficultyChanged;
        
        public static void SaveDifficulty()
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.Level, (int) Level);
        }

        private static void LoadDifficulty()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Difficulty).text;
            _difficulty = JsonUtility.FromJson<DifficultyRules>(json);
        }
    }
}
