using Escapa.Controllers;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Button))]
    public sealed class PlayButton : MonoBehaviour, IButton
    {
        public void Action()
        {
            if (DifficultyManager.Level == DifficultyManager.DifficultiesCount - 1)
                _systemController.GoToScene(GameScenes.Setup);
            else
                _systemController.GoToScene(GameScenes.Game);
        }

        private ISystemController _systemController;

        private void Awake() => _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
    }
}
