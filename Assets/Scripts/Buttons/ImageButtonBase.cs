using Escapa.Core.Managers;
using Escapa.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public abstract class ImageButtonBase : MonoBehaviour, IButton
    {
        public event ButtonEvent ButtonClicked;
        
        protected Image Image;

        protected void Awake()
        {
            Image = GetComponent<Image>();
        }

        private void Start()
        {
            Image.color = StyleManager.Current.Text;
        }

        public virtual void Action()
        {
            ButtonClicked?.Invoke();
        }
    }
}