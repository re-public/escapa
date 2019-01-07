using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Controllers
{
    public sealed class InfoController : MonoBehaviour, ISceneController
    {
        public void PrepareScene()
        {
            var gameCreditText = GameObject.FindWithTag(Tags.GameCreditText).GetComponent<TextMeshProUGUI>();
            gameCreditText.color = StyleManager.CurrentTheme.Text;
            gameCreditText.text = LanguageManager.Language.GameCredit;

            var gameCopyrightText = GameObject.FindWithTag(Tags.GameCopyrightText).GetComponent<TextMeshProUGUI>();
            gameCopyrightText.color = StyleManager.CurrentTheme.Text;
            gameCopyrightText.text = LanguageManager.Language.GameCopyright;

            var gameLinkText = GameObject.FindWithTag(Tags.GameLinkText).GetComponent<TextMeshProUGUI>();
            gameLinkText.color = StyleManager.CurrentTheme.Text;
            gameLinkText.text = LanguageManager.Language.GameLink;

            var soundCreditText = GameObject.FindWithTag(Tags.SoundCreditText).GetComponent<TextMeshProUGUI>();
            soundCreditText.color = StyleManager.CurrentTheme.Text;
            soundCreditText.text = LanguageManager.Language.SoundCredit;

            var soundCopyrightText = GameObject.FindWithTag(Tags.SoundCopyrightText).GetComponent<TextMeshProUGUI>();
            soundCopyrightText.color = StyleManager.CurrentTheme.Text;
            soundCopyrightText.text = LanguageManager.Language.SoundCopyright;

            var soundLinkText = GameObject.FindWithTag(Tags.SoundLinkText).GetComponent<TextMeshProUGUI>();
            soundLinkText.color = StyleManager.CurrentTheme.Text;
            soundLinkText.text = LanguageManager.Language.SoundLink;

            GameObject.FindWithTag(Tags.BackButton).GetComponent<Image>().color = StyleManager.CurrentTheme.Text;
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
