using Escapa.Core.Interfaces;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Core.Controllers
{
    public sealed class MenuGuiController : GuiControllerBase
    {
        [SerializeField]
        private Sprite soundOn;
        [SerializeField]
        private Sprite soundOff;

        private TextMeshProUGUI difficultyButton;
        private IDifficultyController _difficulty;
        private ISoundController _sound;
        private ITranslationController _translation;
        private Image soundButton;

        public void AddDifficulty()
        {
            _difficulty.Increase();
            difficultyButton.SetText(GetString(_difficulty.Current.Difficulty));
        }

        public void GoToInfo() => LoadScene(GameScenes.Info);

        public void GoToSocial() => LoadScene(GameScenes.Social);

        public void StartGame() => LoadScene(GameScenes.Game);

        public void ToggleSound()
        {
            _sound.ToggleSound();
            soundButton.overrideSprite = _sound.IsMuted ? soundOff : soundOn;
        }

        private void Awake()
        {
            difficultyButton = GameObject.FindWithTag(Tags.DifficultyButton).GetComponent<TextMeshProUGUI>();
            _difficulty = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
            _translation = GameObject.FindWithTag(Tags.TranslationController).GetComponent<ITranslationController>();
            _sound = GameObject.FindWithTag(Tags.SoundController).GetComponent<ISoundController>();
            soundButton = GameObject.FindWithTag(Tags.SoundButton).GetComponent<Image>();
        }

        private void Start()
        {
            difficultyButton.SetText(GetString(_difficulty.Current.Difficulty));
            soundButton.overrideSprite = _sound.IsMuted ? soundOff : soundOn;
        }

        private string GetString(Difficulties difficulty)
        {
            switch (difficulty)
            {
                default:
                case Difficulties.Easy:
                    return _translation.Current.GetString(LanguageTokens.DifficultyEasy);
                case Difficulties.Medium:
                    return _translation.Current.GetString(LanguageTokens.DifficultyMedium);
                case Difficulties.Hard:
                    return _translation.Current.GetString(LanguageTokens.DifficultyHard);
                case Difficulties.Insane:
                    return _translation.Current.GetString(LanguageTokens.DifficultyInsane);
            }
        }
    }
}
