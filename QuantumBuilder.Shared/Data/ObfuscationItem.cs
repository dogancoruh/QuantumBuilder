using Quantum.Framework.GenericProperties.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Shared.Data
{
    public class ObfuscationItem : IDeepCloneable<ObfuscationItem>
    {
        public string FileName { get; set; }
        public bool Selected { get; set; }

        public ObfuscationItem Clone()
        {
            return new ObfuscationItem()
            {
                FileName = FileName,
                Selected = Selected
            };
        }
    }
}
