using Escapa.Core.Interfaces;
using Escapa.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundController : MonoBehaviour, ISoundController
    {
        public event SoundEvent MuteChanged;

        private bool _isMuted;
        private AudioSource _audioSource;

        public void Mute()
        {
            _isMuted = !_isMuted;
            _audioSource.mute = _isMuted;
            
            MuteChanged?.Invoke(new SoundEventArgs(_isMuted));
        }
        
        private void Awake()
        {
            _isMuted = PlayerPrefs.GetInt(PlayerPrefKeys.IsSoundMuted, 0) == 1;
            
            _audioSource = GetComponent<AudioSource>();
            _audioSource.mute = _isMuted;
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.IsSoundMuted, _isMuted ? 1 : 0);
        }
    }
}