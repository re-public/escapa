using Escapa.Components;
using Escapa.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Buttons
{
    public sealed class SoundButton : ImageButtonBase
    {
        public Sprite spriteOn;
        public Sprite spriteOff;

        private ISoundPlayer _soundPlayer;

        private new void Awake()
        {
            base.Awake();
            _soundPlayer = GameObject.FindWithTag(Tags.SoundPlayer).GetComponent<ISoundPlayer>();
        }

        private new void OnEnable()
        {
            base.OnEnable();
            _soundPlayer.MuteChanged += OnMuteChanged;
        }

        private new void OnDisable()
        {
            base.OnDisable();
            _soundPlayer.MuteChanged -= OnMuteChanged;
        }

        private void OnMuteChanged(SystemEventArgs e)
        {
            Image.sprite = e.IsSoundMuted ? spriteOff : spriteOn;
        }
    }
}
