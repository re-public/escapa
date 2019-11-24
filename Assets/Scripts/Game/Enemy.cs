using Escapa.Core.Events;
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
        private IDifficultyController _difficultyController;
        private IStyleController _style;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<IGameController>();
            _difficultyController = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
            _style = GameObject.FindWithTag(Tags.StyleController).GetComponent<IStyleController>();
        }

        private void OnEnable()
        {
            _difficultyController.Changed += OnDifficultyChanged;
            gameController.GameStarted += OnGameStarted;
        }

        private void OnDisable()
        {
            _difficultyController.Changed -= OnDifficultyChanged;
            gameController.GameStarted -= OnGameStarted;
        }

        private void OnGameStarted()
        {
            var xForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(minSpeed, maxSpeed);
            var yForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(minSpeed, maxSpeed);
            rigidbody2D.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        }

        private void OnDifficultyChanged(object sender, DifficultyEventArgs e)
        {
            if (e.Level.Difficulty < difficulty)
                gameObject.SetActive(false);
            else
            {
                minSpeed = e.Level.MinSpeed;
                maxSpeed = e.Level.MaxSpeed;
                spriteRenderer.color = _style.Current.Enemy;
            }
        }
    }
}
