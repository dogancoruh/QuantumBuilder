using QuantumBuilder.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Shared.Data
{
    public class Project
    {
        public string Name { get; set; }
        public string ProjectPath { get; set; }
        public string OutputPath { get; set; }
        public Obfuscation Obfuscation { get; set; }
        public Signing Signing { get; set; }

        public Project()
        {
            Obfuscation = new Obfuscation();
            Signing = new Signing();
        }
    }
}
