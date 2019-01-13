using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Controllers
{
    public sealed class EndController : MonoBehaviour, ISceneController
    {
        public void PrepareScene()
        {
            _highScoreText.text = LanguageManager.Language.NewHighScore;
            _highScoreText.gameObject.SetActive(ScoreManager.IsHighScore);

            _timeText.text = LanguageManager.Language.Time + ScoreManager.CurrentRecord.ToString("0.000");
        }

        public void StyleScene()
        {
            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;
            _highScoreText.color = StyleManager.CurrentTheme.Text;
            _timeText.color = StyleManager.CurrentTheme.Text;
            _backButtonImage.color = StyleManager.CurrentTheme.Text;
            _restartButtonImage.color = StyleManager.CurrentTheme.Text;
        }

        private ISystemController _systemController;

        #region UI elements
        TextMeshProUGUI _highScoreText;
        TextMeshProUGUI _timeText;
        Image _backButtonImage;
        Image _restartButtonImage;
        #endregion

        private void Awake()
        {
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();

            _highScoreText = GameObject.FindWithTag(Tags.HighScoreText).GetComponent<TextMeshProUGUI>();
            _timeText = GameObject.FindWithTag(Tags.TimeText).GetComponent<TextMeshProUGUI>();
            _backButtonImage = GameObject.FindWithTag(Tags.BackButton).GetComponent<Image>();
            _restartButtonImage = GameObject.FindWithTag(Tags.RestartButton).GetComponent<Image>();
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                _systemController.GoToScene(GameScenes.Menu);
        }
    }
}
