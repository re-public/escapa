using System;
using Escapa.Utility;

namespace Escapa.Events
{
    public delegate void StyleEvent(StyleEventArgs e);
    
    public sealed class StyleEventArgs : EventArgs
    {
        public Theme Theme { get; }

        public StyleEventArgs(Theme theme)
        {
            Theme = theme;
        }
    }
}