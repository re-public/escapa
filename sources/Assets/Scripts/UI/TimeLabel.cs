﻿using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.UI
{
    [RequireComponent(typeof(Label))]
    public sealed class TimeLabel : MonoBehaviour
    {
        [SerializeField]
        private bool showHighScore;

        private Label label;
        private string newHighScoreTitle;
        private string highScoreTitle;

        private void Awake()
        {
            label = GetComponent<Label>();
            newHighScoreTitle = LanguageManager.GetString(LanguageTokens.NewHighScore);
            highScoreTitle = LanguageManager.GetString(LanguageTokens.HighScoreTitle);
    }

        private void Start()
        {
            string title;
            if (showHighScore)
                title = highScoreTitle;
            else
                title = ScoreManager.IsHighScore ? newHighScoreTitle : string.Empty;

            var time = showHighScore ? ScoreManager.CurrentHigh : ScoreManager.LastTime;

            label.SetText($"{title}\n{time.ToString("0.0")}");
        }
    }
}
