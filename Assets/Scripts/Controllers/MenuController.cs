using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Controllers
{
    public sealed class MenuController : MonoBehaviour, ISceneController
    {
        public void PrepareScene()
        {
            _difficultyText.text = LanguageManager.Language.Difficulties[DifficultyManager.Level];
            _logoText.text = LanguageManager.Language.Logo;
        }

        public void StyleScene()
        {
            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;
            _difficultyText.color = StyleManager.CurrentTheme.Text;
            _logoText.color = StyleManager.CurrentTheme.Text;
            _infoButton.color = StyleManager.CurrentTheme.Text;
            _playButton.color = StyleManager.CurrentTheme.Text;
            _socialButton.color = StyleManager.CurrentTheme.Text;
            _soundButton.color = StyleManager.CurrentTheme.Text;
        }

        #region UI elements
        private TextMeshProUGUI _difficultyText;
        private TextMeshProUGUI _logoText;
        private Image _infoButton;
        private Image _playButton;
        private Image _socialButton;
        private Image _soundButton;
        #endregion

        private void Awake()
        {
            _difficultyText = GameObject.FindWithTag(Tags.DifficultyText).GetComponent<TextMeshProUGUI>();
            _logoText = GameObject.FindWithTag(Tags.LogoText).GetComponent<TextMeshProUGUI>();
            _infoButton = GameObject.FindWithTag(Tags.InfoButton).GetComponent<Image>();
            _playButton = GameObject.FindWithTag(Tags.PlayButton).GetComponent<Image>();
            _socialButton = GameObject.FindWithTag(Tags.SocialButton).GetComponent<Image>();
            _soundButton = GameObject.FindWithTag(Tags.SoundButton).GetComponent<Image>();
    }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                Application.Quit();
        }
    }
}
