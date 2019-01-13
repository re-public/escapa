using Escapa.Utility;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class PreloadController : MonoBehaviour, ISceneController
    {
        public void PrepareScene() => _systemController.GoToScene(GameScenes.Menu);

        public void StyleScene() { }

        private ISystemController _systemController;

        private void Awake() => _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
    }
}
