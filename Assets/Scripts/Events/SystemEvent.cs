using System;
using Escapa.Utility;

namespace Escapa.Events
{
    public delegate void SystemEvent(SystemEventArgs e);
    
    public sealed class SystemEventArgs : EventArgs
    {
        public GameScenes Scene { get; }

        public SystemEventArgs(GameScenes scene)
        {
            Scene = scene;
        }
    }
}