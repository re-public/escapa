using Escapa.Events;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Controllers
{
    public sealed class StyleController : MonoBehaviour, IStyleController
    {
        public event StyleEvent StyleChanged;

        private int _currentTheme;
        private Style _style;

        private void Awake()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Style).text;
            _style = JsonUtility.FromJson<Style>(json);
        }

        private void OnEnable()
        {
            DifficultyManager.DifficultyChanged += OnDifficultyChanged;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnDifficultyChanged(Difficulties difficulty)
        {
            _currentTheme = (int) difficulty;
            StyleChanged?.Invoke(_style.Themes[_currentTheme]);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode _)
        {
            if(scene.buildIndex != (int)GameScenes.Preload)
                StyleChanged?.Invoke(_style.Themes[_currentTheme]);   
        }
    }
}