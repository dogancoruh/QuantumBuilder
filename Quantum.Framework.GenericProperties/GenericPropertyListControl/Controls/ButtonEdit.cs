using Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Data;
using Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl
{
    public class ButtonEdit : UserControl
    {
        public event EventHandler OnTextChanged;
        public event EventHandler<ButtonPressedEventArgs> OnButtonClick;

        private UserControl panel;
        private TextBox textBox;

        public new string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }


        public List<ButtonInfo> Buttons { get; set; }

        public ButtonEdit()
        {
            Margin = Padding.Empty;
            Padding = Padding.Empty;

            Buttons = new List<ButtonInfo>()
            {
                new ButtonInfo()
                {
                    Text = "...",
                    Tag = "default"
                }
            };

            textBox = new TextBox()
            {
                Dock = DockStyle.Fill,
                Height = 25
            };

            textBox.TextChanged += TextBox_TextChanged;

            Controls.Add(textBox);

            panel = new UserControl()
            {
                Dock = DockStyle.Right,
                Margin = Padding.Empty,
                Padding = Padding.Empty,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };
            Controls.Add(panel);

            Layout += ButtonEdit_Layout;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonEdit_Layout(object sender, LayoutEventArgs e)
        {
            // get buttons
            var buttonControls = new List<Button>();
            foreach (var control in panel.Controls)
                buttonControls.Add((Button)control);

            foreach (var buttonControl in buttonControls)
            {
                buttonControl.Click -= Button_Click;
                panel.Controls.Remove(buttonControl);
            }

            foreach (var buttonInfo in Buttons)
            {
                var button = new Button()
                {
                    Dock = DockStyle.Right,
                    Text = buttonInfo.Text,
                    Image = buttonInfo.Image,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    AutoSize = true,
                    Margin = Padding.Empty,
                    Padding = Padding.Empty
                };

                button.Click += Button_Click;

                panel.Controls.Add(button);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var buttonInfo = (ButtonInfo)button.Tag;

            OnButtonClick?.Invoke(this, new ButtonPressedEventArgs()
            {
                ButtonInfo = buttonInfo
            });
        }

        public override void Refresh()
        {
            base.Refresh();
            
        }
    }
}
