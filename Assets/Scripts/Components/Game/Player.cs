using Escapa.Core.Events;
using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Components.Game
{
    [RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
    public sealed class Player : MonoBehaviour, IPlayer
    {
        public event GameEvent Died;
        public event GameEvent Moved;
        public event GameEvent Pressed;
        public event GameEvent Stopped;

        private IMainCamera _camera;
        private SpriteRenderer _spriteRenderer;

        private bool _isTouched;
        private Vector2 _oldPosition;
        private Vector2 _targetPosition;

        private void Awake()
        {
            _camera = GameObject.FindWithTag(Tags.MainCamera).GetComponent<IMainCamera>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start() => _spriteRenderer.color = StyleManager.Current.player;

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                var position = _camera.ScreenToWorldPoint(touch.position);
                OnTouch(position);
            }

            if (_isTouched)
                transform.position = _targetPosition;

            if (Vector2.Distance(_targetPosition, _oldPosition) > float.Epsilon)
                Moved?.Invoke();
            else
                Stopped?.Invoke();
            _oldPosition = _targetPosition;
        }

        private void OnCollisionEnter2D() => Died?.Invoke();

        private void OnTouch(Vector2 position)
        {
            _isTouched = IsTouched(position, _targetPosition);

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
