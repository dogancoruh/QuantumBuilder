using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Framework.GenericProperties.GenericProperty.Data
{
    public class GenericPropertyValueCollection
    {
        private Dictionary<string, object> values;

        public GenericPropertyValueCollection()
        {
            values = new Dictionary<string, object>();
        }

        public T Get<T>(string propertyName, T defaultValue)
        {
            return values.ContainsKey(propertyName) ? (T)values[propertyName] : defaultValue;
        }

        public void Set(string propertyName, object value)
        {
            if (values.ContainsKey(propertyName))
                values[propertyName] = value;
            else
                values.Add(propertyName, value);
        }
    }
}
