using Escapa.Core.Managers;
using TMPro;
using UnityEngine;

namespace Escapa.Components
{
    public sealed class TimeCounterLabel : MonoBehaviour
    {
        private TextMeshProUGUI _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _textMesh.color = StyleManager.Current.textAlfa;
        }

        private void FixedUpdate()
        {
            _textMesh.SetText(ScoreManager.CurrentTime.ToString("0.00"));
        }
    }
}