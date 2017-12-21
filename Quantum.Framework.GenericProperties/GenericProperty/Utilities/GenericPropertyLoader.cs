using Newtonsoft.Json.Linq;
using Quantum.Framework.GenericProperties.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Framework.GenericProperties.Utilities
{
    public class GenericPropertyLoader
    {
        public static void Save(string fileName, GenericPropertyCollection properties)
        {
            var jArrayProperties = GenericPropertySerializer.SerializePropertiesToArray(properties);
            File.WriteAllText(fileName, jArrayProperties.ToString(), Encoding.UTF8);
        }

        public static void Load(GenericPropertyCollection properties, string fileName)
        {
            var contents = File.ReadAllText(fileName, Encoding.UTF8);
            var jArrayProperties = JArray.Parse(contents);
            GenericPropertySerializer.DeserializePropertiesFromArray(properties, jArrayProperties);
        }
    }
}
