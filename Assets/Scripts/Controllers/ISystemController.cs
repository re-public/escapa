using Escapa.Events;
using Escapa.Utility;

namespace Escapa.Controllers
{
    public interface ISystemController
    {
        event SystemEvent SceneLoaded;
        event SystemEvent SceneUnloaded;

        bool IsSoundEnabled { get; set; }

        void GoToScene(GameScenes scene);
    }
}
