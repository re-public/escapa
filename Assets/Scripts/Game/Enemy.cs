using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Game
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public sealed class Enemy : MonoBehaviour
    {
        [SerializeField]
        private Difficulties difficulty;

        private float minSpeed;
        private float maxSpeed;

        private new Rigidbody2D rigidbody2D;
        private SpriteRenderer spriteRenderer;
        private IGameController gameController;
        private IDifficultyController _difficulty;
        private IStyleController _style;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<IGameController>();
            _difficulty = GameObject.FindWithTag(Tags.SystemController).GetComponent<IDifficultyController>();
            _style = GameObject.FindWithTag(Tags.StyleController).GetComponent<IStyleController>();
        }

        private void OnEnable() => gameController.GameStarted += OnGameStarted;

        private void Start()
        {
            if (_difficulty.Current.Difficulty < difficulty)
                gameObject.SetActive(false);
            else
            {
                minSpeed = _difficulty.Current.MinSpeed;
                maxSpeed = _difficulty.Current.MaxSpeed;
                spriteRenderer.color = _style.Current.Enemy;
            }
        }

        private void OnDisable() => gameController.GameStarted -= OnGameStarted;

        private void OnGameStarted()
        {
            var xForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(minSpeed, maxSpeed);
            var yForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(minSpeed, maxSpeed);
            rigidbody2D.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        }
    }
}
