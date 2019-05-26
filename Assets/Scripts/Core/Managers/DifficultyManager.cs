using Escapa.Core.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class DifficultyManager
    {
        private static readonly Level[] levels = Load();
        private static int currentDifficulty = PlayerPrefs.GetInt(PlayerPrefKeys.Difficulty, 0);

        public static event GameEvent DifficultyChanged;

        public static Level Current => levels[currentDifficulty];

        public static void Increase()
        {
            currentDifficulty = Current.difficulty == Difficulties.Insane
                ? (int)Difficulties.Easy
                : currentDifficulty + 1;
            DifficultyChanged?.Invoke();
        }

        public static void Decrease()
        {
            currentDifficulty = Current.difficulty == Difficulties.Easy
                ? (int)Difficulties.Insane
                : currentDifficulty - 1;
            DifficultyChanged?.Invoke();
        }

        public static void Save() => PlayerPrefs.SetInt(PlayerPrefKeys.Difficulty, currentDifficulty);

        private static Level[] Load()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Difficulty).text;
            return JsonUtility.FromJson<Levels>(json).levels;
        }
    }
}
