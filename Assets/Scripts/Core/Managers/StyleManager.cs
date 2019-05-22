using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    /// <summary>
    /// Manager for controlling styles.
    /// </summary>
    public static class StyleManager
    {
        private static readonly Theme[] _themes = Load();

        /// <summary>
        /// Current theme.
        /// </summary>
        public static Theme Current => _themes[(int) DifficultyManager.Current.difficulty];
        
        private static Theme[] Load()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Style).text;
            return JsonUtility.FromJson<Style>(json).themes;
        }
    }
}