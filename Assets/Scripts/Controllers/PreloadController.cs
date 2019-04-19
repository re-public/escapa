using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class PreloadController : MonoBehaviour
    {
        private ISystemController _systemController;

        private void Awake() => _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();

        private void Start()
        {
            SocialManager.Auth(OnGooglePlayLogin);
            Camera.main.backgroundColor = Color.black;
        }

        private void OnGooglePlayLogin() => _systemController.GoToScene(GameScenes.Menu);
    }
}
