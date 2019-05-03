using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Escapa.Controllers
{
    public sealed class SetupController : MonoBehaviour
    {
        public void OnCountValueChanged(float value) => _countSliderText.text = value.ToString();

        public void OnMaxSpeedValueChanged(float value)
        {
            if (_minSpeedSlider.value > value)
                _minSpeedSlider.value = value;

            _maxSpeedSliderText.text = value.ToString("0.0");
        }

        public void OnMinSpeedSliderChanged(float value)
        {
            if (_maxSpeedSlider.value < value)
                _maxSpeedSlider.value = value;

            _minSpeedSliderText.text = value.ToString("0.0");
        }

        public void OnStartClicked()
        {
            DifficultyManager.Difficulty.Count = (int)_countSlider.value;
            DifficultyManager.Difficulty.MinSpeed = _minSpeedSlider.value;
            DifficultyManager.Difficulty.MaxSpeed = _maxSpeedSlider.value;
            SceneManager.LoadSceneAsync((int) GameScenes.Game, LoadSceneMode.Single);
        }
        
        #region UI Elements
        private TextMeshProUGUI _countSliderText;
        private TextMeshProUGUI _minSpeedSliderText;
        private TextMeshProUGUI _maxSpeedSliderText;
        private Slider _countSlider;
        private Slider _minSpeedSlider;
        private Slider _maxSpeedSlider;
        #endregion

        private void Awake()
        {
            _countSliderText = GameObject.FindWithTag(Tags.CountSliderText).GetComponent<TextMeshProUGUI>();
            _minSpeedSliderText = GameObject.FindWithTag(Tags.MinSpeedSliderText).GetComponent<TextMeshProUGUI>();
            _maxSpeedSliderText = GameObject.FindWithTag(Tags.MaxSpeedSliderText).GetComponent<TextMeshProUGUI>();
            _countSlider = GameObject.FindWithTag(Tags.CountSlider).GetComponent<Slider>();
            _minSpeedSlider = GameObject.FindWithTag(Tags.MinSpeedSlider).GetComponent<Slider>();
            _maxSpeedSlider = GameObject.FindWithTag(Tags.MaxSpeedSlider).GetComponent<Slider>();
        }

        private void Start()
        {
            _countSliderText.text = DifficultyManager.Difficulty.Count.ToString();
            _countSlider.value = DifficultyManager.Difficulty.Count;
            _countSlider.minValue = DifficultyManager.MinEnemyCountForSetup;
            _countSlider.maxValue = DifficultyManager.MaxEnemyCountForSetup;

            _minSpeedSliderText.text = DifficultyManager.Difficulty.MinSpeed.ToString("0.0");
            _minSpeedSlider.value = DifficultyManager.Difficulty.MinSpeed;
            _minSpeedSlider.minValue = DifficultyManager.MinEnemySpeedForSetup;
            _minSpeedSlider.maxValue = DifficultyManager.MaxEnemySpeedForSetup;

            _maxSpeedSliderText.text = DifficultyManager.Difficulty.MaxSpeed.ToString("0.0");
            _maxSpeedSlider.value = DifficultyManager.Difficulty.MaxSpeed;
            _maxSpeedSlider.minValue = DifficultyManager.MinEnemySpeedForSetup;
            _maxSpeedSlider.maxValue = DifficultyManager.MaxEnemySpeedForSetup;

            //Style
            Camera.main.backgroundColor = StyleManager.CurrentTheme.Background;
            _countSliderText.color = StyleManager.CurrentTheme.Text;
            _minSpeedSliderText.color = StyleManager.CurrentTheme.Text;
            _maxSpeedSliderText.color = StyleManager.CurrentTheme.Text;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                SceneManager.LoadSceneAsync((int) GameScenes.Menu, LoadSceneMode.Single);
        }
    }
}
