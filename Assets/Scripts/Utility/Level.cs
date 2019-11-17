using UnityEngine;

namespace Escapa.Utility
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Level")]
    public sealed class Level : ScriptableObject
    {
        public Difficulties Difficulty;
        public float MinSpeed;
        public float MaxSpeed;
    }
}
