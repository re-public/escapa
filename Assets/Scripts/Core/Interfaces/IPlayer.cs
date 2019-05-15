using Escapa.Events;

namespace Escapa.Core.Interfaces
{
    public interface IPlayer
    {
        event GameEvent Died;
        event GameEvent Moved;
        event GameEvent Pressed;
        event GameEvent Stopped;
    }
}
