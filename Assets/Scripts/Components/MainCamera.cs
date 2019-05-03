using Escapa.Controllers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Components
{
    [RequireComponent(typeof(Camera))]
    public sealed class MainCamera : MonoBehaviour, IMainCamera
    {
        public float UnitsPerPixel => _camera.orthographicSize * 2.0f / Screen.height;
        
        private Camera _camera;
        private IStyleController _styleController;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            _camera = GetComponent<Camera>();
            _styleController = GameObject.FindWithTag(Tags.SystemController).GetComponent<IStyleController>();
        }

        private void OnEnable()
        {
            _styleController.StyleChanged += OnStyleChanged;
        }

        private void OnDisable()
        {
            _styleController.StyleChanged -= OnStyleChanged;
        }

        private void OnStyleChanged(Theme theme)
        {
            _camera.backgroundColor = theme.Background;
        }
    }
}
