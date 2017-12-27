using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quantum.Framework.GenericProperties.GenericPropertyListControl.Controls
{
    public class PlaceholderTextBox : TextBox
    {
        // PInvoke
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, string lp);

        private string placeholderText;

        public string PlaceholderText
        {
            get { return placeholderText; }
            set { placeholderText = value; UpdatePlaceholderText(); }
        }

        private void UpdatePlaceholderText()
        {
            if (this.IsHandleCreated && placeholderText != null)
            {
                SendMessage(this.Handle, 0x1501, (IntPtr)1, placeholderText);
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            UpdatePlaceholderText();
        }
    }
}
