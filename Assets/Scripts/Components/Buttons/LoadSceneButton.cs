using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Components.Buttons
{
    public sealed class LoadSceneButton : ImageButtonBase
    {
        [SerializeField]
        private GameScenes scene;

        public override void Action() => SceneManager.LoadSceneAsync((int) scene, LoadSceneMode.Single);
    }
}
