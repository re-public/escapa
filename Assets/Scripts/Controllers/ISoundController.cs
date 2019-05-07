using Escapa.Events;

namespace Escapa.Controllers
{
    public interface ISoundController
    {
        event SoundEvent MuteChanged;
    }
}