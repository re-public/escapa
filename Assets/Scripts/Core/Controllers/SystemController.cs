using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Core.Controllers
{
    [RequireComponent(typeof(ISoundController))]
    public sealed class SystemController : MonoBehaviour
    {
        private GameScenes _current;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(GameObject.FindWithTag(Tags.EventSystem));

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
            DifficultyManager.SaveDifficulty();
            ScoreManager.SaveScores();
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
            if (_current == GameScenes.Preload)
            {
                SocialManager.Auth(OnAuthenticated);
            }
        }
        
        private void OnAuthenticated()
        {
            SceneManager.LoadSceneAsync((int) GameScenes.Menu, LoadSceneMode.Single);
        }
    }
}
