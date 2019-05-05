using Escapa.Utility;
using UnityEngine.SceneManagement;

namespace Escapa.Buttons
{
    public sealed class PlayButton : ImageButtonBase
    {
        public override void Action()
        {
            SceneManager.LoadSceneAsync((int) GameScenes.Game, LoadSceneMode.Single);
        }
    }
}
