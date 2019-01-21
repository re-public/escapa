using Escapa.Events;
using Escapa.Managers;
using UnityEngine;

namespace Escapa.Units
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public sealed class Player : MonoBehaviour, IPlayer
    {
        public event PlayerEvent Die;
        public event PlayerEvent MousePressed;

        public Color Color
        {
            get => _spriteRenderer.color;
            set => _spriteRenderer.color = value;
        }

        public float IdleTime => ScoreManager.CurrentRecord - _idleTimeStart;

        public float MovingTime => ScoreManager.CurrentRecord - _moveTimeStart;

        private Vector2 _prevPosition;
        private SpriteRenderer _spriteRenderer;
        private float _idleTimeStart;
        private float _moveTimeStart;

        private void Awake()
        {
            _prevPosition = transform.position;
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _idleTimeStart = 0f;
            _moveTimeStart = 0f;
        }

        private void OnCollisionEnter2D(Collision2D collision) => Die?.Invoke();

        private void OnMouseDown() => MousePressed?.Invoke();

        private void OnMouseDrag()
        {
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.Translate(targetPosition - _prevPosition);

            if (targetPosition == _prevPosition)
            {
                if (_idleTimeStart < float.Epsilon)
                    _idleTimeStart = ScoreManager.CurrentRecord;
                _moveTimeStart = 0f;
            }
            else
            {
                if (_moveTimeStart < float.Epsilon)
                    _moveTimeStart = ScoreManager.CurrentRecord;
                _idleTimeStart = 0f;
            }

            _prevPosition = targetPosition;
        }
    }
}
