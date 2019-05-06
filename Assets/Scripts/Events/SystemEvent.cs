using System;
using Escapa.Utility;

namespace Escapa.Events
{
    public delegate void SystemEvent(SystemEventArgs e);
    
    public sealed class SystemEventArgs : EventArgs
    {
        public bool IsSoundMuted { get; }
        public GameScenes Scene { get; }

        public SystemEventArgs(bool isSoundMuted, GameScenes scene)
        {
            IsSoundMuted = isSoundMuted;
            Scene = scene;
        }
    }
}