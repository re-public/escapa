using Escapa.Core.Events;

namespace Escapa.Core.Interfaces
{
    public interface ISoundController
    {
        event GameEvent MuteChanged;

        bool IsMuted { get; }

        void Mute();
    }
}