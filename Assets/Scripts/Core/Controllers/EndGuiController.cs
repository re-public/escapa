using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Core.Controllers
{
    public class EndGuiController : MonoBehaviour
    {
        public void GoToScene(string scene) => SceneManager.LoadScene(scene);
    }
}
