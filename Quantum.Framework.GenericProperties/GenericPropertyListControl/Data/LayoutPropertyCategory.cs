using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quantum.Framework.GenericProperties.GenericPropertyListControl.Data
{
    public class LayoutPropertyCategory : LayoutItem
    {
        public Panel Seperator { get; set; }

        public List<LayoutItem> Items { get; set; }

        public LayoutPropertyCategory()
        {
            Items = new List<LayoutItem>();
        }
    }
}
