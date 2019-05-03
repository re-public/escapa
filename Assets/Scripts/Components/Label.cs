using Escapa.Controllers;
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
        private IStyleController _styleController;
        
        protected TextMeshProUGUI TextMesh;

        protected void Awake()
        {
            TextMesh = GetComponent<TextMeshProUGUI>();
            _styleController = GameObject.FindWithTag(Tags.SystemController).GetComponent<IStyleController>();
        }

        protected void OnEnable()
        {
            if (!disableStyling)
            {
                _styleController.StyleChanged += OnStyleChanged;
            }
        }

        protected void Start()
        {
            if (!disableTranslating)
            {
                TextMesh.SetText(LanguageManager.GetString(token));
            }
        }

        protected void OnDisable()
        {
            if (!disableStyling)
            {
                _styleController.StyleChanged -= OnStyleChanged;
            }
        }

        private void OnStyleChanged(Theme theme)
        {
            TextMesh.color = isAlfa ? theme.TextAlfa : theme.Text;
        }
    }
}