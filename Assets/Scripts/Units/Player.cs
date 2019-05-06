using Escapa.Components;
using Escapa.Controllers;
using Escapa.Events;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Units
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public sealed class Player : MonoBehaviour, IPlayer
    {
        public event PlayerEvent Die;
        public event PlayerEvent MousePressed;

        public float IdleTime => ScoreManager.CurrentRecord - _idleTimeStart;

        public float MovingTime => ScoreManager.CurrentRecord - _moveTimeStart;

        private IMainCamera _camera;
        private SpriteRenderer _spriteRenderer;
        private IStyleController _styleController;
        
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
            _camera = GameObject.FindWithTag(Tags.MainCamera).GetComponent<IMainCamera>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _styleController = GameObject.FindWithTag(Tags.SystemController).GetComponent<IStyleController>();
            
            _halfScreenHeight = Screen.height / 2.0f;
            _halfScreenWidth = Screen.width / 2.0f;
        }

        private void OnEnable()
        {
            _styleController.StyleChanged += OnStyleChanged;
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

        private void OnCollisionEnter2D() => Die?.Invoke();

        private void OnDisable()
        {
            _styleController.StyleChanged -= OnStyleChanged;
        }

        private void OnStyleChanged(StyleEventArgs e)
        {
            _spriteRenderer.color = e.Theme.Player;
        }

        private void OnTouch(TouchPhase phase, Vector2 position)
        {
            _targetPosition.x = (position.x - _halfScreenWidth) * _camera.UnitsPerPixel;
            _targetPosition.y = (position.y - _halfScreenHeight) * _camera.UnitsPerPixel;

            switch (phase)
            {
                case TouchPhase.Began:
                    var pos = transform.position;
                    
                    _isTouched = IsTouched(pos, _targetPosition);
                    _offset.x = _targetPosition.x - pos.x;
                    _offset.y = _targetPosition.y - pos.y;
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
