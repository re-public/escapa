using Escapa.Core.Managers;
using TMPro;
using UnityEngine;

namespace Escapa.Components.Buttons
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class TextButtonBase : ButtonBase
    {
        protected TextMeshProUGUI TextMesh;

        protected virtual void OnDifficultyChanged() => TextMesh.color = StyleManager.Current.text;

        protected void Awake() => TextMesh = GetComponent<TextMeshProUGUI>();

        protected void OnEnable() => DifficultyManager.DifficultyChanged += OnDifficultyChanged;

        protected void Start() => TextMesh.color = StyleManager.Current.text;

        protected void OnDisable() => DifficultyManager.DifficultyChanged -= OnDifficultyChanged;
    }
}