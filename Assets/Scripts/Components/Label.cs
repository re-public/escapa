using Escapa.Core.Managers;
using TMPro;
using UnityEngine;

namespace Escapa.Components
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class Label : MonoBehaviour
    {        
        protected TextMeshProUGUI TextMesh;

        protected void Awake() => TextMesh = GetComponent<TextMeshProUGUI>();

        protected void OnEnable() => DifficultyManager.DifficultyChanged += OnDifficultyChanged;

        protected void Start() => TextMesh.color = StyleManager.Current.text;

        protected void OnDisable() => DifficultyManager.DifficultyChanged -= OnDifficultyChanged;

        private void OnDifficultyChanged() => TextMesh.color = StyleManager.Current.text;
    }
}