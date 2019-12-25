using Escapa.Core.Events;
using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Game
{
    [RequireComponent(typeof(Camera))]
    public sealed class MainCamera : MonoBehaviour, IMainCamera
    {
        public float UnitsPerPixel => _camera.orthographicSize * 2.0f / Screen.height;

        public Vector2 ScreenToWorldPoint(Vector2 point) => _camera.ScreenToWorldPoint(point);

        private Camera _camera;
        private IStyleController _styleController;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _styleController = GameObject.FindWithTag(Tags.StyleController).GetComponent<IStyleController>();
        }

        private void OnEnable()
        {
            _styleController.Changed += OnStyleChanged;
        }

        private void OnDisable()
        {
            _styleController.Changed -= OnStyleChanged;
        }

        private void OnStyleChanged(object sender, StyleEventArgs e)
        {
            _camera.backgroundColor = e.Style.Background;
        }
    }
}
