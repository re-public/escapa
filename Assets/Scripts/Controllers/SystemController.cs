using Escapa.Events;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Controllers
{
    [RequireComponent(typeof(ISoundController))]
    public sealed class SystemController : MonoBehaviour, ISystemController
    {
        public event SystemEvent SceneLoaded;
        
        private ISocialController _socialController;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(GameObject.FindWithTag(Tags.EventSystem));
            
            _socialController = GetComponent<ISocialController>();

            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 60;
        }

        private void OnApplicationQuit()
        {
            DifficultyManager.SaveLevel();
            DifficultyManager.SaveDifficulty();
            ScoreManager.SaveRecords();
            _socialController.SaveAchievementsLocal();
        }
    }
}
