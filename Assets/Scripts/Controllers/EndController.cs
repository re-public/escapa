using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class EndController : MonoBehaviour, ISceneController
    {
        public void PrepareScene()
        {
            var highScore = GameObject.FindWithTag(Tags.HighScoreText);
            highScore.SetActive(ScoreManager.IsNewScore);
            highScore.GetComponent<TextMeshProUGUI>().text = ScoreManager.IsHighScore ? LanguageManager.Language.NewHighScore : LanguageManager.Language.NewRecord;

            GameObject.FindWithTag(Tags.TimeText).GetComponent<TextMeshProUGUI>().text = LanguageManager.Language.Time + ScoreManager.CurrentRecord.ToString("0.000");
        }
    }
}
