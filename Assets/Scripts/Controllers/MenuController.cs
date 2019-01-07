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
            var difficultyText = GameObject.FindWithTag(Tags.DifficultyText).GetComponent<TextMeshProUGUI>();
            difficultyText.text = LanguageManager.Language.Difficulties[DifficultyManager.Level];

            var logoText = GameObject.FindWithTag(Tags.LogoText).GetComponent<TextMeshProUGUI>();
            logoText.text = LanguageManager.Language.Logo;

            difficultyText.faceColor = StyleManager.CurrentTheme.Text;
            logoText.faceColor = StyleManager.CurrentTheme.Text;
            GameObject.FindWithTag(Tags.InfoButton).GetComponent<Image>().color = StyleManager.CurrentTheme.Text;
            GameObject.FindWithTag(Tags.PlayButton).GetComponent<Image>().color = StyleManager.CurrentTheme.Text;
            GameObject.FindWithTag(Tags.SocialButton).GetComponent<Image>().color = StyleManager.CurrentTheme.Text;
            GameObject.FindWithTag(Tags.SoundButton).GetComponent<Image>().color = StyleManager.CurrentTheme.Text;
        }
    }
}
