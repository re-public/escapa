using Escapa.Utility;

namespace Escapa.Core.Controllers
{
    public class EndGuiController : GuiControllerBase
    {
        public void Restart() => LoadScene(GameScenes.Game);

        public void GoToMenu() => LoadScene(GameScenes.Menu);
    }
}
