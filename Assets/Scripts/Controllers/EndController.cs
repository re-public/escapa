using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class EndController : MonoBehaviour
    {
        private ISystemController _systemController;
        #region UI elements
        private TextMeshProUGUI _highScoreText;
        private TextMeshProUGUI _timeText;
        #endregion

        private void Awake()
        {
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();

            _highScoreText = GameObject.FindWithTag(Tags.HighScoreText).GetComponent<TextMeshProUGUI>();
            _timeText = GameObject.FindWithTag(Tags.TimeText).GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _highScoreText.text = LanguageManager.Language.NewHighScore;
            _highScoreText.gameObject.SetActive(ScoreManager.IsHighScore);

            _timeText.text = LanguageManager.Language.Time + ScoreManager.LastTime.ToString("0.000");

            //Style
            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;
            _highScoreText.color = StyleManager.CurrentTheme.Text;
            _timeText.color = StyleManager.CurrentTheme.Text;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                _systemController.GoToScene(GameScenes.Menu);
        }
    }
}
