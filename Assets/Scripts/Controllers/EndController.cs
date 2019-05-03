using Escapa.Managers;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class EndController : MonoBehaviour
    {
        private void Start()
        {
            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;
        }
    }
}
