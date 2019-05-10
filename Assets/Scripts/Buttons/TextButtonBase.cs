using Escapa.Controllers;
using Escapa.Events;
using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(TextMeshProUGUI), typeof(Button))]
    public abstract class TextButtonBase : MonoBehaviour
    {
        public bool disableTranslating;
        public LanguageTokens token;
        
        protected TextMeshProUGUI TextMesh;

        private IStyleController _styleController;
        
        protected void Awake()
        {
            TextMesh = GetComponent<TextMeshProUGUI>();
            _styleController = GameObject.FindWithTag(Tags.GameController).GetComponent<IStyleController>();
        }
        
        protected void OnEnable()
        {
            _styleController.StyleChanged += OnStyleChanged;
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
            _styleController.StyleChanged -= OnStyleChanged;
        }


        private void OnStyleChanged(StyleEventArgs e)
        {
            TextMesh.color = e.Theme.Text;
        }
        
        public abstract void Action();
    }
}