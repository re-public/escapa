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
            var highScore = GameObject.FindWithTag(Tags.HighScoreText).GetComponent<TextMeshProUGUI>();            
            highScore.text = LanguageManager.Language.NewHighScore;
            highScore.color = StyleManager.CurrentTheme.Text;
            highScore.gameObject.SetActive(ScoreManager.IsHighScore);

            var timeText = GameObject.FindWithTag(Tags.TimeText).GetComponent<TextMeshProUGUI>();
            timeText.text = LanguageManager.Language.Time + ScoreManager.CurrentRecord.ToString("0.000");
            timeText.color = StyleManager.CurrentTheme.Text;

            GameObject.FindWithTag(Tags.BackButton).GetComponent<Image>().color = StyleManager.CurrentTheme.Text;
            GameObject.FindWithTag(Tags.RestartButton).GetComponent<Image>().color = StyleManager.CurrentTheme.Text;
        }

        private ISystemController _systemController;

        private void Awake() => _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                _systemController.GoToScene(GameScenes.Menu);
        }
    }
}
