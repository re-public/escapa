using Escapa.Managers;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class InfoController : MonoBehaviour
    {
        private void Start()
        {
            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;
        }
    }
}
