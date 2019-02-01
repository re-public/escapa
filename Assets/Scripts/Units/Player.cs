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

        private void FixedUpdate()
        {
            if (Input.touchCount > 0)
                OnTouch(Input.GetTouch(0));
        }

        private void OnCollisionEnter2D(Collision2D collision) => Die?.Invoke();

        private void OnTouch(Touch touch)
        {
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
            if (!IsTouched(transform.position, targetPosition)) return;

            MousePressed?.Invoke();

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    if (_moveTimeStart < float.Epsilon)
                        _moveTimeStart = ScoreManager.CurrentRecord;
                    if (_idleTimeStart > float.Epsilon)
                        _idleTimeStart = 0f;

                    transform.Translate(targetPosition - _prevPosition);
                    _prevPosition = targetPosition;

                    break;
                case TouchPhase.Stationary:
                    if (_idleTimeStart < float.Epsilon)
                        _idleTimeStart = ScoreManager.CurrentRecord;
                    if (_moveTimeStart > float.Epsilon)
                        _moveTimeStart = 0f;

                    break;
                case TouchPhase.Ended:
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }
        }

        private static bool IsTouched(Vector2 position, Vector2 touchPosition) => position.x - 0.75f < touchPosition.x
                                                                               && touchPosition.x < position.x + 0.75f
                                                                               && position.y - 0.75f < touchPosition.y
                                                                               && touchPosition.y < position.y + 0.75f;
    }
}
