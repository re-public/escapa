using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    /// <summary>
    /// Manager for controlling styles.
    /// </summary>
    public static class StyleManager
    {
        private static Theme[] _themes;

        /// <summary>
        /// Current theme.
        /// </summary>
        public static Theme Current
        {
            get
            {
                if (_themes == null)
                {
                    _themes = LoadStyle();
                }

                return _themes[(int) DifficultyManager.Current.difficulty];
            }
        }
        
        private static Theme[] LoadStyle()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Style).text;
            return JsonUtility.FromJson<Style>(json).themes;
        }
    }
}