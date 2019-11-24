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

        private float _minSpeed;
        private float _maxSpeed;

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _sprite;
        private IGameController _gameController;
        private IDifficultyController _difficultyController;
        private IStyleController _styleController;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _sprite = GetComponent<SpriteRenderer>();

            _gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<IGameController>();
            _difficultyController = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
            _styleController = GameObject.FindWithTag(Tags.StyleController).GetComponent<IStyleController>();
        }

        private void OnEnable()
        {
            _gameController.GameStarted += OnGameStarted;
            _difficultyController.Changed += OnDifficultyChanged;
            _styleController.Changed += OnStyleChanged;
        }

        private void OnDisable()
        {
            _gameController.GameStarted -= OnGameStarted;
            _difficultyController.Changed -= OnDifficultyChanged;
            _styleController.Changed -= OnStyleChanged;
        }

        private void OnGameStarted()
        {
            var xForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(_minSpeed, _maxSpeed);
            var yForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(_minSpeed, _maxSpeed);
            _rigidbody2D.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        }

        private void OnDifficultyChanged(object sender, DifficultyEventArgs e)
        {
            if (e.Level.Difficulty < difficulty)
                gameObject.SetActive(false);
            else
            {
                _minSpeed = e.Level.MinSpeed;
                _maxSpeed = e.Level.MaxSpeed;
            }
        }

        private void OnStyleChanged(object sender, StyleEventArgs e)
        {
            _sprite.color = e.Style.Enemy;
        }
    }
}
