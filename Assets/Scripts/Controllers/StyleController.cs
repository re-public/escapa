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

        private int _currentStyle;
        private Style _style;
        
        private void Awake()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Style).text;
            _style = JsonUtility.FromJson<Style>(json);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DifficultyManager.DifficultyChanged += OnDifficultyChanged;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            DifficultyManager.DifficultyChanged -= OnDifficultyChanged;
        }

        private void OnDifficultyChanged(Difficulties difficulty)
        {
            _currentStyle = (int) difficulty;
            StyleChanged?.Invoke(new StyleEventArgs(_style.Themes[_currentStyle]));
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if(scene.buildIndex != (int) GameScenes.Preload)
            {
                StyleChanged?.Invoke(new StyleEventArgs(_style.Themes[_currentStyle]));
            }   
        }
    }
}