using Escapa.Utility;
using System;
using UnityEngine;

namespace Escapa.Managers
{
    public static class StyleManager
    {
        [Serializable]
        private class Style
        {
            public Theme[] Themes;
        }

        [Serializable]
        public struct Theme
        {
            public Color Background;
            public Color Enemy;
            public Color Player;
            public Color Text;
        }

        private static Style _style = null;
        public static Theme CurrentTheme
        {
            get
            {
                if (_style == null) LoadStyle();
                return _style.Themes[DifficultyManager.Level];
            }
        }

        private static void LoadStyle()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Style).text;
            _style = JsonUtility.FromJson<Style>(json);
        }
    }
}
