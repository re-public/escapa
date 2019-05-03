using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class MenuController : MonoBehaviour
    {
        private void OnEnable()
        {
            StyleManager.StyleChanged += OnStyleChanged;
        }

        private void Start()
        {
            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                Application.Quit();
        }

        private void OnStyleChanged(Theme theme)
        {
            Camera.main.backgroundColor = theme.Background;
        }
    }
}
