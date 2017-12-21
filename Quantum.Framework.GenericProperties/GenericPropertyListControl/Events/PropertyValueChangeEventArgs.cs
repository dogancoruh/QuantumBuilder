using Quantum.Framework.GenericProperties.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Events
{
    public class PropertyValueChangeEventArgs : EventArgs
    {
        public GenericProperty Property { get; set; }
        public object Value { get; set; }
    }
}
