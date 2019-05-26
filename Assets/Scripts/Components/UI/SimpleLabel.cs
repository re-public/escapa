using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Components.UI
{
    public sealed class SimpleLabel : Label
    {
        [SerializeField]
        private LanguageTokens token;

        private new void Start()
        {
            base.Start();
            TextMesh.SetText(LanguageManager.GetString(token));
        }
    }
}