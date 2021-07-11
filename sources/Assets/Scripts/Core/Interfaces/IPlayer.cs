using System;

namespace Escapa.Core.Interfaces
{
    public interface IPlayer
    {
        event EventHandler Died;
        event EventHandler Moved;
        event EventHandler Pressed;
        event EventHandler Stopped;
    }
}
