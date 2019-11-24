using Escapa.Utility;
using System;

namespace Escapa.Core.Events
{
    public delegate void DifficultyEvent(object sender, DifficultyEventArgs e);

    public sealed class DifficultyEventArgs : EventArgs
    {
        public Level Level { get; private set; }

        public DifficultyEventArgs(Level level)
        {
            Level = level;
        }
    }
}
