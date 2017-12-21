using Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Events
{
    public class ButtonPressedEventArgs : EventArgs
    {
        public ButtonInfo ButtonInfo { get; set; }
    }
}
