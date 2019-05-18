using System;
using Escapa.Core.Managers;
using Escapa.Utility;

namespace Escapa.Components.Buttons
{
    public sealed class DifficultyButton : TextButtonBase
    {
        public override void Action()
        {
            DifficultyManager.AddLevel();
            SetToken(DifficultyManager.Current.difficulty);
            TextMesh.SetText(LanguageManager.GetString(token));
        }

        private new void Start()
        {
            base.Start();
            SetToken(DifficultyManager.Current.difficulty);
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
