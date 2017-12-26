using QuantumBuilder.Shared.Data;
using QuantumBuilder.Shared.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuantumBuilder.Forms
{
    public partial class FormBuild : Form
    {
        public Project Project { get; set; }

        public FormBuild()
        {
            InitializeComponent();
        }

        private void FormBuild_Load(object sender, EventArgs e)
        {

        }

        private void Log(string prefix, string text)
        {
            listBoxLog.Items.Add("[" + prefix + "] " + text);
            listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
            Application.DoEvents();
        }

        private void Error(string prefix, string text)
        {
            listBoxLog.Items.Add("[" + prefix + "] " + text);
            Application.DoEvents();
        }

        private Plugin activePlugin;

        private void FormBuild_Shown(object sender, EventArgs e)
        {
            var buildPath = Path.Combine(Path.GetTempPath(), "QuantumBuilder", "build", Project.Name);

            // obfuscation
            if (Project.Obfuscation.Enabled)
            {
                Log("obfuscation", "Obfuscation started...");

                if (!Directory.Exists(buildPath))
                        Directory.CreateDirectory(buildPath);

                var pluginInfo = PluginManager.Instance.GetPluginInfoByName(Project.Obfuscation.PluginName);
                if (pluginInfo != null)
                {
                    activePlugin = pluginInfo.Plugin;
                    activePlugin.OnLog += Plugin_OnLog;
                    activePlugin.OnError += Plugin_OnError;
                    activePlugin.Execute(new Dictionary<string, object>
                    {
                        ["project"] = Project,
                        ["buildPath"] = buildPath
                    });
                }
                else
                {
                    Error("obfuscation", "Obfuscation profile not selected!");
                }

                Log("obfuscation", "Obfuscation ended");
            }
            else
                Log("obfuscation", "Obfuscation disabled. Passing...");

            // signing
            if (Project.Signing.Enabled)
            {
                Log("signing", "Signing started...");

                var pluginInfo = PluginManager.Instance.GetPluginInfoByName(Project.Signing.PluginName);
                if (pluginInfo != null)
                {
                    activePlugin = pluginInfo.Plugin;
                    activePlugin.OnLog += Plugin_OnLog;
                    activePlugin.OnError += Plugin_OnError;
                    activePlugin.Execute(new Dictionary<string, object>
                    {
                        ["project"] = Project,
                        ["buildPath"] = buildPath
                    });
                }
                else
                {
                    Error("signing", "Signing profile not selected!");
                }

                Log("signing", "Signing ended");
            }
            else
                Log("signing", "Signing disabled. Passing...");

            button.Text = "Close";
            activePlugin = null;
        }

        private void Plugin_OnLog(object sender, Shared.Events.PluginLogEventArgs e)
        {
            Log(e.Prefix, e.Text);
        }

        private void Plugin_OnError(object sender, Shared.Events.PluginErrorEventArgs e)
        {
            Error(e.Prefix, e.Text);
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (activePlugin != null)
            {
                activePlugin.Terminate();

                activePlugin = null;
                button.Text = "Close";
            }
            else
                Close();
        }
    }
}
