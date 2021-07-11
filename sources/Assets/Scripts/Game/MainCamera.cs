using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Game
{
    [RequireComponent(typeof(Camera))]
    public sealed class MainCamera : MonoBehaviour, IMainCamera
    {
        public float UnitsPerPixel { get; private set; }

        public Vector2 ScreenToWorldPoint(Vector2 point) => Camera.ScreenToWorldPoint(point);

        private Camera _camera;
        private Camera Camera
        {
            get
            {
#if DEBUG
                if (_camera == null)
                    throw new InvalidOperationException();
#endif
                return _camera;
            }
            set => _camera = value;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            Camera = GetComponent<Camera>();
            UnitsPerPixel = Camera.orthographicSize * 2.0f / Screen.height;
        }

        private void OnEnable()
        {
            DifficultyManager.DifficultyChanged += OnDifficultyChanged;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            DifficultyManager.DifficultyChanged -= OnDifficultyChanged;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnDifficultyChanged(object sender, EventArgs e) => Camera.backgroundColor = StyleManager.Colors.Background;

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.buildIndex != (int)GameScenes.Preload)
                Camera.backgroundColor = StyleManager.Colors.Background;
        }
    }
}
