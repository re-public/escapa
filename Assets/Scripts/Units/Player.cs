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

        private SpriteRenderer _spriteRenderer;

        private static float _unitsPerPixel;
        private float _halfScreenHeight;
        private float _halfScreenWidth;

        private bool _isTouched;
        private Vector3 _targetPosition;
        private Vector3 _offset;
        private Touch _touch;

        private float _idleTimeStart;
        private float _moveTimeStart;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _idleTimeStart = 0f;
            _moveTimeStart = 0f;
            _isTouched = false;

            _unitsPerPixel = Camera.main.orthographicSize * 2.0f / Screen.height;
            _halfScreenHeight = Screen.height / 2.0f;
            _halfScreenWidth = Screen.width / 2.0f;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.GetTouch(0);
                OnTouch(_touch.phase, _touch.position);
            }

            if (_isTouched)
            {
                _targetPosition.x -= _offset.x;
                _targetPosition.y -= _offset.y;

                transform.position = _targetPosition;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) => Die?.Invoke();

        private void OnTouch(TouchPhase phase, Vector2 position)
        {
            _targetPosition.x = (position.x - _halfScreenWidth) * _unitsPerPixel;
            _targetPosition.y = (position.y - _halfScreenHeight) * _unitsPerPixel;

            switch (phase)
            {
                case TouchPhase.Began:
                    _isTouched = IsTouched(transform.position, _targetPosition);

                    _offset.x = _targetPosition.x - transform.position.x;
                    _offset.y = _targetPosition.y - transform.position.y;
                    break;

                case TouchPhase.Moved:
                    if (_moveTimeStart < float.Epsilon)
                        _moveTimeStart = ScoreManager.CurrentRecord;
                    if (_idleTimeStart > float.Epsilon)
                        _idleTimeStart = 0f;
                    break;

                case TouchPhase.Stationary:
                    if (_idleTimeStart < float.Epsilon)
                        _idleTimeStart = ScoreManager.CurrentRecord;
                    if (_moveTimeStart > float.Epsilon)
                        _moveTimeStart = 0f;
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _isTouched = false;
                    break;

                default:
                    break;
            }

            if (_isTouched)
                MousePressed?.Invoke();
        }

        private static bool IsTouched(Vector3 position, Vector3 touchPosition) => position.x - 0.75f < touchPosition.x
                                                                               && touchPosition.x < position.x + 0.75f
                                                                               && position.y - 0.75f < touchPosition.y
                                                                               && touchPosition.y < position.y + 0.75f;

    }
}
