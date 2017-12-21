using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Shared.Events
{
    public class PluginLogEventArgs : EventArgs
    {
        public string Prefix { get; set; }
        public string Text { get; set; }
    }
}
