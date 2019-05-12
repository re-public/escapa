using Escapa.Core.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.Components
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class Label : MonoBehaviour
    {
        public LanguageTokens token;
        
        private TextMeshProUGUI _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            DifficultyManager.DifficultyChanged += OnDifficultyChanged;
        }

        private void Start()
        {
            _textMesh.SetText(LanguageManager.GetString(token));
            _textMesh.color = StyleManager.Current.text;
        }

        private void OnDisable()
        {
            DifficultyManager.DifficultyChanged -= OnDifficultyChanged;
        }

        private void OnDifficultyChanged()
        {
            _textMesh.color = StyleManager.Current.text;
        }
    }
}