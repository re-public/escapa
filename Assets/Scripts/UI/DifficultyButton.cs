using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.UI
{
    [RequireComponent(typeof(ILabel))]
    public sealed class DifficultyButton : MonoBehaviour
    {
        private IDifficultyController _difficultyController;
        private ITranslationController _translationController;
        private ILabel _label;

        private void Awake()
        {
            _difficultyController = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
            _translationController = GameObject.FindWithTag(Tags.TranslationController).GetComponent<ITranslationController>();
            _label = GetComponent<ILabel>();
        }

        private void OnEnable()
        {
            _difficultyController.Changed += OnDifficultyChanged;
        }

        private void OnDisable()
        {
            _difficultyController.Changed -= OnDifficultyChanged;
        }

        private void OnDifficultyChanged()
        {
            switch (_difficultyController.Current.Difficulty)
            {
                default:
                case Difficulties.Easy:
                    _label.SetText(_translationController.Current.GetString(LanguageTokens.DifficultyEasy));
                    break;
                case Difficulties.Medium:
                    _label.SetText(_translationController.Current.GetString(LanguageTokens.DifficultyMedium));
                    break;
                case Difficulties.Hard:
                    _label.SetText(_translationController.Current.GetString(LanguageTokens.DifficultyHard));
                    break;
                case Difficulties.Insane:
                    _label.SetText(_translationController.Current.GetString(LanguageTokens.DifficultyInsane));
                    break;
            }
        }
    }
}
