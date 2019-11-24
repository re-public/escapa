using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundController : MonoBehaviour, ISoundController
    {
        private AudioSource _audioSource;

        public bool IsMuted { get; private set; }

        public void ToggleSound()
        {
            IsMuted = !IsMuted;
            _audioSource.mute = IsMuted;
        }

        private void Awake()
        {
            IsMuted = PlayerPrefs.GetInt(PlayerPrefKeys.IsSoundMuted, 0) == 1;

            _audioSource = GetComponent<AudioSource>();
            _audioSource.mute = IsMuted;
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
                PlayerPrefs.SetInt(PlayerPrefKeys.IsSoundMuted, IsMuted ? 1 : 0);
        }

        private void OnApplicationQuit() => PlayerPrefs.SetInt(PlayerPrefKeys.IsSoundMuted, IsMuted ? 1 : 0);
    }
}
