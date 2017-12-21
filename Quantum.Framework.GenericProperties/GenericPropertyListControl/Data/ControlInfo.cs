using Quantum.Framework.GenericProperties.Data;
using Quantum.Framework.GenericProperties.GenericPropertyListControl.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Framework.GenericProperties.GenericPropertyListControl.Data
{
    public class ItemControlInfo
    {
        public ControlType ControlType { get; set; }
        public GenericProperty Property { get; set; }
    }
}
