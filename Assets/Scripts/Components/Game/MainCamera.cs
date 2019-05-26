using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Components.Game
{
    [RequireComponent(typeof(Camera))]
    public sealed class MainCamera : MonoBehaviour, IMainCamera
    {
        public float UnitsPerPixel => camera.orthographicSize * 2.0f / Screen.height;

        public Vector2 ScreenToWorldPoint(Vector2 point) => camera.ScreenToWorldPoint(point);

        private new Camera camera;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            camera = GetComponent<Camera>();
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

        private void OnDifficultyChanged() => camera.backgroundColor = StyleManager.Colors.Background;

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.buildIndex != (int)GameScenes.Preload)
                camera.backgroundColor = StyleManager.Colors.Background;
        }
    }
}
