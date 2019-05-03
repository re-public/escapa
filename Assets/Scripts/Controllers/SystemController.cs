using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SystemController : MonoBehaviour, ISystemController
    {
        public bool IsSoundEnabled
        {
            get => !_audioSource.mute;
            set => _audioSource.mute = !value;
        }

        private AudioSource _audioSource;
        private ISocialController _socialController;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(GameObject.FindWithTag(Tags.EventSystem));

            _audioSource = GetComponent<AudioSource>();
            _socialController = GetComponent<ISocialController>();

            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 60;
            Application.quitting += OnApplicationQuit;
        }

        private void Start() => IsSoundEnabled = PlayerPrefs.GetInt(PlayerPrefKeys.IsSoundEnabled, 1) == 1;

        private void OnApplicationQuit()
        {
            DifficultyManager.SaveLevel();
            DifficultyManager.SaveDifficulty();
            ScoreManager.SaveRecords();
            _socialController.SaveAchievementsLocal();
            PlayerPrefs.SetInt(PlayerPrefKeys.IsSoundEnabled, IsSoundEnabled ? 1 : 0);
        }
    }
}
