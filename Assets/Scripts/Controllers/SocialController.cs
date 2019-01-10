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
            var highScoreTitle = GameObject.FindWithTag(Tags.HighScoreTitleText).GetComponent<TextMeshProUGUI>();
            highScoreTitle.color = StyleManager.CurrentTheme.Text;
            highScoreTitle.text = LanguageManager.Language.HighScoreTitle;

            var highScoreText = GameObject.FindWithTag(Tags.HighScoreText).GetComponent<TextMeshProUGUI>();
            highScoreText.color = StyleManager.CurrentTheme.Text;
            highScoreText.text = ScoreManager.CurrentTop.ToString();
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
