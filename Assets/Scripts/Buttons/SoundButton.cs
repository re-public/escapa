using Escapa.Core.Interfaces;
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
            Image.overrideSprite = _soundController.IsMuted ? spriteOff : spriteOn;
        }

        private new void Awake()
        {
            base.Awake();
            _soundController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISoundController>();
        }

        private new void Start()
        {
            base.Start();
            Image.overrideSprite = _soundController.IsMuted ? spriteOff : spriteOn;
        }
    }
}
