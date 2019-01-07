using Escapa.Events;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Controllers
{
    public sealed class SystemController : MonoBehaviour, ISystemController
    {
        public event SystemEvent SceneLoaded;
        public event SystemEvent SceneUnloaded;

        public void GoToScene(GameScenes scene) => SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Single);

        private void Awake()
        {
            DontDestroyOnLoad(Camera.main);
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            Application.quitting += OnApplicationQuit;
        }

        private void OnApplicationQuit()
        {
            DifficultyManager.SaveLevel();
            ScoreManager.SaveRecords();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            GameObject.FindWithTag(Tags.SceneController).GetComponent<ISceneController>().PrepareScene();
            SceneLoaded?.Invoke(gameObject, new SystemEventArgs(scene.buildIndex));
        }

        private void OnSceneUnloaded(Scene scene)
        {
            SceneUnloaded?.Invoke(gameObject, new SystemEventArgs(scene.buildIndex));
        }
    }
}
