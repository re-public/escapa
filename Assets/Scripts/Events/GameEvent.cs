using System;
using Escapa.Utility;

namespace Escapa.Events
{
    public delegate void GameEvent(GameEventArgs e);
    
    public sealed class GameEventArgs : EventArgs
    {
        public float Time { get; }
        public Level Level { get; }

        public GameEventArgs(float time, Level level)
        {
            Time = time;
            Level = level;
        }
    }
}