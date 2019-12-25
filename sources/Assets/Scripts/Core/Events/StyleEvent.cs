using Escapa.Utility;
using System;

namespace Escapa.Core.Events
{
    public delegate void StyleEvent(object sender, StyleEventArgs e);

    public sealed class StyleEventArgs:EventArgs
    {
        public Style Style { get; private set; }

        public StyleEventArgs(Style style)
        {
            Style = style;
        }
    }
}
