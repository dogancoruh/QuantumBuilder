using Quantum.Framework.GenericProperties.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Framework.GenericProperties.GenericPropertyCheckListBox.Events
{
    public class PropertyValueChangedEventArgs : EventArgs
    {
        public GenericProperty Property { get; set; }
        public object Value { get; set; }
    }
}
