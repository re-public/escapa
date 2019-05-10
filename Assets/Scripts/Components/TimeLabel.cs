using Escapa.Controllers;
using Escapa.Events;
using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.Components
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class TimeLabel : MonoBehaviour
    {
        private IGameController _gameController;
        private IStyleController _styleController;
        private TextMeshProUGUI _textMesh;

        private void Awake()
        {
            _gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<IGameController>();
            _styleController = GameObject.FindWithTag(Tags.GameController).GetComponent<IStyleController>();
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _gameController.GameEnded += OnGameEnded;
            _styleController.StyleChanged += OnStyleChanged;
        }

        private void OnDisable()
        {
            _gameController.GameEnded -= OnGameEnded;
            _styleController.StyleChanged -= OnStyleChanged;
        }

        private void OnStyleChanged(StyleEventArgs e)
        {
            _textMesh.color = e.Theme.Text;
        }

        private void OnGameEnded(GameEventArgs e)
        {
            _textMesh.SetText(LanguageManager.GetString(LanguageTokens.Time) + e.Time.ToString("0.000"));
        }
    }
}