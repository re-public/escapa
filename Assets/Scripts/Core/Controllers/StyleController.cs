using Escapa.Core.Events;
using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    public class StyleController : MonoBehaviour, IStyleController
    {
        public event StyleEvent Changed;

        [SerializeField]
        private Style[] Styles;

        private IDifficultyController _difficultyController;

        private void Awake()
        {
            _difficultyController = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
        }

        private void OnEnable()
        {
            _difficultyController.Changed += OnDifficultyChanged;
        }

        private void OnDisable()
        {
            _difficultyController.Changed += OnDifficultyChanged;
        }

        private void OnDifficultyChanged(object sender, DifficultyEventArgs e)
        {
            Changed?.Invoke(_difficultyController, new StyleEventArgs(Styles[(int)e.Level.Difficulty]));
        }
    }
}
