using Newtonsoft.Json.Linq;
using Quantum.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Framework.GenericProperty.Data
{

    public class GenericPropertyCollection : List<GenericProperty>, IDeepCloneable<GenericPropertyCollection>
    {
        public T Get<T>(string name)
        {
            return Get<T>(name, default(T));
        }

        public T Get<T>(string name, T defaultValue)
        {
            foreach (var item in this)
                if (item.Name == name)
                    return (T)item.Value;

            return defaultValue;
        }

        public void Set(string name, object value)
        {
            foreach (var item in this)
            {
                if (item.Name == name)
                {
                    item.Value = value;
                    return;
                }
            }

            // if it does not contains same key, add it 
            Add(new GenericProperty()
            {
                Name = name,
                Value = value
            });

            //throw new Exception("Property is not found");
        }

        public GenericPropertyCollection GetPropertiesByScope(string scopeName)
        {
            var result = new GenericPropertyCollection();

            foreach (var item in this)
            {
                if (item.ScopeName == scopeName)
                    result.Add(item);
            }

            return result;
        }

        public GenericPropertyCollection Clone()
        {
            var properties = new GenericPropertyCollection();

            foreach (var item in this)
                properties.Add(item.Clone());

            return properties;
        }

        public void CopyTo(GenericPropertyCollection properties)
        {
            foreach (var sourceProperty in this)
            {
                foreach (var targetProperty in properties)
                {
                    if(sourceProperty.Equals(targetProperty))
                    {
                        targetProperty.Value = sourceProperty.Value;
                        break;
                    }
                }
            }
        }
    }
}
