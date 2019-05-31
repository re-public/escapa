using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.UI
{
    public sealed class TimeLabel : Label
    {
        [SerializeField]
        private bool showHighScore;

        private string newHighScoreTitle = LanguageManager.GetString(LanguageTokens.NewHighScore);
        private string highScoreTitle = LanguageManager.GetString(LanguageTokens.HighScoreTitle);

        private new void Start()
        {
            base.Start();

            string title;
            if (showHighScore)
                title = highScoreTitle;
            else
                title = ScoreManager.IsHighScore ? newHighScoreTitle : string.Empty;

            var time = showHighScore ? ScoreManager.CurrentHigh : ScoreManager.LastTime;

            TextMesh.SetText($"{title}\n{time.ToString("0.0")}");
        }
    }
}
