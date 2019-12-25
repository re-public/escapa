using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    public sealed class SocialGuiController : GuiControllerBase
    {
        public void GoToMenu() => LoadScene(GameScenes.Menu);

        public void ShowAchievements() => _socialController.ShowAchievements();

        public void ShowLeaderboards() => _socialController.ShowLeaderboards();

        private ISocialController _socialController;

        private void Awake()
        {
            _socialController = GameObject.FindWithTag(Tags.SocialController).GetComponent<ISocialController>();
        }
    }
}
