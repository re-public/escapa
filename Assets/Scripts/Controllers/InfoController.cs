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
            _gameCreditText.text = LanguageManager.Language.GameCredit;
            _gameCopyrightText.text = LanguageManager.Language.GameCopyright;
            _gameLinkText.text = LanguageManager.Language.GameLink;
            _soundCreditText.text = LanguageManager.Language.SoundCredit;
            _soundCopyrightText.text = LanguageManager.Language.SoundCopyright;
            _soundLinkText.text = LanguageManager.Language.SoundLink;
        }

        public void StyleScene()
        {
            _gameCreditText.color = StyleManager.CurrentTheme.Text;
            _gameCopyrightText.color = StyleManager.CurrentTheme.Text;
            _gameLinkText.color = StyleManager.CurrentTheme.Text;
            _soundCreditText.color = StyleManager.CurrentTheme.Text;
            _soundCopyrightText.color = StyleManager.CurrentTheme.Text;
            _soundLinkText.color = StyleManager.CurrentTheme.Text;
            _backButton.color = StyleManager.CurrentTheme.Text;
        }

        private ISystemController _systemController;
        #region UI elements
        private TextMeshProUGUI _gameCreditText;
        private TextMeshProUGUI _gameCopyrightText;
        private TextMeshProUGUI _gameLinkText;
        private TextMeshProUGUI _soundCreditText;
        private TextMeshProUGUI _soundCopyrightText;
        private TextMeshProUGUI _soundLinkText;
        private Image _backButton;
        #endregion

        private void Awake()
        {
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();

            _gameCreditText = GameObject.FindWithTag(Tags.GameCreditText).GetComponent<TextMeshProUGUI>();
            _gameCopyrightText = GameObject.FindWithTag(Tags.GameCopyrightText).GetComponent<TextMeshProUGUI>();
            _gameLinkText = GameObject.FindWithTag(Tags.GameLinkText).GetComponent<TextMeshProUGUI>();
            _soundCreditText = GameObject.FindWithTag(Tags.SoundCreditText).GetComponent<TextMeshProUGUI>();
            _soundCopyrightText = GameObject.FindWithTag(Tags.SoundCopyrightText).GetComponent<TextMeshProUGUI>();
            _soundLinkText = GameObject.FindWithTag(Tags.SoundLinkText).GetComponent<TextMeshProUGUI>();
            _backButton = GameObject.FindWithTag(Tags.BackButton).GetComponent<Image>();
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                _systemController.GoToScene(GameScenes.Menu);
        }
    }
}
