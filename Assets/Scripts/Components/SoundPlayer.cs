using Escapa.Buttons;
using Escapa.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Components
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundPlayer : MonoBehaviour, ISoundPlayer
    {
        public event SystemEvent MuteChanged;
        
        private AudioSource _audioSource;
        private bool _isMuted;

        private IButton _soundButton;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            _audioSource = GetComponent<AudioSource>();
            _soundButton = GameObject.FindWithTag(Tags.SoundButton).GetComponent<IButton>();
        }

        private void OnEnable()
        {
            _soundButton.ButtonClicked += OnSoundButtonClicked;
        }

        private void Start()
        {
            _isMuted = PlayerPrefs.GetInt(PlayerPrefKeys.IsSoundMuted, 0) == 1;
            _audioSource.mute = _isMuted;
            MuteChanged?.Invoke(new SystemEventArgs(_isMuted));
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
            _soundButton.ButtonClicked -= OnSoundButtonClicked;
        }

        private void OnSoundButtonClicked()
        {
            _isMuted = !_isMuted;
            _audioSource.mute = _isMuted;
            MuteChanged?.Invoke(new SystemEventArgs(_isMuted));
        }
    }
}