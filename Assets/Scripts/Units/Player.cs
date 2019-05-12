using Escapa.Components;
using Escapa.Core.Managers;
using Escapa.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Units
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public sealed class Player : MonoBehaviour, IPlayer
    {
        public event GameEvent Died;
        public event GameEvent Moved;
        public event GameEvent Pressed;
        public event GameEvent Stopped;

        private IMainCamera _camera;
        private SpriteRenderer _spriteRenderer;

        private bool _isTouched;
        private Vector2 _targetPosition;

        private void Awake()
        {
            _camera = GameObject.FindWithTag(Tags.MainCamera).GetComponent<IMainCamera>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _spriteRenderer.color = StyleManager.Current.player;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                var position = _camera.ScreenToWorldPoint(touch.position);
                OnTouch(touch.phase, position);
            }

            if (_isTouched)
            {
                transform.position = _targetPosition;
            }
        }

        private void OnCollisionEnter2D() => Died?.Invoke();

        private void OnTouch(TouchPhase phase, Vector2 position)
        {
            _isTouched = IsTouched(position, _targetPosition);

            switch (phase)
            {
                case TouchPhase.Began:
                case TouchPhase.Moved:
                    Moved?.Invoke();
                    break;

                case TouchPhase.Stationary:
                    Stopped?.Invoke();
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _isTouched = false;
                    Stopped?.Invoke();
                    break;
            }

            if (_isTouched)
            {
                _targetPosition = position;
                Pressed?.Invoke();
            }
        }

        private static bool IsTouched(Vector2 position, Vector2 touchPosition) => position.x - 0.75f < touchPosition.x
                                                                               && touchPosition.x < position.x + 0.75f
                                                                               && position.y - 0.75f < touchPosition.y
                                                                               && touchPosition.y < position.y + 0.75f;

    }
}
