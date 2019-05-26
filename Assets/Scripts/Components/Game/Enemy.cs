using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Components.Game
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public sealed class Enemy : MonoBehaviour
    {
        public Difficulties difficulty;

        private float minSpeed;
        private float maxSpeed;

        private new Rigidbody2D rigidbody2D;
        private SpriteRenderer spriteRenderer;
        private IGameController gameController;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<IGameController>();
        }

        private void OnEnable() => gameController.GameStarted += OnGameStarted;

        private void Start()
        {
            if (DifficultyManager.Current.difficulty < difficulty)
                gameObject.SetActive(false);
            else
            {
                minSpeed = DifficultyManager.Current.minSpeed;
                maxSpeed = DifficultyManager.Current.maxSpeed;
                spriteRenderer.color = StyleManager.Colors.Enemy;
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
