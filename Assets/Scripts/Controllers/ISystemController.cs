using Escapa.Utility;

namespace Escapa.Controllers
{
    public interface ISystemController
    {
        bool IsSoundEnabled { get; set; }

        void GoToScene(GameScenes scene);
    }
}
