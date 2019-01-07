using Escapa.Events;
using UnityEngine;

namespace Escapa.Units
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public sealed class Player : MonoBehaviour, IColoredUnit, IPlayer
    {
        public event PlayerEvent Die;
        public event PlayerEvent MousePressed;

        public Color Color
        {
            get => _spriteRenderer.color;
            set => _spriteRenderer.color = value;
        }

        private Vector2 _prevPosition;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _prevPosition = transform.position;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D collision) => Die?.Invoke();

        private void OnMouseDown() => MousePressed?.Invoke();

        private void OnMouseDrag()
        {
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.Translate(targetPosition - _prevPosition);
            _prevPosition = targetPosition;
        }
    }
}
