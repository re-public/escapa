using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Core.Controllers
{
    public class InfoGuiController : MonoBehaviour
    {
        public void OpenLink(string url) => Application.OpenURL(url);

        public void GoToMenu() => SceneManager.LoadScene((int)GameScenes.Menu);
    }
}
