using Escapa.Core.Managers;
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

        private void Start()
        {
            Image.color = StyleManager.Current.Text;
        }

        public abstract void Action();
    }
}