using Escapa.Events;

namespace Escapa.Core.Interfaces
{
    public interface IGameController
    {
        event GameEvent GameStarted;
    }
}