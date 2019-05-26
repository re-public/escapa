using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Components.Buttons
{
    public sealed class LinkButton : TextButtonBase
    {
        [SerializeField]
        private LanguageTokens token;
        [SerializeField]
        private string url;

        public override void Action() => Application.OpenURL(url);

        private new void Start()
        {
            base.Start();
            TextMesh.SetText(LanguageManager.GetString(token));
        }
    }
}
