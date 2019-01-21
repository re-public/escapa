using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Controllers
{
    public sealed class SocialController : MonoBehaviour, ISceneController
    {
        public void PrepareScene()
        {
            _highScoreTitleText.text = LanguageManager.Language.HighScoreTitle;
            _highScoreText.text = ScoreManager.CurrentTop.ToString();

            if (!Social.localUser.authenticated)
            {
                _achievementsButton.gameObject.SetActive(false);
                _leaderboardsButton.gameObject.SetActive(false);
            }
        }

        public void StyleScene()
        {
            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;
            _achievementsButton.color = StyleManager.CurrentTheme.Text;
            _backButton.color = StyleManager.CurrentTheme.Text;
            _leaderboardsButton.color = StyleManager.CurrentTheme.Text;
            _highScoreTitleText.color = StyleManager.CurrentTheme.Text;
            _highScoreText.color = StyleManager.CurrentTheme.Text;
        }

        public void OpenAchievements() => SocialManager.ShowAchievements();

        public void OpenLeaderboards() => SocialManager.ShowLeaderboards();

        private ISystemController _systemController;
        #region UI elements
        private Image _achievementsButton;
        private Image _backButton;
        private Image _leaderboardsButton;
        private TextMeshProUGUI _highScoreTitleText;
        private TextMeshProUGUI _highScoreText;
        #endregion

        private void Awake()
        {
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
            _achievementsButton = GameObject.FindWithTag(Tags.AchievementsButton).GetComponent<Image>();
            _backButton = GameObject.FindWithTag(Tags.BackButton).GetComponent<Image>();
            _leaderboardsButton = GameObject.FindWithTag(Tags.LeaderboardsButton).GetComponent<Image>();
            _highScoreTitleText = GameObject.FindWithTag(Tags.HighScoreTitleText).GetComponent<TextMeshProUGUI>();
            _highScoreText = GameObject.FindWithTag(Tags.HighScoreText).GetComponent<TextMeshProUGUI>();
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                _systemController.GoToScene(GameScenes.Menu);
        }
    }
}
