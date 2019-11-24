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
                textMesh.SetText(text);
            else
                textMesh.SetText(_translation.Current.GetString(Token));
        }

        [SerializeField]
        private bool isAlfa;
        [SerializeField]
        private LanguageTokens token;

        private TextMeshProUGUI textMesh;
        private IStyleController _styleController;
        private ITranslationController _translation;

        private void Awake()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
            _styleController = GameObject.FindWithTag(Tags.StyleController).GetComponent<IStyleController>();
            _translation = GameObject.FindWithTag(Tags.TranslationController).GetComponent<ITranslationController>();
        }

        private void OnEnable()
        {
            _styleController.Changed += OnStyleChanged;
        }

        private void Start()
        {
            if(Token != LanguageTokens.None)
                textMesh.SetText(_translation.Current.GetString(Token));
        }

        private void OnDisable()
        {
            _styleController.Changed -= OnStyleChanged;
        }

        private void OnStyleChanged(object sender, StyleEventArgs e)
        {
            textMesh.color = isAlfa ? e.Style.TextAlfa : e.Style.Text;
        }
    }
}
