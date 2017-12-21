using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Framework.GenericProperties.GenericPropertyListControl.Events
{
    public class ColorChangedEventArgs : EventArgs
    {
        public Color Color { get; set; }
    }
}
