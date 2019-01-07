using Escapa.Events;

namespace Escapa.Units
{
    public interface IPlayer
    {
        event PlayerEvent Die;
        event PlayerEvent MousePressed;
    }
}
