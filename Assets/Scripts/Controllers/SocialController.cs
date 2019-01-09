using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.Controllers
{
    public sealed class SocialController : MonoBehaviour, ISceneController
    {
        public void PrepareScene()
        {
            var rows = GameObject.FindWithTag(Tags.Canvas).GetComponentsInChildren<TextMeshProUGUI>();

            rows[0].text = LanguageManager.Language.Leaderboard;
            rows[0].color = StyleManager.CurrentTheme.Text;

            for (var i = 1; i < 6;  i++)
            {
                rows[i].text = string.Format("{0}: {1}", i, ScoreManager.Records[i - 1].ToString("0.00"));
                rows[i].color = StyleManager.CurrentTheme.Text;
            }
        }

        private ISystemController _systemController;

        private void Awake() => _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                _systemController.GoToScene(GameScenes.Menu);
        }
    }
}
