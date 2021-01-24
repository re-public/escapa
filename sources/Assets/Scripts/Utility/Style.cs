using System;
using UnityEngine;

namespace Escapa.Utility
{
    [Serializable]
    public class Style
    {
        public Colors[] Colors;
    }

    [Serializable]
    public class Colors
    {
        public Color Background;
        public Color Enemy;
        public Color Player;
        public Color Text;
        public Color TextAlfa;
    }
}