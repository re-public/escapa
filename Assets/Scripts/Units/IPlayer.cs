using Escapa.Events;

namespace Escapa.Units
{
    public interface IPlayer
    {
        event GameEvent Died;
        event GameEvent Moved;
        event GameEvent Pressed;
        event GameEvent Stopped;
    }
}
