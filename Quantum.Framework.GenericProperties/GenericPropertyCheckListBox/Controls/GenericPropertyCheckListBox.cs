using Quantum.Framework.GenericProperties.Data;
using Quantum.Framework.GenericProperties.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quantum.Framework.GenericProperties.Controls.GenericPropertyCheckListBox.Controls
{
    public class GenericPropertyCheckListBox : Panel
    {
        public event EventHandler<Quantum.Framework.GenericProperties.GenericPropertyCheckListBox.Events.PropertyValueChangedEventArgs> OnPropertyValueChanged;

        private FlowLayoutPanel panel;

        public GenericPropertyCollection Properties { get; set; }
        public Dictionary<string, object> Values { get; set; }

        public GenericPropertyCheckListBox()
        {
            panel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                AutoScroll = true
            };
            Controls.Add(panel);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var property = (GenericProperty)checkBox.Tag;

            bool value = checkBox.Checked;

            if (Values != null)
                Values[property.Name] = value;
            else
                property.Value = value;

            OnPropertyValueChanged?.Invoke(this, new Quantum.Framework.GenericProperties.GenericPropertyCheckListBox.Events.PropertyValueChangedEventArgs()
            {
                Property = property,
                Value = value
            });
        }

        override public void Refresh()
        {
            base.Refresh();

            var controlsToRemove = new List<Control>();
            foreach (Control control in panel.Controls)
                if (control is CheckBox)
                    controlsToRemove.Add(control);

            foreach (var control in controlsToRemove)
                panel.Controls.Remove(control);

            if (Properties != null)
            {
                foreach (var property in Properties)
                {
                    if (property.Browsable &&
                        property.Type == GenericPropertyType.Boolean)
                    {
                        bool value = property.Value != null ? (bool)property.Value : (bool)property.DefaultValue;

                        if (Values != null && Values.ContainsKey(property.Name))
                            value = (bool)Values[property.Name];

                        var checkBox = new CheckBox()
                        {
                            Checked = value,
                            Text = property.DisplayName,
                            Width = 150,
                            Tag = property
                        };
                        checkBox.CheckedChanged += CheckBox_CheckedChanged;
                        panel.Controls.Add(checkBox);
                    }
                }
            }
        }
    }
}
