using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class MenuController : MonoBehaviour
    {
        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                Application.Quit();
        }
    }
}
