using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(Button))]
    public sealed class LinkButton : MonoBehaviour, IButton
    {
        public string Url;

        public void Action() => Application.OpenURL(Url);
    }
}
