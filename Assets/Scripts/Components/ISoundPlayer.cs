using Escapa.Events;

namespace Escapa.Components
{
    public interface ISoundPlayer
    {
        event SystemEvent MuteChanged;
    }
}