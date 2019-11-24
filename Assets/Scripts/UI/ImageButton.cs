using Escapa.Core.Events;
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
        private IDifficultyController _difficultyController;
        private IStyleController _style;

        private void Awake()
        {
            image = GetComponent<Image>();
            _difficultyController = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
            _style = GameObject.FindWithTag(Tags.StyleController).GetComponent<IStyleController>();
        }

        private void OnEnable() => _difficultyController.Changed += OnDifficultyChanged;

        private void Start()
        {
            if (isSocialButton)
                gameObject.SetActive(Social.localUser.authenticated);
            image.color = _style.Current.Text;
        }

        private void OnDisable() => _difficultyController.Changed -= OnDifficultyChanged;

        private void OnDifficultyChanged(object sender, DifficultyEventArgs e)
        {
            image.color = _style.Current.Text;
        }
    }
}
