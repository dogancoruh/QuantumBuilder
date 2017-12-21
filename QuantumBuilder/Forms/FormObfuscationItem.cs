using QuantumBuilder.Shared.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuantumBuilder.Forms
{
    public partial class FormObfuscationItem : Form
    {
        public ObfuscationItem ObfuscationItem { get; set; }

        public FormObfuscationItem()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "Supported File(s)|*.exe;*.dll|Executable File(s)|*.exe|Assembly File(s)|*.dll"
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxFileName.Text = dialog.FileName;
            }
        }
    }
}
