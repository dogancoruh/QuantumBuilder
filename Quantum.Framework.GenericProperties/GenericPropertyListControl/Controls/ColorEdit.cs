using Quantum.Framework.GenericProperties.GenericPropertyListControl.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl
{
    public class ColorEdit : UserControl
    {
        const int COLOR_BOX_WIDTH = 40;

        public event EventHandler<ColorChangedEventArgs> OnColorChanged;

        private Panel panelColor;
        private Label labelText;
        private Button button;

        private Color color = Color.White;

        public Color Color
        {
            get { return color; }
            set { color = value; RefreshColor(); }
        }

        public ColorEdit()
        {
            Padding = new Padding(3);

            labelText = new Label()
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = ColorToString(color)
            };
            Controls.Add(labelText);

            panelColor = new Panel()
            {
                BackColor = color,
                Dock = DockStyle.Left,
                Width = COLOR_BOX_WIDTH,
            };
            Controls.Add(panelColor);

            button = new Button()
            {
                Dock = DockStyle.Right,
                Font = new Font("Microsoft Sans Serif", 7),
                Text = "...",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                TextAlign = ContentAlignment.MiddleCenter
            };
            button.Click += Button_Click;
            Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var dialog = new ColorDialog()
            {
                Color = color
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                color = dialog.Color;
                RefreshColor();

                OnColorChanged?.Invoke(this, new ColorChangedEventArgs()
                {
                    Color = color
                });
            }
        }

        private void RefreshColor()
        {
            panelColor.BackColor = color;
            labelText.Text = ColorToString(color);
        }

        private string ColorToString(Color color)
        {
            if (!color.IsKnownColor)
                return color.R + "; " + color.G + "; " + color.B;
            else
                return color.ToKnownColor().ToString();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var pen = new Pen(Color.FromArgb(255, 122, 122, 122));
            var rectangle = new Rectangle(0, 0, Width - 1, Height - 1);
            e.Graphics.DrawRectangle(pen, rectangle);
        }
    }
}
