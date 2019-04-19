using Escapa.Controllers;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Button))]
    public sealed class PlayButton : MonoBehaviour, IButton
    {
        public void Action()
        {
            if (DifficultyManager.CurrentLevelIsCustom)
                _systemController.GoToScene(GameScenes.Setup);
            else
                _systemController.GoToScene(GameScenes.Game);
        }

        private ISystemController _systemController;
        private Image _image;

        private void Awake()
        {
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
            _image = GetComponent<Image>();

            StyleManager.StyleChanged += OnStyleChanged;            
        }

        private void Start() => _image.color = StyleManager.CurrentTheme.Text;

        private void OnStyleChanged(Theme theme) => _image.color = theme.Text;
    }
}
