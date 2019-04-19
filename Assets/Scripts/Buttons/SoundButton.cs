using Escapa.Controllers;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public sealed class SoundButton : MonoBehaviour, IButton
    {
        public Sprite SpriteOn;
        public Sprite SpriteOff;

        public void Action()
        {
            _systemController.IsSoundEnabled = !_systemController.IsSoundEnabled;
            _image.sprite = _systemController.IsSoundEnabled ? SpriteOn : SpriteOff;
            _image.color = StyleManager.CurrentTheme.Text;
        }

        private ISystemController _systemController;
        private Image _image;

        private void Awake()
        {
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
            _image = GetComponent<Image>();

            StyleManager.StyleChanged += OnStyleChanged;
        }

        private void Start()
        {
            _image.sprite = _systemController.IsSoundEnabled ? SpriteOn : SpriteOff;
            _image.color = StyleManager.CurrentTheme.Text;
        }

        private void OnStyleChanged(Theme theme) => _image.color = theme.Text;
    }
}
