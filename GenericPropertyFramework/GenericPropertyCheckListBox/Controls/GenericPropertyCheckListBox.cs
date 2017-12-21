using Quantum.GenericProperty.Data;
using Quantum.GenericProperty.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuantumBuilder.Shared.GenericPropertyCheckListBox.Controls
{
    public class GenericPropertyCheckListBox : Panel
    {
        private FlowLayoutPanel panel;

        public GenericPropertyCollection Properties { get; set; }

        public GenericPropertyCheckListBox()
        {
            panel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(panel);
        }

        public override void Refresh()
        {
            base.Refresh();

            foreach (var property in Properties)
            {
                if (property.Browsable &&
                    property.Type == GenericPropertyType.Boolean)
                {
                    var checkBox = new CheckBox()
                    {
                        Checked = property.Value != null ? (bool)property.Value : false,
                        Text = property.DisplayName,
                        Width = 150
                    };
                    panel.Controls.Add(checkBox);
                }
            }
        }
    }
}
