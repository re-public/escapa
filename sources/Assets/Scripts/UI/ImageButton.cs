using Escapa.Core.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.UI
{
    [RequireComponent(typeof(Image))]
    public sealed class ImageButton : MonoBehaviour
    {
        [SerializeField]
        private bool isSocialButton;

        private Image image;

        private void Awake() => image = GetComponent<Image>();

        private void OnEnable() => DifficultyManager.DifficultyChanged += OnDifficultyChanged;

        private void Start()
        {
            if (isSocialButton)
                gameObject.SetActive(Social.localUser.authenticated);
            image.color = StyleManager.Colors.Text;
        }

        private void OnDisable() => DifficultyManager.DifficultyChanged -= OnDifficultyChanged;

        private void OnDifficultyChanged(object sender, EventArgs e) => image.color = StyleManager.Colors.Text;
    }
}
