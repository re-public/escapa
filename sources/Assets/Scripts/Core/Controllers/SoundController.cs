﻿using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundController : MonoBehaviour, ISoundController
    {
        private AudioSource audioSource;

        public bool IsMuted { get; private set; }

        public void ToggleSound()
        {
            IsMuted = !IsMuted;
            audioSource.mute = IsMuted;
        }

        private void Awake()
        {
            IsMuted = PlayerPrefs.GetInt(PlayerPrefKeys.IsSoundMuted, 0) == 1;

            audioSource = GetComponent<AudioSource>();
            audioSource.mute = IsMuted;
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
                PlayerPrefs.SetInt(PlayerPrefKeys.IsSoundMuted, IsMuted ? 1 : 0);
        }

        private void OnApplicationQuit() => PlayerPrefs.SetInt(PlayerPrefKeys.IsSoundMuted, IsMuted ? 1 : 0);
    }
}
