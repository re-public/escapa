using Escapa.Controllers;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Units
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public sealed class Enemy : MonoBehaviour, IEnemy
    {
        public void AddForce()
        {
            var xForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(DifficultyManager.Difficulty.MinSpeed, DifficultyManager.Difficulty.MaxSpeed);
            var yForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(DifficultyManager.Difficulty.MinSpeed, DifficultyManager.Difficulty.MaxSpeed);
            _rigidbody2D.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        }

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private IStyleController _styleController;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _styleController = GameObject.FindWithTag(Tags.SystemController).GetComponent<IStyleController>();
        }
        
        private void OnEnable()
        {
            _styleController.StyleChanged += OnStyleChanged;
        }
        
        private void OnDisable()
        {
            _styleController.StyleChanged -= OnStyleChanged;
        }
        
        private void OnStyleChanged(Theme theme)
        {
            _spriteRenderer.color = theme.Player;
        }
    }
}
