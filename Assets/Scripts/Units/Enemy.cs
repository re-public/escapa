using Escapa.Controllers;
using Escapa.Managers;
using Escapa.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Escapa.Units
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public sealed class Enemy : MonoBehaviour, IEnemy
    {
        public Difficulties difficulty;
        
        public void AddForce()
        {
            var xForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(DifficultyManager.Difficulty.MinSpeed, DifficultyManager.Difficulty.MaxSpeed);
            var yForce = (Random.Range(0, 2) == 0 ? -1 : 1) * Random.Range(DifficultyManager.Difficulty.MinSpeed, DifficultyManager.Difficulty.MaxSpeed);
            _rigidbody2D.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        }

        private IPlayer _player;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private IStyleController _styleController;

        private void Awake()
        {
            _player = GameObject.FindWithTag(Tags.Player).GetComponent<IPlayer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _styleController = GameObject.FindWithTag(Tags.SystemController).GetComponent<IStyleController>();
        }
        
        private void OnEnable()
        {
            _player.MousePressed += OnPlayerPressed;
            _styleController.StyleChanged += OnStyleChanged;
        }

        private void Start()
        {
            if (DifficultyManager.Level < difficulty)
                gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _styleController.StyleChanged -= OnStyleChanged;
        }

        private void OnPlayerPressed()
        {
            _player.MousePressed -= OnPlayerPressed;
            AddForce();
        }
        
        private void OnStyleChanged(Theme theme)
        {
            _spriteRenderer.color = theme.Enemy;
        }
    }
}
