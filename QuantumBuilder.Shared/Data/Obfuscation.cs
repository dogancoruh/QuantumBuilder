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
        public string ProfileName { get; set; }
        public Dictionary<string, object> ProfileParameters { get; set; }
        public List<ObfuscationItem> Items { get; set; }

        public Obfuscation()
        {
            ProfileParameters = new Dictionary<string, object>();
            Items = new List<ObfuscationItem>();
        }
    }
}
