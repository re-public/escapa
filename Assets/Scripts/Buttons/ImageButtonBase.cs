using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public abstract class ImageButtonBase : MonoBehaviour
    {
        protected Image Image;

        protected void Awake()
        {
            Image = GetComponent<Image>();            
        }

        protected void OnEnable()
        {
            StyleManager.StyleChanged += OnStyleChanged;
        }

        protected void Start()
        {
            Image.color = StyleManager.CurrentTheme.Text;
        }

        protected void OnDisable()
        {
            StyleManager.StyleChanged -= OnStyleChanged;
        }


        protected void OnStyleChanged(Theme theme)
        {
            Image.color = theme.Text;
        }
        
        public abstract void Action();
    }
}