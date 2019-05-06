using Escapa.Buttons;
using Escapa.Events;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Controllers
{
    public sealed class SystemController : MonoBehaviour, ISystemController
    {
        public event SystemEvent MuteChanged;
        public event SystemEvent SceneLoaded;

        private bool _isMuted;
        private IButton _soundButton;
        private ISocialController _socialController;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(GameObject.FindWithTag(Tags.EventSystem));
            
            _isMuted = PlayerPrefs.GetInt(PlayerPrefKeys.IsSoundMuted, 0) == 1;
            
            _socialController = GetComponent<ISocialController>();

            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 60;
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnApplicationQuit()
        {
            DifficultyManager.SaveLevel();
            DifficultyManager.SaveDifficulty();
            ScoreManager.SaveRecords();
            _socialController.SaveAchievementsLocal();
            
            PlayerPrefs.SetInt(PlayerPrefKeys.IsSoundMuted, _isMuted ? 1 : 0);
        }

        private void OnDisable()
        {
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.buildIndex == (int) GameScenes.Menu)
            {
                _soundButton = GameObject.FindWithTag(Tags.SoundButton).GetComponent<IButton>();
                _soundButton.ButtonClicked += OnSoundButtonClicked;
            }
            SceneLoaded?.Invoke(new SystemEventArgs(_isMuted, (GameScenes) scene.buildIndex));
        }
        
        private void OnSceneUnloaded(Scene scene)
        {
            if (scene.buildIndex == (int) GameScenes.Menu)
            {
                _soundButton = GameObject.FindWithTag(Tags.SoundButton).GetComponent<IButton>();
                _soundButton.ButtonClicked -= OnSoundButtonClicked;
            }
        }

        private void OnSoundButtonClicked()
        {
            _isMuted = !_isMuted;
            MuteChanged?.Invoke(new SystemEventArgs(_isMuted, (GameScenes) SceneManager.GetActiveScene().buildIndex));
        }
    }
}
