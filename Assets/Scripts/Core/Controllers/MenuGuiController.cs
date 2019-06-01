using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Escapa.Core.Controllers
{
    public sealed class MenuGuiController : MonoBehaviour
    {
        [SerializeField]
        private Sprite soundOn;
        [SerializeField]
        private Sprite soundOff;

        private TextMeshProUGUI difficultyButton;
        private ISoundController soundController;
        private Image soundButton;

        public void AddDifficulty()
        {
            DifficultyManager.Increase();
            difficultyButton.SetText(GetString(DifficultyManager.Current.difficulty));
        }

        public void GoToScene(string scene) => SceneManager.LoadScene(scene);

        public void Mute()
        {
            soundController.Mute();
            soundButton.overrideSprite = soundController.IsMuted ? soundOff : soundOn;
        }

        private void Awake()
        {
            difficultyButton = GameObject.FindWithTag(Tags.DifficultyButton).GetComponent<TextMeshProUGUI>();
            soundController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISoundController>();
            soundButton = GameObject.FindWithTag(Tags.SoundButton).GetComponent<Image>();
        }

        private void Start()
        {
            difficultyButton.SetText(GetString(DifficultyManager.Current.difficulty));
            soundButton.overrideSprite = soundController.IsMuted ? soundOff : soundOn;
        }

        private string GetString(Difficulties difficulty)
        {
            switch (difficulty)
            {
                default:
                case Difficulties.Easy:
                    return LanguageManager.GetString(LanguageTokens.DifficultyEasy);
                case Difficulties.Medium:
                    return LanguageManager.GetString(LanguageTokens.DifficultyMedium);
                case Difficulties.Hard:
                    return LanguageManager.GetString(LanguageTokens.DifficultyHard);
                case Difficulties.Insane:
                    return LanguageManager.GetString(LanguageTokens.DifficultyInsane);
            }
        }
    }
}
