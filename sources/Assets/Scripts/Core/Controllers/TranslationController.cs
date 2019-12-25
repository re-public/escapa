using Escapa.Core.Interfaces;
using Escapa.Utility;
using System.Linq;
using UnityEngine;

namespace Escapa.Core.Controllers
{
    public sealed class TranslationController : MonoBehaviour, ITranslationController
    {
        public Language Current { get; private set; }

        [SerializeField]
        private Language[] Languages;

        private void Awake()
        {
            var current = Languages.FirstOrDefault(x => x.SystemLanguage == Application.systemLanguage);
            if (current == null)
                current = Languages.FirstOrDefault(x => x.SystemLanguage == SystemLanguage.English);

            Current = current;
        }
    }
}
