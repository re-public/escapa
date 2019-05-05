using Escapa.Utility;
using UnityEngine;

namespace Escapa.Components
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundPlayer : MonoBehaviour, ISoundPlayer
    {
        public bool IsMuted
        {
            get => _audioSource.mute;
            set => _audioSource.mute = value;
        }
        
        private AudioSource _audioSource;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            IsMuted = PlayerPrefs.GetInt(PlayerPrefKeys.IsSoundMuted, 0) == 1;
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
            PlayerPrefs.SetInt(PlayerPrefKeys.IsSoundMuted, IsMuted ? 1 : 0);
        }
    }
}