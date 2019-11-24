using Escapa.Core.Events;
using Escapa.Core.Interfaces;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class Label : MonoBehaviour, ILabel
    {
        public LanguageTokens Token
        {
            get => token;
            set => token = value;
        }

        public void SetText(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
                _textMesh.SetText(text);
            else
                _textMesh.SetText(_translationController.Current.GetString(Token));
        }

        [SerializeField]
        private bool isAlfa;
        [SerializeField]
        private LanguageTokens token;

        private TextMeshProUGUI _textMesh;
        private IStyleController _styleController;
        private ITranslationController _translationController;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            _styleController = GameObject.FindWithTag(Tags.StyleController).GetComponent<IStyleController>();
            _translationController = GameObject.FindWithTag(Tags.TranslationController).GetComponent<ITranslationController>();
        }

        private void OnEnable()
        {
            _styleController.Changed += OnStyleChanged;
        }

        private void Start()
        {
            if(Token != LanguageTokens.None)
                _textMesh.SetText(_translationController.Current.GetString(Token));
        }

        private void OnDisable()
        {
            _styleController.Changed -= OnStyleChanged;
        }

        private void OnStyleChanged(object sender, StyleEventArgs e)
        {
            _textMesh.color = isAlfa ? e.Style.TextAlfa : e.Style.Text;
        }
    }
}
