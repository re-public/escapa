using Escapa.Core.Managers;
using UnityEngine;

namespace Escapa.Buttons
{
    public sealed class LinkButton : TextButtonBase
    {
        public string url;

        public override void Action() => Application.OpenURL(url);

        private new void Start()
        {
            base.Start();
            TextMesh.SetText(LanguageManager.GetString(token));
        }
    }
}
