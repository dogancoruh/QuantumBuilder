using Newtonsoft.Json.Linq;
using Quantum.GenericProperty.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.GenericProperty.Utilities
{
    public class GenericPropertyLoader
    {
        public static void Save(string fileName, GenericPropertyCollection properties)
        {
            var jArrayProperties = GenericPropertySerializer.SerializeToArray(properties);
            File.WriteAllText(fileName, jArrayProperties.ToString(), Encoding.UTF8);
        }

        public static void Load(GenericPropertyCollection properties, string fileName)
        {
            var contents = File.ReadAllText(fileName, Encoding.UTF8);
            var jArrayProperties = JArray.Parse(contents);
            GenericPropertySerializer.Deserialize(properties, jArrayProperties);
        }
    }
}
