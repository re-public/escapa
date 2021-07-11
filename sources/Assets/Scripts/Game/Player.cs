using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using System;
using UnityEngine;

namespace Escapa.Game
{
    [RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
    public sealed class Player : MonoBehaviour, IPlayer
    {
        public event EventHandler Died;
        public event EventHandler Moved;
        public event EventHandler Pressed;
        public event EventHandler Stopped;

        private new IMainCamera camera;
        private SpriteRenderer spriteRenderer;

        private bool isTouched;
        private Vector2 oldPosition;
        private Vector2 targetPosition;

        private const float Width = 0.75f;

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
                Moved?.Invoke(gameObject, null);
            else
                Stopped?.Invoke(gameObject, null);
            oldPosition = targetPosition;
        }

        private void OnCollisionEnter2D() => Died?.Invoke(gameObject, null);

        private void OnTouch(Vector2 position)
        {
            isTouched = IsTouched(position, targetPosition);

            if (isTouched)
            {
                targetPosition = position;
                Pressed?.Invoke(gameObject, null);
            }
        }

        private static bool IsTouched(Vector2 position, Vector2 touchPosition) => position.x - Width < touchPosition.x
                                                                               && touchPosition.x < position.x + Width
                                                                               && position.y - Width < touchPosition.y
                                                                               && touchPosition.y < position.y + Width;
    }
}
