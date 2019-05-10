using Escapa.Events;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Controllers
{
    [RequireComponent(typeof(ISoundController))]
    public sealed class SystemController : MonoBehaviour, ISystemController
    {
        public event SystemEvent SceneLoaded;
        public event SystemEvent SceneUnloaded;
        
        private ISocialController _socialController;

        public void GoToScene(GameScenes scene)
        {
            SceneManager.LoadSceneAsync((int) scene, LoadSceneMode.Single);
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(GameObject.FindWithTag(Tags.EventSystem));
            
            _socialController = GetComponent<ISocialController>();

            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 60;
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnApplicationQuit()
        {
            DifficultyManager.SaveLevel();
            DifficultyManager.SaveDifficulty();
            ScoreManager.SaveRecords();
            _socialController.SaveAchievementsLocal();
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneLoaded?.Invoke(new SystemEventArgs((GameScenes) scene.buildIndex));
        }

        private void OnSceneUnloaded(Scene scene)
        {
            SceneUnloaded?.Invoke(new SystemEventArgs((GameScenes) scene.buildIndex));
        }
    }
}
