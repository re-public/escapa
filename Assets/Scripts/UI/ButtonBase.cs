using UnityEngine;
using UnityEngine.UI;

namespace Escapa.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonBase : MonoBehaviour
    {
        public abstract void Action();
    }
}