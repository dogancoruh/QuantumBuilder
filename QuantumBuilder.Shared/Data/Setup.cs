using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Shared.Data
{
    public class Setup
    {
        public bool Enabled { get; set; }
        public string PluginName { get; set; }
        public Dictionary<string, object> PluginParameters { get; set; }
        public List<SetupItem> Items { get; set; }

        public Setup()
        {
            PluginParameters = new Dictionary<string, object>();
            Items = new List<SetupItem>();
        }
    }
}
