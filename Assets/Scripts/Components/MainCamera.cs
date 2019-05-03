using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Components
{
    [RequireComponent(typeof(Camera))]
    public sealed class MainCamera : MonoBehaviour, IMainCamera
    {
        public float UnitsPerPixel => _camera.orthographicSize * 2.0f / Screen.height;
        
        private Camera _camera;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            _camera = GetComponent<Camera>();
        }

        private void OnEnable()
        {
            StyleManager.StyleChanged += OnStyleChanged;
        }

        private void OnDisable()
        {
            StyleManager.StyleChanged -= OnStyleChanged;
        }

        private void OnStyleChanged(Theme theme)
        {
            _camera.backgroundColor = theme.Background;
        }
    }
}
