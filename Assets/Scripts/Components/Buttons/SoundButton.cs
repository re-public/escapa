using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Components.Buttons
{
    public sealed class SoundButton : ImageButtonBase
    {
        public Sprite spriteOn;
        public Sprite spriteOff;

        private ISoundController soundController;

        public override void Action()
        {
            soundController.Mute();
            Image.overrideSprite = soundController.IsMuted ? spriteOff : spriteOn;
        }

        private new void Awake()
        {
            base.Awake();
            soundController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISoundController>();
        }

        private new void Start()
        {
            base.Start();
            Image.overrideSprite = soundController.IsMuted ? spriteOff : spriteOn;
        }
    }
}
