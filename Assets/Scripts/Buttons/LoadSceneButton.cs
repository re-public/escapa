using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Buttons
{
    public sealed class LoadSceneButton : ImageButtonBase
    {
        public bool isBackButton;
        public GameScenes scene;

        public override void Action()
        {
            SceneManager.LoadSceneAsync((int) scene, LoadSceneMode.Single);
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
