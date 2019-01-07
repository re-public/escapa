using Escapa.Controllers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public sealed class SocialButton : MonoBehaviour, IButton
    {
        public void Action() => _systemController.GoToScene(GameScenes.Social);

        private ISystemController _systemController;

        private void Awake() => _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
    }
}
