using Escapa.Controllers;
using Escapa.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Buttons
{
    public sealed class SoundButton : ImageButtonBase
    {
        public Sprite spriteOn;
        public Sprite spriteOff;

        private ISystemController _systemController;

        private new void Awake()
        {
            base.Awake();
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
        }

        private new void OnEnable()
        {
            base.OnEnable();
            _systemController.MuteChanged += OnMuteChanged;
            _systemController.SceneLoaded += OnMuteChanged;
        }

        private new void OnDisable()
        {
            base.OnDisable();
            _systemController.MuteChanged -= OnMuteChanged;
            _systemController.SceneLoaded -= OnMuteChanged;
        }

        private void OnMuteChanged(SystemEventArgs e)
        {
            Image.sprite = e.IsSoundMuted ? spriteOff : spriteOn;
        }
    }
}
