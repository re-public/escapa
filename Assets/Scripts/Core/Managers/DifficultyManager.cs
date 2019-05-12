using Escapa.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    /// <summary>
    /// Manager for controlling difficulty.
    /// </summary>
    public static class DifficultyManager
    {
        private static Level[] _levels;
        private static int _currentDifficulty = PlayerPrefs.GetInt(PlayerPrefKeys.Difficulty, 0);

        /// <summary>
        /// Raised when dificulty has been changed.
        /// </summary>
        public static event GameEvent DifficultyChanged;
        
        /// <summary>
        /// Current difficulty level.
        /// </summary>
        public static Level Current
        {
            get
            {
                if (_levels == null)
                {
                    _levels = LoadDifficulty();
                }
                return _levels[_currentDifficulty];
            }
        }

        /// <summary>
        /// Increase difficulty level.
        /// </summary>
        public static void AddLevel()
        {
            _currentDifficulty = Current.difficulty == Difficulties.Insane ? 0 : _currentDifficulty + 1;
            DifficultyChanged?.Invoke();
        }

        /// <summary>
        /// Save current difficulty.
        /// </summary>
        public static void SaveDifficulty()
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.Difficulty, _currentDifficulty);
        }

        private static Level[] LoadDifficulty()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Difficulty).text;
            return JsonUtility.FromJson<Levels>(json).levels;
        }
    }
}
