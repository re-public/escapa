using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    public class StyleController : MonoBehaviour, IStyleController
    {
        public Style Current => Styles[(int)_difficulty.Current.Difficulty];

        [SerializeField]
        private Style[] Styles;

        private IDifficultyController _difficulty;

        private void Awake() => _difficulty = GameObject.FindWithTag(Tags.DifficultyController).GetComponent<IDifficultyController>();
    }
}
