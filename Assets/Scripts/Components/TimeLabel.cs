using Escapa.Core.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.Components
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class TimeLabel : MonoBehaviour
    {
        private TextMeshProUGUI _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _textMesh.SetText(LanguageManager.GetString(LanguageTokens.Time) + ScoreManager.LastTime.ToString("0.000"));
            _textMesh.color = StyleManager.Current.Text;
        }
    }
}