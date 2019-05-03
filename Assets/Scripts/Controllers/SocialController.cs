using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Escapa.Controllers
{
    public sealed class SocialController : MonoBehaviour
    {
        public void OpenAchievements() => SocialManager.ShowAchievements();

        public void OpenLeaderboards() => SocialManager.ShowLeaderboards();
        
        #region UI elements
        private Image _achievementsButton;
        private Image _leaderboardsButton;
        #endregion

        private void Awake()
        {
            _achievementsButton = GameObject.FindWithTag(Tags.AchievementsButton).GetComponent<Image>();
            _leaderboardsButton = GameObject.FindWithTag(Tags.LeaderboardsButton).GetComponent<Image>();
        }

        private void Start()
        {
            if (!Social.localUser.authenticated)
            {
                _achievementsButton.gameObject.SetActive(false);
                _leaderboardsButton.gameObject.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                SceneManager.LoadSceneAsync((int) GameScenes.Menu, LoadSceneMode.Single);
        }
    }
}
