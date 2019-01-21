using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Controllers
{
    public sealed class SetupController : MonoBehaviour, ISceneController
    {
        public void PrepareScene()
        {
            _gameSetupTitleText.text = LanguageManager.Language.GameSetupTitle;
            _enemiesCountText.text = LanguageManager.Language.EnemiesCount;
            _minimalSpeedText.text = LanguageManager.Language.MinimalSpeed;
            _maximumSpeedText.text = LanguageManager.Language.MaximumSpeed;
            _startText.text = LanguageManager.Language.Start;

            _countSlider.value = DifficultyManager.Difficulty.Count;
            _countSlider.minValue = DifficultyManager.MinEnemyCountForSetup;
            _countSlider.maxValue = DifficultyManager.MaxEnemyCountForSetup;

            _minSpeedSlider.value = DifficultyManager.Difficulty.MinSpeed;
            _minSpeedSlider.minValue = DifficultyManager.MinEnemySpeedForSetup;
            _minSpeedSlider.maxValue = DifficultyManager.MaxEnemySpeedForSetup;

            _maxSpeedSlider.value = DifficultyManager.Difficulty.MaxSpeed;
            _maxSpeedSlider.minValue = DifficultyManager.MinEnemySpeedForSetup;
            _maxSpeedSlider.maxValue = DifficultyManager.MaxEnemySpeedForSetup;
        }

        public void StyleScene()
        {
            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;
            _gameSetupTitleText.color = StyleManager.CurrentTheme.Text;
            _enemiesCountText.color = StyleManager.CurrentTheme.Text;
            _minimalSpeedText.color = StyleManager.CurrentTheme.Text;
            _maximumSpeedText.color = StyleManager.CurrentTheme.Text;
            _startText.color = StyleManager.CurrentTheme.Text;
        }

        public void OnMaxSpeedValueChanged(float value)
        {
            if (_minSpeedSlider.value > value)
                _minSpeedSlider.value = value;
        }

        public void OnMinSpeedSliderChanged(float value)
        {
            if (_maxSpeedSlider.value < value)
                _maxSpeedSlider.value = value;
        }

        public void OnStartClicked()
        {
            DifficultyManager.Difficulty.Count = (int)_countSlider.value;
            DifficultyManager.Difficulty.MinSpeed = _minSpeedSlider.value;
            DifficultyManager.Difficulty.MaxSpeed = _maxSpeedSlider.value;
            _systemController.GoToScene(GameScenes.Game);
        }

        private ISystemController _systemController;
        #region UI Elements
        private TextMeshProUGUI _gameSetupTitleText;
        private TextMeshProUGUI _enemiesCountText;
        private TextMeshProUGUI _minimalSpeedText;
        private TextMeshProUGUI _maximumSpeedText;
        private TextMeshProUGUI _startText;
        private Slider _countSlider;
        private Slider _minSpeedSlider;
        private Slider _maxSpeedSlider;
        #endregion

        private void Awake()
        {
            _systemController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISystemController>();

            _gameSetupTitleText = GameObject.FindWithTag(Tags.GameSetupText).GetComponent<TextMeshProUGUI>();
            _enemiesCountText = GameObject.FindWithTag(Tags.EnemiesCountText).GetComponent<TextMeshProUGUI>();
            _minimalSpeedText = GameObject.FindWithTag(Tags.MinimalSpeedText).GetComponent<TextMeshProUGUI>();
            _maximumSpeedText = GameObject.FindWithTag(Tags.MaximumSpeedText).GetComponent<TextMeshProUGUI>();
            _startText = GameObject.FindWithTag(Tags.StartText).GetComponent<TextMeshProUGUI>();
            _countSlider = GameObject.FindWithTag(Tags.CountSlider).GetComponent<Slider>();
            _minSpeedSlider = GameObject.FindWithTag(Tags.MinSpeedSlider).GetComponent<Slider>();
            _maxSpeedSlider = GameObject.FindWithTag(Tags.MaxSpeedSlider).GetComponent<Slider>();
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                _systemController.GoToScene(GameScenes.Menu);
        }
    }
}
