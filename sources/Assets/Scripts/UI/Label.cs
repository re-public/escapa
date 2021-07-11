using Escapa.Core.Managers;
using Escapa.Utility;
using System;
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

        public void SetText(string text) => textMesh.SetText(text);

        private void Awake() => textMesh = GetComponent<TextMeshProUGUI>();

        private void OnEnable() => DifficultyManager.DifficultyChanged += OnDifficultyChanged;

        private void Start()
        {
            textMesh.color = isAlfa ? StyleManager.Colors.TextAlfa : StyleManager.Colors.Text;
            if(token != LanguageTokens.None)
                textMesh.SetText(LanguageManager.GetString(token));
        }

        private void OnDisable() => DifficultyManager.DifficultyChanged -= OnDifficultyChanged;

        private void OnDifficultyChanged(object sender, EventArgs e) => textMesh.color = StyleManager.Colors.Text;
    }
}
