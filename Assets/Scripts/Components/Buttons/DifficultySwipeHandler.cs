using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Components.Buttons
{
    public sealed class DifficultySwipeHandler : MonoBehaviour
    {
        public float distance;

        private new IMainCamera camera;
        private Vector2 startPoint;
        private Vector2 endPoint;

        private void Awake() => camera = GameObject.FindWithTag(Tags.MainCamera).GetComponent<IMainCamera>();

        private void FixedUpdate()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPoint = camera.ScreenToWorldPoint(touch.position);
                        break;
                    case TouchPhase.Ended:
                        endPoint = camera.ScreenToWorldPoint(touch.position);
                        if (Vector2.Distance(startPoint, endPoint) > distance)
                        {
                            // If swipe was directed from right to left
                            if (Vector2.Dot(Vector2.left, endPoint - startPoint) > 0)
                                DifficultyManager.Increase();
                            // If swipe was directed from left to right
                            else if (Vector2.Dot(endPoint - startPoint, Vector2.right) > 0)
                                DifficultyManager.Decrease();
                        }
                        startPoint = Vector2.zero;
                        endPoint = Vector2.zero;
                        break;
                    case TouchPhase.Canceled:
                        startPoint = Vector2.zero;
                        endPoint = Vector2.zero;
                        break;
                }
            }
        }
    }
}
