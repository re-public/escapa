using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Controllers
{
    public sealed class PreloadController : MonoBehaviour
    {
        private void Start()
        {
            SocialManager.Auth(OnGooglePlayLogin);
        }

        private void OnGooglePlayLogin() => SceneManager.LoadSceneAsync((int) GameScenes.Menu, LoadSceneMode.Single);
    }
}
