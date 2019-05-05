using Escapa.Components;
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
            _soundPlayer.IsMuted = !_soundPlayer.IsMuted;
            Image.sprite = _soundPlayer.IsMuted ? spriteOff : spriteOn;
        }

        private ISoundPlayer _soundPlayer;

        private new void Awake()
        {
            base.Awake();
            _soundPlayer = GameObject.FindWithTag(Tags.SoundPlayer).GetComponent<ISoundPlayer>();
        }

        private void Start()
        {
            Image.sprite = _soundPlayer.IsMuted ? spriteOff : spriteOn;
        }
    }
}
