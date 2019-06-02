using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Core.Controllers
{
    public abstract class GuiControllerBase : MonoBehaviour
    {
        protected void LoadScene(GameScenes scene) => SceneManager.LoadScene((int)scene);
    }
}
