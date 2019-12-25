using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Core.Controllers
{
    public sealed class SystemController : MonoBehaviour
    {
        private GameScenes _current;

        private void Awake()
        {
            Input.multiTouchEnabled = false;
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                GoBack();
        }

        private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

        private void GoBack()
        {
            if (_current == GameScenes.Menu)
                Application.Quit();
            else
                SceneManager.LoadScene((int)GameScenes.Menu, LoadSceneMode.Single);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) => _current = (GameScenes)scene.buildIndex;
    }
}
