using Escapa.Controllers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Button))]
    public sealed class LoadSceneButton : MonoBehaviour, IButton
    {
        public GameScenes Scene;

        public void Action() => _systemController.GoToScene(Scene);

        private ISystemController _systemController;

        private void Awake() => _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
    }
}
