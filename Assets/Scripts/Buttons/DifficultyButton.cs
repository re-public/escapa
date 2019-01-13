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
            _sceneController.StyleScene();
        }

        private TextMeshProUGUI _buttonText;
        private ISceneController _sceneController;
        private ISystemController _systemController;

        private void Awake()
        {
            _buttonText = GetComponent<TextMeshProUGUI>();
            _sceneController = GameObject.FindWithTag(Tags.SceneController).GetComponent<ISceneController>();
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
        }
    }
}
