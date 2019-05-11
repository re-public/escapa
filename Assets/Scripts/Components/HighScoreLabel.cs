using Escapa.Core.Managers;
using TMPro;
using UnityEngine;

namespace Escapa.Components
{
    public sealed class HighScoreLabel : MonoBehaviour
    {
        private TextMeshProUGUI _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _textMesh.SetText(ScoreManager.CurrentHigh.ToString("0.000"));
            _textMesh.color = StyleManager.Current.Text;
        }
    }
}