using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Button))]
    public sealed class LinkButton : MonoBehaviour, IButton
    {
        public string Url;

        public void Action() => Application.OpenURL(Url);

        private TextMeshProUGUI _buttonText;

        private void Awake()
        {
            _buttonText = GetComponent<TextMeshProUGUI>();

            StyleManager.StyleChanged += OnStyleChanged;
        }

        private void Start() => _buttonText.color = _buttonText.color = StyleManager.CurrentTheme.Text;
        private void OnStyleChanged(Theme theme) => _buttonText.color = theme.Text;
    }
}
