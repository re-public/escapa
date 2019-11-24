using Escapa.Core.Events;
using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Game
{
    [RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
    public sealed class Player : MonoBehaviour, IPlayer
    {
        public event GameEvent Died;
        public event GameEvent Moved;
        public event GameEvent Pressed;
        public event GameEvent Stopped;

        private new IMainCamera camera;
        private IStyleController _style;
        private SpriteRenderer spriteRenderer;

        private bool isTouched;
        private Vector2 oldPosition;
        private Vector2 targetPosition;

        private const float Width = 0.75f;

        private void Awake()
        {
            camera = GameObject.FindWithTag(Tags.MainCamera).GetComponent<IMainCamera>();
            _style = GameObject.FindWithTag(Tags.StyleController).GetComponent<IStyleController>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start() => spriteRenderer.color = _style.Current.Player;

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                var position = camera.ScreenToWorldPoint(touch.position);
                OnTouch(position);
            }

            if (isTouched)
                transform.position = targetPosition;

            if (Vector2.Distance(targetPosition, oldPosition) > float.Epsilon)
                Moved?.Invoke();
            else
                Stopped?.Invoke();
            oldPosition = targetPosition;
        }

        private void OnCollisionEnter2D(Collision2D _) => Died?.Invoke();

        private void OnTouch(Vector2 position)
        {
            isTouched = IsTouched(position, targetPosition);

            if (isTouched)
            {
                targetPosition = position;
                Pressed?.Invoke();
            }
        }

        private static bool IsTouched(Vector2 position, Vector2 touchPosition) => position.x - Width < touchPosition.x
                                                                               && touchPosition.x < position.x + Width
                                                                               && position.y - Width < touchPosition.y
                                                                               && touchPosition.y < position.y + Width;
    }
}
