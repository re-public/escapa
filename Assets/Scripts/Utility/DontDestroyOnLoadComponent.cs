using UnityEngine;

namespace Escapa.Utility
{
    public sealed class DontDestroyOnLoadComponent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
