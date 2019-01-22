using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class PreloadController : MonoBehaviour, ISceneController
    {
        public void PrepareScene() => SocialManager.Auth(OnGooglePlayLogin);

        public void StyleScene()
        {
            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;

            _loadingText.color = StyleManager.CurrentTheme.Text;
        }

        private ISystemController _systemController;
        private TextMeshProUGUI _loadingText;

        private void Awake()
        {
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();
            _loadingText = GameObject.FindWithTag(Tags.LoadingText).GetComponent<TextMeshProUGUI>();
        }

        private void OnGooglePlayLogin() => _systemController.GoToScene(GameScenes.Menu);
    }
}
