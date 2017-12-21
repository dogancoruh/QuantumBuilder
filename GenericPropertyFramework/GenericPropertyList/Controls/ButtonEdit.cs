using QuantumBuilder.Shared.GenericPropertyList.Data;
using QuantumBuilder.Shared.GenericPropertyList.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuantumBuilder.Shared.GenericPropertyList.Controls
{
    public class ButtonEdit : Panel
    {
        public event EventHandler<ButtonPressedEventArgs> OnButtonClick;

        private TextBox textBox;

        public List<ButtonInfo> Buttons { get; set; }

        public ButtonEdit()
        {
            Buttons = new List<ButtonInfo>();

            textBox = new TextBox()
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(textBox);
        }

        public void Refresh()
        {
            // get buttons
            var buttonControls = new List<Button>();
            foreach (var control in Controls)
                if (control is Button)
                    buttonControls.Add((Button)control);

            foreach (var buttonControl in buttonControls)
                Controls.Remove(buttonControl);

            foreach (var buttonInfo in Buttons)
            {
                Controls.Add(new Button()
                {
                    Dock = DockStyle.Right,
                    Image = buttonInfo.Image
                });
            }
        }
    }
}
