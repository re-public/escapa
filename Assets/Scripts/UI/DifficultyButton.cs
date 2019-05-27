using Escapa.Core.Managers;
using Escapa.Utility;

namespace Escapa.UI
{
    public sealed class DifficultyButton : TextButtonBase
    {
        public override void Action() => DifficultyManager.Increase();

        protected override void OnDifficultyChanged()
        {
            base.OnDifficultyChanged();
            TextMesh.SetText(GetString(DifficultyManager.Current.difficulty));
        }

        private new void Start()
        {
            base.Start();
            TextMesh.SetText(GetString(DifficultyManager.Current.difficulty));
        }        
        
        private string GetString(Difficulties difficulty)
        {
            switch (difficulty)
            {
                default:
                case Difficulties.Easy:
                    return LanguageManager.GetString(LanguageTokens.DifficultyEasy);
                case Difficulties.Medium:
                    return LanguageManager.GetString(LanguageTokens.DifficultyMedium);
                case Difficulties.Hard:
                    return LanguageManager.GetString(LanguageTokens.DifficultyHard);
                case Difficulties.Insane:
                    return LanguageManager.GetString(LanguageTokens.DifficultyInsane);
            }
        }
    }
}
