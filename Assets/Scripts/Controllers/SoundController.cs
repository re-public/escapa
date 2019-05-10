using Escapa.Buttons;
using Escapa.Events;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundController : MonoBehaviour, ISoundController
    {
        public event SoundEvent MuteChanged;

        private bool _isMuted;
        private AudioSource _audioSource;
        private IButton _soundButton;

        private void Awake()
        {
            _isMuted = PlayerPrefs.GetInt(PlayerPrefKeys.IsSoundMuted, 0) == 1;
            
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                _audioSource.Pause();
            }
            else
            {
                _audioSource.UnPause();
            }
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.IsSoundMuted, _isMuted ? 1 : 0);
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.buildIndex == (int) GameScenes.Menu)
            {
                if (!_isMuted && _audioSource.isPlaying)
                {
                    _audioSource.Play();
                }
                
                _soundButton = GameObject.FindWithTag(Tags.SoundButton).GetComponent<IButton>();
                _soundButton.ButtonClicked += OnSoundButtonClicked;
            }
        }

        private void OnSceneUnloaded(Scene scene)
        {
            if (scene.buildIndex == (int) GameScenes.Menu)
            {
                _soundButton.ButtonClicked -= OnSoundButtonClicked;
            }   
        }

        private void OnSoundButtonClicked()
        {
            _isMuted = !_isMuted;
            _audioSource.mute = _isMuted;
            if (!_isMuted && _audioSource.isPlaying)
            {
                _audioSource.Play();
            }
            
            MuteChanged?.Invoke(new SoundEventArgs(_isMuted));
        }
    }
}