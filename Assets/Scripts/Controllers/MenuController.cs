using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class MenuController : MonoBehaviour
    {
        private TextMeshProUGUI _logoText;

        private void Awake()
        {
            _logoText = GameObject.FindWithTag(Tags.LogoText).GetComponent<TextMeshProUGUI>();
            StyleManager.StyleChanged += OnStyleChanged;
        }

        private void Start()
        {
            _logoText.SetText(LanguageManager.Language.Logo);
            _logoText.color = StyleManager.CurrentTheme.Text;
            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                Application.Quit();
        }

        private void OnStyleChanged(Theme theme)
        {
            _logoText.color = StyleManager.CurrentTheme.Text;
            Camera.main.backgroundColor = theme.Background;
        }
    }
}
