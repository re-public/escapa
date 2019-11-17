using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.UI
{
    [RequireComponent(typeof(Label))]
    public sealed class TimeLabel : MonoBehaviour
    {
        [SerializeField]
        private bool showHighScore;

        private IDifficultyController _difficulty;
        private IScoreController _score;
        private Label label;
        private string newHighScoreTitle;
        private string highScoreTitle;

        private void Awake()
        {
            label = GetComponent<Label>();
            _difficulty = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
            _score = GameObject.FindWithTag(Tags.ScoreController).GetComponent<IScoreController>();
            newHighScoreTitle = LanguageManager.GetString(LanguageTokens.NewHighScore);
            highScoreTitle = LanguageManager.GetString(LanguageTokens.HighScoreTitle);
    }

        private void Start()
        {
            string title;
            if (showHighScore)
                title = highScoreTitle;
            else
                title = _score.IsHighScore ? newHighScoreTitle : string.Empty;

            var time = showHighScore
                ? _score.GetHigh(_difficulty.Current.Difficulty)
                : _score.LastTime;

            label.SetText($"{title}\n{time.ToString("0.0")}");
        }
    }
}
