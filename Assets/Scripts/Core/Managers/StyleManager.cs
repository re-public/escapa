using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class StyleManager
    {
        private static readonly Theme[] themes = Load();

        public static Theme Current => themes[(int)DifficultyManager.Current.difficulty];

        private static Theme[] Load()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Style).text;
            return JsonUtility.FromJson<Style>(json).themes;
        }
    }
}