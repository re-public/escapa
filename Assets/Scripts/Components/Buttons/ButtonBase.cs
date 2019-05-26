using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Components.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonBase : MonoBehaviour
    {
        public abstract void Action();
    }
}