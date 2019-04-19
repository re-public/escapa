using Escapa.Controllers;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Button))]
    public sealed class LoadSceneButton : MonoBehaviour, IButton
    {
        public GameScenes Scene;

        public void Action() => _systemController.GoToScene(Scene);

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
