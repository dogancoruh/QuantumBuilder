using Quantum.Framework.GenericProperties.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Framework.GenericProperties.Data
{
    public class GenericPropertyEnumItem : IDeepCloneable<GenericPropertyEnumItem>
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public GenericPropertyEnumItem()
        {

        }

        public GenericPropertyEnumItem(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public GenericPropertyEnumItem Clone()
        {
            return new GenericPropertyEnumItem()
            {
                Name = Name,
                Value = Value
            };
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
