using Escapa.Events;

namespace Escapa.Units
{
    public interface IPlayer
    {
        event PlayerEvent Died;
        event PlayerEvent Moved;
        event PlayerEvent Pressed;
        event PlayerEvent Stopped;
    }
}
