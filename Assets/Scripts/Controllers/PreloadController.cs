using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class PreloadController : MonoBehaviour, ISceneController
    {
        public void PrepareScene() => SocialManager.Auth(OnGooglePlayLogin);

        public void StyleScene() => Camera.main.backgroundColor = Color.black;

        private ISystemController _systemController;

        private void Awake() => _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();

        private void OnGooglePlayLogin() => _systemController.GoToScene(GameScenes.Menu);
    }
}
