using Escapa.Managers;

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
            TextMesh.SetText(LanguageManager.Language.Difficulties[DifficultyManager.Level]);
        }
        
        private new void OnDisable()
        {
            base.OnDisable();
            DifficultyManager.DifficultyChanged -= OnDifficultyChanged;
        }

        private void OnDifficultyChanged() => TextMesh.SetText(LanguageManager.Language.Difficulties[DifficultyManager.Level]);
    }
}
