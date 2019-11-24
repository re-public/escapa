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

        private IDifficultyController _difficultyController;
        private ISoundController _soundController;
        private Image _soundButton;

        public void AddDifficulty()
        {
            _difficultyController.Increase();
        }

        public void GoToInfo() => LoadScene(GameScenes.Info);

        public void GoToSocial() => LoadScene(GameScenes.Social);

        public void StartGame() => LoadScene(GameScenes.Game);

        public void ToggleSound()
        {
            _soundController.ToggleSound();
            _soundButton.overrideSprite = _soundController.IsMuted ? soundOff : soundOn;
        }

        private void Awake()
        {
            _difficultyController = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
            _soundController = GameObject.FindWithTag(Tags.SoundController).GetComponent<ISoundController>();
            _soundButton = GameObject.FindWithTag(Tags.SoundButton).GetComponent<Image>();
        }

        private void Start()
        {
            _soundButton.overrideSprite = _soundController.IsMuted ? soundOff : soundOn;
        }
    }
}
