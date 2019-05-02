using Escapa.Managers;
using Escapa.Utility;
using UnityEngine.SceneManagement;

namespace Escapa.Buttons
{
    public sealed class PlayButton : ImageButtonBase
    {
        public override void Action()
        {
            if (DifficultyManager.CurrentLevelIsCustom)
                SceneManager.LoadSceneAsync((int) GameScenes.Setup, LoadSceneMode.Single);
            else
                SceneManager.LoadSceneAsync((int) GameScenes.Game, LoadSceneMode.Single);
        }
    }
}
