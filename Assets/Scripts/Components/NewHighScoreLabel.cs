using Escapa.Core.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.Components
{
    public sealed class NewHighScoreLabel : MonoBehaviour
    {
        private TextMeshProUGUI _textMesh;

        private void Awake() => _textMesh = GetComponent<TextMeshProUGUI>();
        
        private void Start()
        {
            gameObject.SetActive(ScoreManager.IsHighScore);
            _textMesh.SetText(LanguageManager.GetString(LanguageTokens.NewHighScore));
            _textMesh.color = StyleManager.Current.text;
        }
    }
}