using UnityEngine;

namespace Escapa.Buttons
{
    public sealed class LinkButton : MonoBehaviour, IButton
    {
        public string Url;

        public void Action() => Application.OpenURL(Url);
    }
}
