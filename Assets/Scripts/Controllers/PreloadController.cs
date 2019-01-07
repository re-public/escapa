using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class PreloadController : MonoBehaviour, ISceneController
    {
        public void PrepareScene()
        {
            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;

            _systemController.GoToScene(GameScenes.Menu);
        }

        private ISystemController _systemController;

        private void Awake() => _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
    }
}
