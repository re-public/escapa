using Escapa.Core.Events;
using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    public sealed class DifficultyController : MonoBehaviour, IDifficultyController
    {
        public event GameEvent Changed;

        public Level Current => Levels[_currentDifficulty];

        public void Increase()
        {
            _currentDifficulty = Current.Difficulty == Difficulties.Insane
                ? (int)Difficulties.Easy
                : _currentDifficulty + 1;
            Changed?.Invoke();
        }

        public void Decrease()
        {
            _currentDifficulty = Current.Difficulty == Difficulties.Easy
                ? (int)Difficulties.Insane
                : _currentDifficulty - 1;
            Changed?.Invoke();
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _currentDifficulty = PlayerPrefs.GetInt(PlayerPrefKeys.Difficulty, 0);
        }

        // If user wants to quit using Task Manager.
        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                Save();
            }
        }

        // If user wants to quit the usual way.
        private void OnApplicationQuit() => Save();

        [SerializeField]
        private Level[] Levels;

        private int _currentDifficulty;

        private void Save() => PlayerPrefs.SetInt(PlayerPrefKeys.Difficulty, _currentDifficulty);
    }
}
