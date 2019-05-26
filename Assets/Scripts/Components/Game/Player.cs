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

        private new IMainCamera camera;
        private SpriteRenderer spriteRenderer;

        private bool isTouched;
        private Vector2 oldPosition;
        private Vector2 targetPosition;

        private void Awake()
        {
            camera = GameObject.FindWithTag(Tags.MainCamera).GetComponent<IMainCamera>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start() => spriteRenderer.color = StyleManager.Colors.Player;

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

        private void OnCollisionEnter2D() => Died?.Invoke();

        private void OnTouch(Vector2 position)
        {
            isTouched = IsTouched(position, targetPosition);

            if (isTouched)
            {
                targetPosition = position;
                Pressed?.Invoke();
            }
        }

        private static bool IsTouched(Vector2 position, Vector2 touchPosition) => position.x - 0.75f < touchPosition.x
                                                                               && touchPosition.x < position.x + 0.75f
                                                                               && position.y - 0.75f < touchPosition.y
                                                                               && touchPosition.y < position.y + 0.75f;
    }
}
