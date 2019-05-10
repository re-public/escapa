using Escapa.Controllers;
using Escapa.Events;
using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.Components
{
    public sealed class HighScoreLabel : MonoBehaviour
    {
        private IStyleController _styleController;
        private TextMeshProUGUI _textMesh;

        private void Awake()
        {
            _styleController = GameObject.FindWithTag(Tags.GameController).GetComponent<IStyleController>();
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _styleController.StyleChanged += OnStyleChanged;
        }
        
        private void Start()
        {
            _textMesh.SetText(ScoreManager.CurrentTop.ToString("0.000"));
        }

        private void OnDisable()
        {
            _styleController.StyleChanged -= OnStyleChanged;
        }

        private void OnStyleChanged(StyleEventArgs e)
        {
            _textMesh.color = e.Theme.Text;
        }
    }
}