using QuantumBuilder.Shared.GenericPropertyList.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Shared.GenericPropertyList.Events
{
    public class ButtonPressedEventArgs : EventArgs
    {
        public ButtonInfo ButtonInfo { get; set; }
    }
}
