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
        private IDifficultyController _difficultyController;
        private IStyleController _style;
        private ITranslationController _translation;

        private void Awake()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
            _difficultyController = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
            _style = GameObject.FindWithTag(Tags.StyleController).GetComponent<IStyleController>();
            _translation = GameObject.FindWithTag(Tags.TranslationController).GetComponent<ITranslationController>();
        }

        private void OnEnable() => _difficultyController.Changed += OnDifficultyChanged;

        private void Start()
        {
            textMesh.color = isAlfa ? _style.Current.TextAlfa : _style.Current.Text;
            if(Token != LanguageTokens.None)
                textMesh.SetText(_translation.Current.GetString(Token));
        }

        private void OnDisable() => _difficultyController.Changed -= OnDifficultyChanged;

        private void OnDifficultyChanged(object sender, DifficultyEventArgs e)
        {
            textMesh.color = _style.Current.Text;
        }
    }
}
