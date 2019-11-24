using UnityEngine;

namespace Escapa.Utility
{
    [CreateAssetMenu(fileName = "Style", menuName = "Scriptable Objects/Style")]
    public sealed class Style: ScriptableObject
    {
        public Color Background;
        public Color Enemy;
        public Color Player;
        public Color Text;
        public Color TextAlfa;
    }
}