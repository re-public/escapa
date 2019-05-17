using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    /// <summary>
    /// Base class for buttons.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class ButtonBase : MonoBehaviour
    {
        /// <summary>
        /// Action when button was clicked.
        /// </summary>
        public abstract void Action();
    }
}