using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Controllers
{
    public sealed class SocialController : MonoBehaviour
    {
        public void OpenAchievements() => SocialManager.ShowAchievements();

        public void OpenLeaderboards() => SocialManager.ShowLeaderboards();

        private ISystemController _systemController;
        #region UI elements
        private Image _achievementsButton;
        private Image _leaderboardsButton;
        private TextMeshProUGUI _highScoreTitleText;
        private TextMeshProUGUI _highScoreText;
        #endregion

        private void Awake()
        {
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
            _highScoreTitleText = GameObject.FindWithTag(Tags.HighScoreTitleText).GetComponent<TextMeshProUGUI>();
            _highScoreText = GameObject.FindWithTag(Tags.HighScoreText).GetComponent<TextMeshProUGUI>();
            _achievementsButton = GameObject.FindWithTag(Tags.AchievementsButton).GetComponent<Image>();
            _leaderboardsButton = GameObject.FindWithTag(Tags.LeaderboardsButton).GetComponent<Image>();
        }

        private void Start()
        {
            _highScoreTitleText.SetText(LanguageManager.Language.HighScoreTitle);
            _highScoreText.SetText(ScoreManager.CurrentTop.ToString());

            if (!Social.localUser.authenticated)
            {
                _achievementsButton.gameObject.SetActive(false);
                _leaderboardsButton.gameObject.SetActive(false);
            }

            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;
            _highScoreTitleText.color = StyleManager.CurrentTheme.Text;
            _highScoreText.color = StyleManager.CurrentTheme.Text;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                _systemController.GoToScene(GameScenes.Menu);
        }
    }
}
