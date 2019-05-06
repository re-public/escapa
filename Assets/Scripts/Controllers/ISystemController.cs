using Escapa.Events;

namespace Escapa.Controllers
{
    public interface ISystemController
    {
        event SystemEvent MuteChanged;
        event SystemEvent SceneLoaded;
    }
}