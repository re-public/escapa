using System;

namespace Escapa.Events
{
    public delegate void SystemEvent(SystemEventArgs e);
    
    public sealed class SystemEventArgs : EventArgs
    {
        public bool IsSoundMuted { get; }

        public SystemEventArgs(bool isSoundMuted)
        {
            IsSoundMuted = isSoundMuted;
        }
    }
}