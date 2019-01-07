using Escapa.Events;
using Escapa.Utility;

namespace Escapa.Controllers
{
    public interface ISystemController
    {
        event SystemEvent SceneLoaded;
        event SystemEvent SceneUnloaded;

        void GoToScene(GameScenes scene);
    }
}
