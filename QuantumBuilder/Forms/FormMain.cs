using QuantumBuilder.Forms;
using QuantumBuilder.Shared.Data;
using QuantumBuilder.Shared.Plugin;
using QuantumBuilder.Shared.Utilities;
using QuantumBuilder.Utilities;
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

namespace QuantumBuilder
{
    public partial class FormMain : Form
    {
        Document.Framework.Document document;

        Project project;

        BindingSource bindingSourceObfuscationItems;
        BindingSource bindingSourceSigningItems;

        public FormMain()
        {
            InitializeComponent();
            InitializePlugins();
            InitializeProject();
            InitializeDocument();
            InitializeDataGridViews();
        }

        private void InitializePlugins()
        {
            PluginManager.Instance.LoadPluginsFromPath(AppDomain.CurrentDomain.BaseDirectory);

            // obfuscate profile names
            comboBoxObfuscationProfile.DisplayMember = "Key";
            comboBoxObfuscationProfile.ValueMember = "Value";

            var pluginInfos = PluginManager.Instance.GetPluginInfosForType(PluginType.Obfuscation);
            foreach (var pluginInfo in pluginInfos)
                comboBoxObfuscationProfile.Items.Add(new KeyValuePair<string, object>(pluginInfo.Plugin.DisplayName, pluginInfo));

            // signing profile names
            comboBoxSigningProfile.DisplayMember = "Key";
            comboBoxSigningProfile.ValueMember = "Value";

            pluginInfos = PluginManager.Instance.GetPluginInfosForType(PluginType.Signing);
            foreach (var pluginInfo in pluginInfos)
                comboBoxSigningProfile.Items.Add(new KeyValuePair<string, object>(pluginInfo.Plugin.DisplayName, pluginInfo));
        }

        private void InitializeProject()
        {
            project = new Project();
        }

        private void InitializeDocument()
        {
            document = new Document.Framework.Document
            {
                DefaultTitle = "Quantum Builder"
            };

            document.OnDocumentFileNameChanged += Document_OnDocumentFileNameChanged;
            document.OnDocumentModified += Document_OnDocumentModified;
            document.OnNewDocumentPrompt += Document_OnNewDocumentPrompt;
            document.OnNewDocument += Document_OnNewDocument;
            document.OnOpenDocumentSavePrompt += Document_OnOpenDocumentSavePrompt;
            document.OnOpenDocument += Document_OnOpenDocument;
            document.OnSaveDocument += Document_OnSaveDocument;
            document.OnSaveAsDocumentPrompt += Document_OnSaveAsDocumentPrompt;
            document.OnSaveAsDocument += Document_OnSaveAsDocument;
            document.OnCloseDocument += Document_OnCloseDocument;

            document.RefreshTitle();
        }

        private void InitializeDataGridViews()
        {
            // obfuscation
            bindingSourceObfuscationItems = new BindingSource();
            bindingSourceObfuscationItems.CurrentChanged += BindingSourceObfuscationItems_CurrentChanged;
            bindingSourceObfuscationItems.DataSource = project.Obfuscation.Items;

            dataGridViewObfuscationItems.AutoSize = true;
            dataGridViewObfuscationItems.AutoGenerateColumns = false;
            dataGridViewObfuscationItems.AllowUserToAddRows = false;
            dataGridViewObfuscationItems.AllowUserToDeleteRows = false;
            dataGridViewObfuscationItems.AllowUserToResizeRows = false;
            dataGridViewObfuscationItems.RowHeadersVisible = false;
            dataGridViewObfuscationItems.AutoGenerateColumns = false;
            dataGridViewObfuscationItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridViewObfuscationItems.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                DataPropertyName = "Selected",
                HeaderText = string.Empty,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 32
            });

            dataGridViewObfuscationItems.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "FileName",
                HeaderText = "File Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Resizable = DataGridViewTriState.False,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true
            });

            dataGridViewObfuscationItems.DataSource = bindingSourceObfuscationItems;

            // signing
            bindingSourceSigningItems = new BindingSource();
            bindingSourceSigningItems.CurrentChanged += BindingSourceObfuscationItems_CurrentChanged;
            bindingSourceSigningItems.DataSource = project.Signing.Items;

            dataGridViewSigningItems.AutoSize = true;
            dataGridViewSigningItems.AutoGenerateColumns = false;
            dataGridViewSigningItems.AllowUserToAddRows = false;
            dataGridViewSigningItems.AllowUserToDeleteRows = false;
            dataGridViewSigningItems.AllowUserToResizeRows = false;
            dataGridViewSigningItems.RowHeadersVisible = false;
            dataGridViewSigningItems.AutoGenerateColumns = false;
            dataGridViewSigningItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridViewSigningItems.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                DataPropertyName = "Selected",
                HeaderText = string.Empty,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 32
            });

            dataGridViewSigningItems.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "FileName",
                HeaderText = "File Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Resizable = DataGridViewTriState.False,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true
            });

            dataGridViewSigningItems.DataSource = bindingSourceSigningItems;
        }

        private void RefreshControls()
        {
            if (project != null)
            {
                checkBoxObfuscation.Checked = project.Obfuscation.Enabled;
                checkBoxSigning.Checked = project.Signing.Enabled;
            }
        }

        private void RefreshProject()
        {
            textBoxProjectPath.Text = project.ProjectPath;
            textBoxOutputPath.Text = project.OutputPath;

            // obfuscation
            checkBoxObfuscation.Checked = project.Obfuscation.Enabled;

            var pluginInfos = PluginManager.Instance.GetPluginInfosForType(PluginType.Obfuscation);
            var index = 0;
            foreach (var pluginInfoObfuscation in pluginInfos)
            {
                if (pluginInfoObfuscation.Plugin.Name == project.Obfuscation.ProfileName)
                    comboBoxObfuscationProfile.SelectedIndex = index;

                index++;
            }

            bindingSourceObfuscationItems.DataSource = project.Obfuscation.Items;

            // show/hide obfuscation options panel
            checkBoxSigning.Checked = project.Signing.Enabled;

            if (comboBoxObfuscationProfile.SelectedItem != null)
            {
                var keyValuePair = (KeyValuePair<string, object>)comboBoxObfuscationProfile.SelectedItem;
                var pluginInfoObfuscation_ = (PluginInfo)keyValuePair.Value;
                groupBoxObfuscationOptions.Visible = pluginInfoObfuscation_.PluginProperties.Count > 0;
            }

            // signing
            checkBoxSigning.Checked = project.Signing.Enabled;

            pluginInfos = PluginManager.Instance.GetPluginInfosForType(PluginType.Signing);
            index = 0;
            foreach (var pluginInfoSigning in pluginInfos)
            {
                if (pluginInfoSigning.Plugin.Name == project.Signing.ProfileName)
                    comboBoxSigningProfile.SelectedIndex = index;

                index++;
            }

            bindingSourceSigningItems.DataSource = project.Signing.Items;

            // show/hide signing options panel
            if (comboBoxSigningProfile.SelectedItem != null)
            {
                var keyValuePair = (KeyValuePair<string, object>)comboBoxSigningProfile.SelectedItem;
                var pluginInfoSigning_ = (PluginInfo)keyValuePair.Value;
                groupBoxSigningOptions.Visible = pluginInfoSigning_.PluginProperties.Count > 0;
            }
        }

        private void BindingSourceObfuscationItems_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        private void Document_OnDocumentFileNameChanged(object sender, Document.Framework.Event.DocumentFileNameChangedEventArgs e)
        {
            Text = e.Title;
        }

        private void Document_OnDocumentModified(object sender, Document.Framework.Event.DocumentModifiedEventArgs e)
        {
            Text = e.Title;
            RefreshToolbars();
        }

        private void Document_OnNewDocumentPrompt(object sender, Document.Framework.Event.NewDocumentSavePromptEventArgs e)
        {
            e.DialogResult = MessageBox.Show(this, "Current project is modified.\nDo you want to save it?", "Document Modified", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
        }

        private void Document_OnNewDocument(object sender, Document.Framework.Event.NewDocumentEventArgs e)
        {
            project = new Project();

            document.DocumentFileName = string.Empty;
            document.IsDocumentModified = false;

            RefreshProject();

            bindingSourceObfuscationItems.DataSource = project.Obfuscation.Items;
            bindingSourceObfuscationItems.ResetBindings(false);
            bindingSourceSigningItems.DataSource = project.Signing.Items;
            bindingSourceSigningItems.ResetBindings(false);
        }

        private void Document_OnOpenDocumentSavePrompt(object sender, Document.Framework.Event.OpenDocumentSavePromptEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Document_OnOpenDocument(object sender, Document.Framework.Event.OpenDocumentEventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "Project File(s)|*.qbp"
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                LoadProject(dialog.FileName);
            }
        }

        private void Document_OnSaveDocument(object sender, Document.Framework.Event.SaveDocumentEventArgs e)
        {
            if (e.FileName == string.Empty)
            {
                var dialog = new SaveFileDialog()
                {
                    Filter = "Project File(s)|*.qbp"
                };
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    ProjectLoader.Save(dialog.FileName, project);

                    document.IsDocumentModified = false;
                }
            }
            else
            {
                ProjectLoader.Save(e.FileName, project);

                document.IsDocumentModified = false;
            }
        }

        private void Document_OnSaveAsDocumentPrompt(object sender, Document.Framework.Event.SaveAsDocumentPromptEventArgs e)
        {
            
        }

        private void Document_OnSaveAsDocument(object sender, Document.Framework.Event.DocumentSaveAsEventArgs e)
        {
            var dialog = new SaveFileDialog()
            {
                Filter = "Project File(s)|*.qbp"
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ProjectLoader.Save(e.FileName, project);

                document.DocumentFileName = e.FileName;
                document.IsDocumentModified = false;
            }
        }

        private void Document_OnCloseDocument(object sender, Document.Framework.Event.CloseDocumentEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveProject()
        {
            document.SaveDocument();
        }

        private void LoadProject(string fileName)
        {
            project = ProjectLoader.Load(fileName);

            textBoxProjectPath.Text = project.ProjectPath;
            textBoxOutputPath.Text = project.OutputPath;
            comboBoxObfuscationProfile.SelectedValue = project.Obfuscation.ProfileName;
            comboBoxSigningProfile.SelectedValue = project.Signing.ProfileName;

            RefreshProject();

            document.DocumentFileName = fileName;
            document.IsDocumentModified = false;
        }

        private void ShowAbout()
        {
            new FormAbout().ShowDialog(this);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }

        private void toolStripButtonAbout_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        private void OpenProject()
        {
            document.OpenDocument();
        }

        private void checkBoxObfuscation_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxObfuscationOptions.Enabled =
            groupBoxObfuscationItems.Enabled =
            comboBoxObfuscationProfile.Enabled = checkBoxObfuscation.Checked;

            project.Obfuscation.Enabled = checkBoxObfuscation.Checked;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewProject();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProject();
        }

        private void NewProject()
        {
            document.NewDocument();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            document.IsDocumentModified = true;
        }

        private void toolStripButtonSaveAs_Click(object sender, EventArgs e)
        {
            SaveProjectAs();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProjectAs();
        }

        private void SaveProjectAs()
        {
            document.SaveAsDocument();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog()
            {
                SelectedPath = project.ProjectPath,
                Description = "Select folder to import assembly file(s)"
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                foreach (var fileName in Directory.GetFiles(dialog.SelectedPath, "*.exe"))
                {
                    var fileName_ = PathHelper.GetRelativeFileName(project.ProjectPath, fileName);

                    project.Obfuscation.Items.Add(new ObfuscationItem()
                    {
                        Selected = true,
                        FileName = fileName_
                    });
                }

                foreach (var fileName in Directory.GetFiles(dialog.SelectedPath, "*.dll"))
                {
                    var fileName_ = PathHelper.GetRelativeFileName(project.ProjectPath, fileName);

                    project.Obfuscation.Items.Add(new ObfuscationItem()
                    {
                        Selected = true,
                        FileName = fileName_
                    });
                }

                bindingSourceObfuscationItems.ResetBindings(false);
            }
        }

        private void comboBoxObfuscationPlugin_SelectedIndexChanged(object sender, EventArgs e)
        {
            var keyValuePair = (KeyValuePair<string, object>)comboBoxObfuscationProfile.SelectedItem;
            var pluginInfo = (PluginInfo)keyValuePair.Value;

            genericPropertyCheckListBoxObfuscation.Properties = pluginInfo.PluginProperties;
            genericPropertyCheckListBoxObfuscation.Values = project.Obfuscation.ProfileParameters;
            genericPropertyCheckListBoxObfuscation.Refresh();

            project.Obfuscation.ProfileName = pluginInfo.Plugin.Name;

            if (pluginInfo.PluginProperties != null)
                groupBoxObfuscationOptions.Visible = pluginInfo.PluginProperties.Count > 0;
            else
                groupBoxObfuscationOptions.Visible = false;

            document.IsDocumentModified = true;
        }

        private void toolStripButtonAddObfuscationItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "Assembly File(s)|*.exe;*.dll|Executable File(s)|*.exe|Class Library File(s)|*.dll",
                Multiselect = true
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                foreach (var fileName in dialog.FileNames)
                {
                    var fileName_ = PathHelper.GetRelativeFileName(project.ProjectPath, fileName);

                    project.Obfuscation.Items.Add(new ObfuscationItem()
                    {
                        Selected = true,
                        FileName = fileName_
                    });
                }

                bindingSourceObfuscationItems.ResetBindings(false);

                document.IsDocumentModified = true;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadProject("c:\\test.qbp");
        }

        private void buildAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuildAll();
        }

        private void toolStripButtonBuildAll_Click(object sender, EventArgs e)
        {
            BuildAll();
        }

        private void BuildAll()
        {
            var dialog = new FormBuild
            {
                Project = project
            };
            dialog.ShowDialog(this);
        }

        private void buttonBrowseProjectPath_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog()
            {
                SelectedPath = textBoxProjectPath.Text
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxProjectPath.Text = dialog.SelectedPath;
            }
        }

        private void buttonBrowseOutputPath_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog()
            {
                SelectedPath = textBoxOutputPath.Text
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var path = PathHelper.GetRelativePath(project.ProjectPath, dialog.SelectedPath);
                textBoxOutputPath.Text = path;
            }
        }

        private void toolStripButtonSaveProject_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

        private void textBoxProjectPath_TextChanged(object sender, EventArgs e)
        {
            project.ProjectPath = ((TextBox)sender).Text;
            document.IsDocumentModified = true;
        }

        private void textBoxOutputPath_TextChanged(object sender, EventArgs e)
        {
            project.OutputPath = ((TextBox)sender).Text;
            document.IsDocumentModified = true;
        }

        private void toolStripButtonRemoveObfuscationItem_Click(object sender, EventArgs e)
        {
            if (bindingSourceObfuscationItems.Current != null)
            {
                var obfuscationItem = (ObfuscationItem)bindingSourceObfuscationItems.Current;
                project.Obfuscation.Items.Remove(obfuscationItem);
                bindingSourceObfuscationItems.ResetBindings(false);
                document.IsDocumentModified = true;
            }
        }

        private void checkBoxSigning_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxSigningOptions.Enabled =
            groupBoxSigningItems.Enabled =
            comboBoxSigningProfile.Enabled = checkBoxSigning.Checked;

            document.IsDocumentModified = true;

            project.Signing.Enabled = checkBoxSigning.Checked;
        }

        private void comboBoxSigningProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            var keyValuePair = (KeyValuePair<string, object>)comboBoxSigningProfile.SelectedItem;
            var pluginInfo = (PluginInfo)keyValuePair.Value;

            genericPropertyListControlSigning.Properties = pluginInfo.PluginProperties;
            genericPropertyListControlSigning.Values = project.Signing.ProfileParameters;
            genericPropertyListControlSigning.Refresh();

            project.Signing.ProfileName = pluginInfo.Plugin.Name;

            if (pluginInfo.PluginProperties != null)
                groupBoxSigningOptions.Visible = pluginInfo.PluginProperties.Count > 0;
            else
                groupBoxSigningOptions.Visible = false;

            document.IsDocumentModified = true;
        }

        private void genericPropertyCheckListBoxObfuscation_OnPropertyValueChanged(object sender, Quantum.Framework.GenericProperties.GenericPropertyCheckListBox.Events.PropertyValueChangedEventArgs e)
        {
            document.IsDocumentModified = true;
        }

        private void toolStripButtonAddSigningItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "Assembly File(s)|*.exe;*.dll|Executable File(s)|*.exe|Class Library File(s)|*.dll",
                Multiselect = true
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                foreach (var fileName in dialog.FileNames)
                {
                    var fileName_ = PathHelper.GetRelativeFileName(project.ProjectPath, fileName);

                    project.Signing.Items.Add(new SigningItem()
                    {
                        Selected = true,
                        FileName = fileName_
                    });
                }

                bindingSourceSigningItems.ResetBindings(false);

                document.IsDocumentModified = true;
            }
        }

        private void toolStripButtonRemoveSigningItem_Click(object sender, EventArgs e)
        {
            if (bindingSourceSigningItems.Current != null)
            {
                var signingItem = (SigningItem)bindingSourceSigningItems.Current;
                project.Signing.Items.Remove(signingItem);
                bindingSourceSigningItems.ResetBindings(false);
            }
        }

        private void RefreshToolbars()
        {
            saveToolStripMenuItem.Enabled =
            toolStripButtonSave.Enabled = document.IsDocumentModified;
        }

        private void dataGridViewObfuscationItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            document.IsDocumentModified = true;
        }

        private void genericPropertyListControlSigning_OnPropertyValueChanged(object sender, Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Events.PropertyValueChangeEventArgs e)
        {
            document.IsDocumentModified = true;
        }
    }
}
