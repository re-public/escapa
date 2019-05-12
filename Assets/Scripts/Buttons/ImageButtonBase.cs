using Escapa.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public abstract class ImageButtonBase : MonoBehaviour
    {
        public abstract void Action();
        
        protected Image Image;

        protected void Awake()
        {
            Image = GetComponent<Image>();
        }

        protected void OnEnable()
        {
            DifficultyManager.DifficultyChanged += OnDifficultyChanged;
        }

        protected void Start()
        {
            Image.color = StyleManager.Current.text;
        }

        protected void OnDisable()
        {
            DifficultyManager.DifficultyChanged -= OnDifficultyChanged;
        }

        private void OnDifficultyChanged()
        {
            Image.color = StyleManager.Current.text;
        }
    }
}