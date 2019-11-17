using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
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

        public void SetText(string text) => textMesh.SetText(text);

        private void Awake()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
            _difficulty = GameObject.FindWithTag(Tags.SystemController).GetComponent<IDifficultyController>();
        }

        private void OnEnable() => _difficulty.Changed += OnDifficultyChanged;

        private void Start()
        {
            textMesh.color = isAlfa
                ? StyleManager.Colors[(int)_difficulty.Current.Difficulty].TextAlfa
                : StyleManager.Colors[(int)_difficulty.Current.Difficulty].Text;
            if(token != LanguageTokens.None)
                textMesh.SetText(LanguageManager.GetString(token));
        }

        private void OnDisable() => _difficulty.Changed -= OnDifficultyChanged;

        private void OnDifficultyChanged() => textMesh.color = StyleManager.Colors[(int)_difficulty.Current.Difficulty].Text;
    }
}
