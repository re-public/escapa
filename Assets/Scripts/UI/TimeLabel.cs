using Escapa.Core.Events;
using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.UI
{
    [RequireComponent(typeof(Label))]
    public sealed class TimeLabel : MonoBehaviour
    {
        [SerializeField]
        private bool showHighScore;

        private IDifficultyController _difficultyController;
        private IScoreController _scoreController;
        private ITranslationController _translationController;
        private Label _label;
        private string _newHighScore;
        private string _highScore;

        private void Awake()
        {
            _label = GetComponent<Label>();
            _difficultyController = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
            _scoreController = GameObject.FindWithTag(Tags.ScoreController).GetComponent<IScoreController>();
            _translationController = GameObject.FindWithTag(Tags.TranslationController).GetComponent<ITranslationController>();

            _newHighScore = _translationController.Current.GetString(LanguageTokens.NewHighScore);
            _highScore = _translationController.Current.GetString(LanguageTokens.HighScoreTitle);
        }

        private void OnEnable()
        {
            _difficultyController.Changed += OnDifficultyChanged;
        }

        private void OnDisable()
        {
            _difficultyController.Changed -= OnDifficultyChanged;
        }

        private void OnDifficultyChanged(object sender, DifficultyEventArgs e)
        {
            string title = showHighScore ? _highScore
                : _scoreController.IsHighScore ? _newHighScore : string.Empty;

            var time = showHighScore
                ? _scoreController.GetHigh(e.Level.Difficulty)
                : _scoreController.LastTime;

            _label.SetText($"{title}\n{time.ToString("0.0")}");
        }
    }
}
