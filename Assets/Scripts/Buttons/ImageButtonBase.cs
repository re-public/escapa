using Escapa.Controllers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public abstract class ImageButtonBase : MonoBehaviour
    {
        protected Image Image;
        
        private IStyleController _styleController;

        protected void Awake()
        {
            Image = GetComponent<Image>();          
            _styleController = GameObject.FindWithTag(Tags.SystemController).GetComponent<IStyleController>();
        }

        protected void OnEnable()
        {
            _styleController.StyleChanged += OnStyleChanged;
        }

        protected void OnDisable()
        {
            _styleController.StyleChanged -= OnStyleChanged;
        }


        private void OnStyleChanged(Theme theme)
        {
            Image.color = theme.Text;
        }
        
        public abstract void Action();
    }
}