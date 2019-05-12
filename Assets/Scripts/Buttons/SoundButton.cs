using Escapa.Core.Interfaces;
using Escapa.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Buttons
{
    public sealed class SoundButton : ImageButtonBase
    {
        public Sprite spriteOn;
        public Sprite spriteOff;

        private ISoundController _soundController;

        public override void Action()
        {
            _soundController.Mute();
        }

        private new void Awake()
        {
            base.Awake();
            _soundController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISoundController>();
        }

        private new void OnEnable()
        {
            base.OnEnable();
            _soundController.MuteChanged += OnMuteChanged;
        }

        private new void OnDisable()
        {
            base.OnDisable();
            _soundController.MuteChanged -= OnMuteChanged;
        }

        private void OnMuteChanged(SoundEventArgs e)
        {
            Image.overrideSprite = e.IsMuted ? spriteOff : spriteOn;
        }
    }
}
