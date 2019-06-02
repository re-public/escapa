using Escapa.Core.Managers;
using Escapa.Utility;

namespace Escapa.Core.Controllers
{
    public sealed class SocialGuiController : GuiControllerBase
    {
        public void GoToMenu() => LoadScene(GameScenes.Menu);

        public void ShowAchievements() => SocialManager.ShowAchievements();

        public void ShowLeaderboards() => SocialManager.ShowLeaderboards();
    }
}
