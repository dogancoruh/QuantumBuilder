using Quantum.Framework.GenericProperties.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Shared.Plugin
{
    public class PluginInfo
    {
        public string FileName { get; set; }
        public Plugin Plugin { get; set; }
        public GenericPropertyCollection PluginProperties { get; set; }
    }
}
