using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Button), typeof(TextMeshProUGUI))]
    public sealed class DifficultyButton : MonoBehaviour, IButton
    {
        public void Action() => DifficultyManager.AddLevel();

        private TextMeshProUGUI _buttonText;

        private void Awake()
        {
            _buttonText = GetComponent<TextMeshProUGUI>();

            DifficultyManager.DifficultyChanged += OnDifficultyChanged;
            StyleManager.StyleChanged += OnStyleChanged;
        }

        private void Start()
        {
            _buttonText.color = _buttonText.color = StyleManager.CurrentTheme.Text;
            _buttonText.SetText(LanguageManager.Language.Difficulties[DifficultyManager.Level]);
        }

        private void OnDifficultyChanged() => _buttonText.SetText(LanguageManager.Language.Difficulties[DifficultyManager.Level]);
        private void OnStyleChanged(Theme theme) => _buttonText.color = theme.Text;
    }
}
