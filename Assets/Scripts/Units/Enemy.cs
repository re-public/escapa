using Escapa.Core.Interfaces;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Units
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public sealed class Enemy : MonoBehaviour
    {
        public Difficulties difficulty;

        private float _minSpeed;
        private float _maxSpeed;
        
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private IGameController _gameController;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<IGameController>();
        }
        
        private void OnEnable()
        {
            _gameController.GameStarted += OnGameStarted;
        }

        private void Start()
        {
            if (DifficultyManager.Current.difficulty < difficulty)
            {
                gameObject.SetActive(false);
            }
            else
            {
                _minSpeed = DifficultyManager.Current.minSpeed;
                _maxSpeed = DifficultyManager.Current.maxSpeed;
                _spriteRenderer.color = StyleManager.Current.enemy;
            }
        }

        private void OnDisable()
        {
            _gameController.GameStarted -= OnGameStarted;
        }

        private void OnGameStarted()
        {
            var xForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(_minSpeed, _maxSpeed);
            var yForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(_minSpeed, _maxSpeed);
            _rigidbody2D.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        }
    }
}
