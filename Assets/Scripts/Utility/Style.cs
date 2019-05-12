using System;
using UnityEngine;

namespace Escapa.Utility
{
    [Serializable]
    public class Style
    {
        public Theme[] themes;
    }

    [Serializable]
    public class Theme
    {
        public Color background;
        public Color enemy;
        public Color player;
        public Color text;
        public Color textAlfa;
    }
}