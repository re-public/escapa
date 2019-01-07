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
            highScore.gameObject.SetActive(ScoreManager.IsNewScore);
            highScore.text = ScoreManager.IsHighScore ? LanguageManager.Language.NewHighScore : LanguageManager.Language.NewRecord;
            highScore.color = StyleManager.CurrentTheme.Text;

            var timeText = GameObject.FindWithTag(Tags.TimeText).GetComponent<TextMeshProUGUI>();
            timeText.text = LanguageManager.Language.Time + ScoreManager.CurrentRecord.ToString("0.000");
            timeText.color = StyleManager.CurrentTheme.Text;

            GameObject.FindWithTag(Tags.BackButton).GetComponent<Image>().color = StyleManager.CurrentTheme.Text;
            GameObject.FindWithTag(Tags.RestartButton).GetComponent<Image>().color = StyleManager.CurrentTheme.Text;
        }
    }
}
