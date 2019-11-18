using Escapa.Core.Interfaces;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class Label : MonoBehaviour
    {
        [SerializeField]
        private bool isAlfa;
        [SerializeField]
        private LanguageTokens token;

        private TextMeshProUGUI textMesh;
        private IDifficultyController _difficulty;
        private IStyleController _style;
        private ITranslationController _translation;

        public void SetText(string text) => textMesh.SetText(text);

        private void Awake()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
            _difficulty = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
            _style = GameObject.FindWithTag(Tags.StyleController).GetComponent<IStyleController>();
            _translation = GameObject.FindWithTag(Tags.TranslationController).GetComponent<ITranslationController>();
        }

        private void OnEnable() => _difficulty.Changed += OnDifficultyChanged;

        private void Start()
        {
            textMesh.color = isAlfa ? _style.Current.TextAlfa : _style.Current.Text;
            if(token != LanguageTokens.None)
                textMesh.SetText(_translation.Current.GetString(token));
        }

        private void OnDisable() => _difficulty.Changed -= OnDifficultyChanged;

        private void OnDifficultyChanged() => textMesh.color = _style.Current.Text;
    }
}
