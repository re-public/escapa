using Escapa.Core.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class DifficultyManager
    {
        private static readonly Level[] _levels = Load();
        private static int _currentDifficulty = PlayerPrefs.GetInt(PlayerPrefKeys.Difficulty, 0);

        public static event GameEvent DifficultyChanged;

        public static Level Current => _levels[_currentDifficulty];

        public static void Increase()
        {
            _currentDifficulty = Current.difficulty == Difficulties.Insane
                ? (int)Difficulties.Easy
                : _currentDifficulty + 1;
            DifficultyChanged?.Invoke();
        }

        public static void Decrease()
        {
            _currentDifficulty = Current.difficulty == Difficulties.Easy
                ? (int)Difficulties.Insane
                : _currentDifficulty - 1;
            DifficultyChanged?.Invoke();
        }

        public static void Save() => PlayerPrefs.SetInt(PlayerPrefKeys.Difficulty, _currentDifficulty);

        private static Level[] Load()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Difficulty).text;
            return JsonUtility.FromJson<Levels>(json).levels;
        }
    }
}
