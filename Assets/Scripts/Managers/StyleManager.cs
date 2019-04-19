using Escapa.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Managers
{
    public static class StyleManager
    {
        private static Style _style = null;
        public static Theme CurrentTheme
        {
            get
            {
                if (_style == null) LoadStyle();
                return _style.Themes[DifficultyManager.Level];
            }
        }

        public static event StyleEvent StyleChanged;

        private static void LoadStyle()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Style).text;
            _style = JsonUtility.FromJson<Style>(json);

            DifficultyManager.DifficultyChanged += OnDifficultyChanged;
        }

        private static void OnDifficultyChanged() => StyleChanged?.Invoke(CurrentTheme);
    }
}
