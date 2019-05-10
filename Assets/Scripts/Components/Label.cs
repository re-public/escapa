using Escapa.Controllers;
using Escapa.Events;
using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.Components
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class Label : MonoBehaviour
    {
        public LanguageTokens token;

        private IStyleController _styleController;
        private TextMeshProUGUI _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            _styleController = GameObject.FindWithTag(Tags.GameController).GetComponent<IStyleController>();
        }

        private void OnEnable()
        {
            _styleController.StyleChanged += OnStyleChanged;
        }

        private void Start()
        {
            _textMesh.SetText(LanguageManager.GetString(token));
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