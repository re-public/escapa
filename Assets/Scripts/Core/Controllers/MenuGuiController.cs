using Escapa.Core.Interfaces;
using Escapa.Utility;
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

        private ISoundController soundController;
        private Image soundButton;

        public void GoToScene(string scene) => SceneManager.LoadScene(scene);

        public void Mute()
        {
            soundController.Mute();
            soundButton.overrideSprite = soundController.IsMuted ? soundOff : soundOn;
        }

        private void Awake()
        {
            soundController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISoundController>();
            soundButton = GameObject.FindWithTag(Tags.SoundButton).GetComponent<Image>();
        }

        private void Start() => soundButton.overrideSprite = soundController.IsMuted ? soundOff : soundOn;
    }
}
