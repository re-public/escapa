using System;

namespace Escapa.Events
{
    public delegate void SoundEvent(SoundEventArgs e);

    public sealed class SoundEventArgs : EventArgs
    {
        public bool IsMuted { get; }

        public SoundEventArgs(bool isMuted)
        {
            IsMuted = isMuted;
        }
    }
}