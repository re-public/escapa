using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Core.Controllers
{
    public sealed class SocialGuiController : MonoBehaviour
    {
        public void GoToMenu() => SceneManager.LoadScene((int)GameScenes.Menu);

        public void ShowAchievements() => SocialManager.ShowAchievements();

        public void ShowLeaderboards() => SocialManager.ShowLeaderboards();
    }
}
