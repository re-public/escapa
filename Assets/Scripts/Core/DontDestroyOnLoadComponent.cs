using UnityEngine;

public sealed class DontDestroyOnLoadComponent : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
