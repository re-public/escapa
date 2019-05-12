using Escapa.Core.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(TextMeshProUGUI), typeof(Button))]
    public abstract class TextButtonBase : MonoBehaviour
    {
        public bool disableTranslating;
        public LanguageTokens token;
        
        protected TextMeshProUGUI TextMesh;
        
        public abstract void Action();
        
        protected void Awake()
        {
            TextMesh = GetComponent<TextMeshProUGUI>();
        }

        protected void OnEnable()
        {
            DifficultyManager.DifficultyChanged += OnDifficultyChanged;
        }

        protected void Start()
        {
            if (!disableTranslating)
            {
                TextMesh.SetText(LanguageManager.GetString(token));
            }

            TextMesh.color = StyleManager.Current.text;
        }

        protected void OnDisable()
        {
            DifficultyManager.DifficultyChanged -= OnDifficultyChanged;
        }

        private void OnDifficultyChanged()
        {
            TextMesh.color = StyleManager.Current.text;
        }
    }
}