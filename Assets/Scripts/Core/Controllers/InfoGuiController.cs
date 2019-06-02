using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    public class InfoGuiController : GuiControllerBase
    {
        public void OpenLink(string url) => Application.OpenURL(url);

        public void GoToMenu() => LoadScene(GameScenes.Menu);
    }
}
