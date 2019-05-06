using Escapa.Controllers;
using Escapa.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Components
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SoundPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;

        private ISystemController _systemController;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            _audioSource = GetComponent<AudioSource>();
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
        }

        private void OnEnable()
        {
            _systemController.MuteChanged += OnMuteChanged;
            _systemController.SceneLoaded += OnSceneLoaded;
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

        private void OnDisable()
        {
            _systemController.MuteChanged += OnMuteChanged;
            _systemController.SceneLoaded += OnSceneLoaded;
        }

        private void OnMuteChanged(SystemEventArgs e)
        {
            _audioSource.mute = e.IsSoundMuted;
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }

        private void OnSceneLoaded(SystemEventArgs e)
        {
            _audioSource.mute = e.IsSoundMuted;
            if (!_audioSource.isPlaying && e.Scene == GameScenes.Menu)
            {
                _audioSource.Play();
            }
        }
    }
}