using Escapa.Controllers;
using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Button), typeof(TextMeshProUGUI))]
    public sealed class DifficultyButton : MonoBehaviour, IButton
    {
        public void Action()
        {
            DifficultyManager.AddLevel();

            _buttonText.text = LanguageManager.Language.Difficulties[DifficultyManager.Level];
        }

        private TextMeshProUGUI _buttonText;
        private ISystemController _systemController;

        private void Awake()
        {
            _buttonText = GetComponent<TextMeshProUGUI>();
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
        }
    }
}
