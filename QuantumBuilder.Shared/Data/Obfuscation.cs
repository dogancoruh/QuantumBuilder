using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Shared.Data
{
    public class Obfuscation
    {
        public bool Enabled { get; set; }
        public string PluginName { get; set; }
        public Dictionary<string, object> PluginParameters { get; set; }
        public List<ObfuscationItem> Items { get; set; }

        public Obfuscation()
        {
            PluginParameters = new Dictionary<string, object>();
            Items = new List<ObfuscationItem>();
        }
    }
}
