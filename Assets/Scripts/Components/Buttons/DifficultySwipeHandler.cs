using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Components.Buttons
{
    public sealed class DifficultySwipeHandler : MonoBehaviour
    {
        public float distance;

        private IMainCamera _camera;
        private Vector2 _startPoint;
        private Vector2 _endPoint;

        private void Awake() => _camera = GameObject.FindWithTag(Tags.MainCamera).GetComponent<IMainCamera>();

        private void FixedUpdate()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _startPoint = _camera.ScreenToWorldPoint(touch.position);
                        break;
                    case TouchPhase.Ended:
                        _endPoint = _camera.ScreenToWorldPoint(touch.position);
                        if (Vector2.Distance(_startPoint, _endPoint) > distance)
                        {
                            // If swipe was directed from right to left
                            if (Vector2.Dot(Vector2.left, _endPoint - _startPoint) > 0)
                                DifficultyManager.Increase();
                            // If swipe was directed from left to right
                            else if (Vector2.Dot(_endPoint - _startPoint, Vector2.right) > 0)
                                DifficultyManager.Decrease();
                        }
                        _startPoint = Vector2.zero;
                        _endPoint = Vector2.zero;
                        break;
                    case TouchPhase.Canceled:
                        _startPoint = Vector2.zero;
                        _endPoint = Vector2.zero;
                        break;
                }
            }
        }
    }
}