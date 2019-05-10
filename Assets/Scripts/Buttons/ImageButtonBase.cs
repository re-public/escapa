using Escapa.Controllers;
using Escapa.Events;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public abstract class ImageButtonBase : MonoBehaviour, IButton
    {
        public event ButtonEvent ButtonClicked;
        
        protected Image Image;
        
        private IStyleController _styleController;

        protected void Awake()
        {
            Image = GetComponent<Image>();          
            _styleController = GameObject.FindWithTag(Tags.GameController).GetComponent<IStyleController>();
        }

        protected void OnEnable()
        {
            _styleController.StyleChanged += OnStyleChanged;
        }

        protected void OnDisable()
        {
            _styleController.StyleChanged -= OnStyleChanged;
        }


        private void OnStyleChanged(StyleEventArgs e)
        {
            Image.color = e.Theme.Text;
        }

        public virtual void Action()
        {
            ButtonClicked?.Invoke();
        }
    }
}