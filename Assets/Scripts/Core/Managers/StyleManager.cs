using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class StyleManager
    {
        private static readonly Colors[] colors = Load();

        public static Colors Colors => colors[(int)DifficultyManager.Current.difficulty];

        private static Colors[] Load()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Style).text;
            return JsonUtility.FromJson<Style>(json).Colors;
        }
    }
}