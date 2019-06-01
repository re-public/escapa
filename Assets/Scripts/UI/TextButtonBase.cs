using Escapa.Core.Managers;
using TMPro;
using UnityEngine;

namespace Escapa.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class TextButtonBase : MonoBehaviour
    {
        protected TextMeshProUGUI TextMesh;

        public abstract void Action();

        protected virtual void OnDifficultyChanged() => TextMesh.color = StyleManager.Colors.Text;

        protected void Awake() => TextMesh = GetComponent<TextMeshProUGUI>();

        protected void OnEnable() => DifficultyManager.DifficultyChanged += OnDifficultyChanged;

        protected void Start() => TextMesh.color = StyleManager.Colors.Text;

        protected void OnDisable() => DifficultyManager.DifficultyChanged -= OnDifficultyChanged;
    }
}