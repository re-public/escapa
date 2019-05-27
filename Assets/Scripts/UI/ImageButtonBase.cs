using Escapa.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.UI
{
    [RequireComponent(typeof(Image))]
    public abstract class ImageButtonBase : ButtonBase
    {
        protected Image Image;

        protected void Awake() => Image = GetComponent<Image>();

        protected void OnEnable() => DifficultyManager.DifficultyChanged += OnDifficultyChanged;

        protected void Start() => Image.color = StyleManager.Colors.Text;

        protected void OnDisable() => DifficultyManager.DifficultyChanged -= OnDifficultyChanged;

        private void OnDifficultyChanged() => Image.color = StyleManager.Colors.Text;
    }
}