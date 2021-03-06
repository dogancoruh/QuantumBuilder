﻿using Quantum.Framework.GenericProperties.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Shared.Data
{
    public class Signing
    {
        public bool Enabled { get; set; }
        public string PluginName { get; set; }
        public Dictionary<string, object> PluginParameters { get; set; }
        public List<SigningItem> Items { get; set; }

        public Signing()
        {
            PluginParameters = new Dictionary<string, object>();
            Items = new List<SigningItem>();
        }
    }
}
