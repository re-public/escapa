using Escapa.Events;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class StyleController : MonoBehaviour, IStyleController
    {
        public event StyleEvent StyleChanged;

        private int _currentStyle;
        private Style _style;

        private ISystemController _systemController;

        private void Awake()
        {
            _systemController = GetComponent<ISystemController>();
            
            var json = Resources.Load<TextAsset>(ResourceKeys.Style).text;
            _style = JsonUtility.FromJson<Style>(json);
        }

        private void OnEnable()
        {
            _systemController.SceneLoaded += OnSceneLoaded;
            DifficultyManager.DifficultyChanged += OnDifficultyChanged;
        }

        private void OnDisable()
        {
            _systemController.SceneLoaded -= OnSceneLoaded;
            DifficultyManager.DifficultyChanged -= OnDifficultyChanged;
        }

        private void OnDifficultyChanged(Difficulties difficulty)
        {
            _currentStyle = (int) difficulty;
            StyleChanged?.Invoke(new StyleEventArgs(_style.Themes[_currentStyle]));
        }

        private void OnSceneLoaded(SystemEventArgs e)
        {
            if(e.Scene != GameScenes.Preload)
                StyleChanged?.Invoke(new StyleEventArgs(_style.Themes[_currentStyle]));   
        }
    }
}