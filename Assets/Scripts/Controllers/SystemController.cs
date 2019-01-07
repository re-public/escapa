using Escapa.Events;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SystemController : MonoBehaviour, ISystemController
    {
        public event SystemEvent SceneLoaded;
        public event SystemEvent SceneUnloaded;

        public bool IsSoundEnabled
        {
            get => !_audioSource.mute;
            set => _audioSource.mute = !value;
        }

        public void GoToScene(GameScenes scene) => SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Single);

        private AudioSource _audioSource;

        private void Awake()
        {
            DontDestroyOnLoad(Camera.main);
            DontDestroyOnLoad(gameObject);

            _audioSource = GetComponent<AudioSource>();

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            Application.quitting += OnApplicationQuit;
        }

        private void Start()
        {
            IsSoundEnabled = PlayerPrefs.GetInt(PlayerPrefKeys.IsSoundEnabled, 1) == 1 ? true : false;
        }

        private void OnApplicationQuit()
        {
            DifficultyManager.SaveLevel();
            ScoreManager.SaveRecords();
            PlayerPrefs.SetInt(PlayerPrefKeys.IsSoundEnabled, IsSoundEnabled ? 1 : 0);
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
