using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Game
{
    [RequireComponent(typeof(Camera))]
    public sealed class MainCamera : MonoBehaviour, IMainCamera
    {
        public float UnitsPerPixel => camera.orthographicSize * 2.0f / Screen.height;

        public Vector2 ScreenToWorldPoint(Vector2 point) => camera.ScreenToWorldPoint(point);

        private new Camera camera;
        private IDifficultyController _difficulty;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            camera = GetComponent<Camera>();
            _difficulty = GameObject.FindWithTag(Tags.SystemController).GetComponent<IDifficultyController>();
        }

        private void OnEnable()
        {
            _difficulty.Changed += OnDifficultyChanged;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            _difficulty.Changed -= OnDifficultyChanged;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnDifficultyChanged() => camera.backgroundColor = StyleManager.Colors[(int)_difficulty.Current.Difficulty].Background;

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.buildIndex != (int)GameScenes.Preload)
                camera.backgroundColor = StyleManager.Colors[(int)_difficulty.Current.Difficulty].Background;
        }
    }
}
