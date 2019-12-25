using UnityEngine;

namespace Escapa.Utility
{
    [CreateAssetMenu(fileName = "Achievements", menuName = "Scriptable Objects/Achievements config")]
    public sealed class AchievementsConfig: ScriptableObject
    {
        public float BlackHawk;
        public float Zen;
        public float Jagger;
    }
}
