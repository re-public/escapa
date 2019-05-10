using Escapa.Controllers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Buttons
{
    public sealed class LoadSceneButton : ImageButtonBase
    {
        public bool isBackButton;
        public GameScenes scene;

        private ISystemController _systemController;

        public override void Action()
        {
            base.Action();
            _systemController.GoToScene(scene);
        }

        private new void Awake()
        {
            base.Awake();
            _systemController = GameController.GetComponent<ISystemController>();
        }

        private void FixedUpdate()
        {
            if (isBackButton && Input.GetKey(KeyCode.Escape))
            {
                Action();
            }
        }
    }
}
