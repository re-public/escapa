using Escapa.Core.Events;
using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Core.Controllers
{
    public sealed class DifficultyController : MonoBehaviour, IDifficultyController
    {
        public event DifficultyEvent Changed;

        public void Increase()
        {
            _current = _current == Difficulties.Insane ? Difficulties.Easy : _current + 1;
            Changed?.Invoke(gameObject, new DifficultyEventArgs(Levels[(int)_current]));
        }

        public void Decrease()
        {
            _current = _current == Difficulties.Easy ? Difficulties.Insane : _current - 1;
            Changed?.Invoke(gameObject, new DifficultyEventArgs(Levels[(int)_current]));
        }

        private void Awake() => _current = (Difficulties)PlayerPrefs.GetInt(PlayerPrefKeys.Difficulty, 0);

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
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

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        [SerializeField]
        private Level[] Levels;

        private Difficulties _current;

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            Changed?.Invoke(null, new DifficultyEventArgs(Levels[(int)_current]));
        }

        private void Save() => PlayerPrefs.SetInt(PlayerPrefKeys.Difficulty, (int)_current);
    }
}
