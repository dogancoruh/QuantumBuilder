using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.GenericPropertyList.Events
{
    public class PropertyValueChangedEventArgs : EventArgs
    {
        public GenericProperty.Data.GenericProperty Property { get; set; }
        public object Value { get; set; }
    }
}
