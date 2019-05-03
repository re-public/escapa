using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.Components
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Label : MonoBehaviour
    {
        public bool disableStyling;
        public bool disableTranslating;
        public bool isAlfa;
        public LanguageTokens token;
        
        protected TextMeshProUGUI TextMesh;

        protected void Awake()
        {
            TextMesh = GetComponent<TextMeshProUGUI>();
        }

        protected void OnEnable()
        {
            if (!disableStyling)
            {
                StyleManager.StyleChanged += OnStyleChanged;
            }
        }

        protected void Start()
        {
            if (!disableTranslating)
            {
                TextMesh.SetText(LanguageManager.GetString(token));
            }

            if (!disableStyling)
            {
                TextMesh.color = isAlfa ? StyleManager.CurrentTheme.TextAlfa : StyleManager.CurrentTheme.Text;
            }
        }

        protected void OnDisable()
        {
            if (!disableStyling)
            {
                StyleManager.StyleChanged -= OnStyleChanged;
            }
        }

        private void OnStyleChanged(Theme theme)
        {
            TextMesh.color = isAlfa ? theme.TextAlfa : theme.Text;
        }
    }
}