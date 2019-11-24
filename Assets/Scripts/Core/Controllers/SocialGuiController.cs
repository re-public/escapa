using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    public sealed class SocialGuiController : GuiControllerBase
    {
        public void GoToMenu() => LoadScene(GameScenes.Menu);

        public void ShowAchievements() => _social.ShowAchievements();

        public void ShowLeaderboards() => _social.ShowLeaderboards();

        private ISocialController _social;

        private void Awake()
        {
            _social = GameObject.FindWithTag(Tags.SocialController).GetComponent<ISocialController>();
        }
    }
}
