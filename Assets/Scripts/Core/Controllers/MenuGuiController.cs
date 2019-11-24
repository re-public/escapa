using Escapa.Core.Interfaces;
using Escapa.Utility;
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

        private IDifficultyController _difficulty;
        private ISoundController _sound;
        private Image soundButton;

        public void AddDifficulty()
        {
            _difficulty.Increase();
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
            _difficulty = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
            _sound = GameObject.FindWithTag(Tags.SoundController).GetComponent<ISoundController>();
            soundButton = GameObject.FindWithTag(Tags.SoundButton).GetComponent<Image>();
        }

        private void Start()
        {
            soundButton.overrideSprite = _sound.IsMuted ? soundOff : soundOn;
        }
    }
}
