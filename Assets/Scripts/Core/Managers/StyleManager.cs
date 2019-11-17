using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class StyleManager
    {
        public static Colors[] Colors { get; } = Load();

        private static Colors[] Load()
        {
            var json = Resources.Load<TextAsset>(ResourceKeys.Style).text;
            return JsonUtility.FromJson<Style>(json).Colors;
        }
    }
}