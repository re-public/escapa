using System;
using Escapa.Managers;
using Escapa.Utility;

namespace Escapa.Buttons
{
    public sealed class DifficultyButton : TextButtonBase
    {
        public override void Action()
        {
            DifficultyManager.AddLevel();
        }

        private new void OnEnable()
        {
            base.OnEnable();
            DifficultyManager.DifficultyChanged += OnDifficultyChanged;
        }

        private new void Start()
        {
            base.Start();
            SetToken(DifficultyManager.Level);
            TextMesh.SetText(LanguageManager.GetString(token));
        }
        
        private new void OnDisable()
        {
            base.OnDisable();
            DifficultyManager.DifficultyChanged -= OnDifficultyChanged;
        }

        private void OnDifficultyChanged(Difficulties difficulty)
        {
            SetToken(difficulty);
            TextMesh.SetText(LanguageManager.GetString(token));
        }

        private void SetToken(Difficulties difficulty)
        {
            switch (difficulty)
            {
                case Difficulties.Easy:
                    token = LanguageTokens.DifficultyEasy;
                    break;
                case Difficulties.Medium:
                    token = LanguageTokens.DifficultyMedium;
                    break;
                case Difficulties.Hard:
                    token = LanguageTokens.DifficultyHard;
                    break;
                case Difficulties.Insane:
                    token = LanguageTokens.DifficultyInsane;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(difficulty));
            }
        }
    }
}
