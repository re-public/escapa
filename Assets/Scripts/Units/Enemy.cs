using Escapa.Controllers;
using Escapa.Events;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Units
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public sealed class Enemy : MonoBehaviour, IEnemy
    {
        public Difficulties difficulty;
        
        public void AddForce()
        {
            var xForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(_minSpeed, _maxSpeed);
            var yForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(_minSpeed, _maxSpeed);
            _rigidbody2D.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        }

        private float _minSpeed;
        private float _maxSpeed;
        
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private IGameController _gameController;
        private IStyleController _styleController;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<IGameController>();
            _styleController = GameObject.FindWithTag(Tags.SystemController).GetComponent<IStyleController>();
        }
        
        private void OnEnable()
        {
            _gameController.GameInitialized += OnGameInitialized;
            _gameController.GameStarted += OnGameStarted;
            _styleController.StyleChanged += OnStyleChanged;
        }

        private void OnDisable()
        {
            _gameController.GameInitialized -= OnGameInitialized;
            _gameController.GameStarted -= OnGameStarted;
            _styleController.StyleChanged -= OnStyleChanged;
        }

        private void OnGameInitialized(GameEventArgs e)
        {
            if (e.Level.difficulty < difficulty)
            {
                gameObject.SetActive(false);
                _minSpeed = e.Level.minSpeed;
                _maxSpeed = e.Level.maxSpeed;
            }
        }

        private void OnGameStarted(GameEventArgs e)
        {
            AddForce();
        }
        
        private void OnStyleChanged(Theme theme)
        {
            _spriteRenderer.color = theme.Enemy;
        }
    }
}
