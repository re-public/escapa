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
            SetToken();
            TextMesh.SetText(LanguageManager.GetString(token));
        }
        
        private new void OnDisable()
        {
            base.OnDisable();
            DifficultyManager.DifficultyChanged -= OnDifficultyChanged;
        }

        private void OnDifficultyChanged()
        {
            SetToken();
            TextMesh.SetText(LanguageManager.GetString(token));
        }

        private void SetToken()
        {
            switch (DifficultyManager.Level)
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
