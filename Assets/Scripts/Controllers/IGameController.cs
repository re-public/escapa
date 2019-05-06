using Escapa.Events;

namespace Escapa.Controllers
{
    public interface IGameController
    {
        event GameEvent GameInitialized;
        event GameEvent GameStarted;
        event GameEvent GameEnded;
    }
}