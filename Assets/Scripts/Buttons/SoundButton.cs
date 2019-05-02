using Escapa.Controllers;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Buttons
{
    public sealed class SoundButton : ImageButtonBase
    {
        public Sprite spriteOn;
        public Sprite spriteOff;

        public override void Action()
        {
            _systemController.IsSoundEnabled = !_systemController.IsSoundEnabled;
            Image.sprite = _systemController.IsSoundEnabled ? spriteOn : spriteOff;
            Image.color = StyleManager.CurrentTheme.Text;
        }

        private ISystemController _systemController;

        private new void Awake()
        {
            base.Awake();
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
        }

        private new void Start()
        {
            base.Start();
            Image.sprite = _systemController.IsSoundEnabled ? spriteOn : spriteOff;
        }
    }
}
