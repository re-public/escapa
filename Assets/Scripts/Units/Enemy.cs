using Escapa.Managers;
using UnityEngine;

namespace Escapa.Units
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public sealed class Enemy : MonoBehaviour, IEnemy
    {
        public Color Color
        {
            get => _spriteRenderer.color;
            set => _spriteRenderer.color = value;
        }

        public void AddForce()
        {
            var xForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(DifficultyManager.Difficulty.MinSpeed, DifficultyManager.Difficulty.MaxSpeed);
            var yForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(DifficultyManager.Difficulty.MinSpeed, DifficultyManager.Difficulty.MaxSpeed);
            _rigidbody2D.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        }

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
