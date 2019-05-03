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

        private void OnDifficultyChanged(int difficulty)
        {
            SetToken(difficulty);
            TextMesh.SetText(LanguageManager.GetString(token));
        }

        private void SetToken(int difficulty)
        {
            switch (difficulty)
            {
                case 1:
                    token = LanguageTokens.DifficultyMedium;
                    break;
                case 2:
                    token = LanguageTokens.DifficultyHard;
                    break;
                case 3:
                    token = LanguageTokens.DifficultyInsane;
                    break;
                case 4:
                    token = LanguageTokens.DifficultyCustom;
                    break;
                default:
                    token = LanguageTokens.DifficultyEasy;
                    break;
            }
        }
    }
}
