using Escapa.Utility;
using UnityEngine.SceneManagement;

namespace Escapa.Components.Buttons
{
    public sealed class LoadSceneButton : ImageButtonBase
    {
        public GameScenes scene;

        public override void Action() => SceneManager.LoadSceneAsync((int) scene, LoadSceneMode.Single);
    }
}
