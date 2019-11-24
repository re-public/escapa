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

        private Image _image;
        private IStyleController _styleController;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _styleController = GameObject.FindWithTag(Tags.StyleController).GetComponent<IStyleController>();
        }

        private void OnEnable()
        {
            _styleController.Changed += OnStyleChanged;
        }

        private void Start()
        {
            if (isSocialButton)
                gameObject.SetActive(Social.localUser.authenticated);
        }

        private void OnDisable()
        {
            _styleController.Changed -= OnStyleChanged;
        }

        private void OnStyleChanged(object sender, StyleEventArgs e)
        {
            _image.color = e.Style.Text;
        }
    }
}
