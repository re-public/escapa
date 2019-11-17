using Escapa.Core.Interfaces;
using Escapa.Utility;
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
        private IDifficultyController _difficulty;
        private IStyleController _style;

        private void Awake()
        {
            image = GetComponent<Image>();
            _difficulty = GameObject.FindWithTag(Tags.SystemController).GetComponent<IDifficultyController>();
            _style = GameObject.FindWithTag(Tags.SystemController).GetComponent<IStyleController>();
        }

        private void OnEnable() => _difficulty.Changed += OnDifficultyChanged;

        private void Start()
        {
            if (isSocialButton)
                gameObject.SetActive(Social.localUser.authenticated);
            image.color = _style.Current.Text;
        }

        private void OnDisable() => _difficulty.Changed -= OnDifficultyChanged;

        private void OnDifficultyChanged() => image.color = _style.Current.Text;
    }
}
