using Escapa.Events;

namespace Escapa.Core.Interfaces
{
    public interface ISoundController
    {
        event SoundEvent MuteChanged;

        void Mute();
    }
}