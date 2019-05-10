using Escapa.Buttons;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Controllers
{
    [RequireComponent(typeof(ISoundController))]
    public sealed class SystemController : MonoBehaviour
    {
        private GameScenes _current;
        private ISocialController _socialController;
        private IButton _backButton;

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
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                GoBack();
            }
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
        }

        private void GoBack()
        {
            if(_current == GameScenes.Menu)
            {
                Application.Quit();
            }
            else
            {
                SceneManager.LoadSceneAsync((int) GameScenes.Menu, LoadSceneMode.Single);
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            _current = (GameScenes) scene.buildIndex;
        }
    }
}
