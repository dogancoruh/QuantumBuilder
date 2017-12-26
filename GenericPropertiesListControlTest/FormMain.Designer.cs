using Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Enum;

namespace GenericPropertiesListControlTest
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Data.GenericPropertyListOptions genericPropertyListOptions1 = new Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Data.GenericPropertyListOptions();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.genericPropertyListControl1 = new Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.GenericPropertyListControl();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.genericPropertyListControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 317);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(512, 342);
            this.listBox1.TabIndex = 1;
            // 
            // genericPropertyListControl1
            // 
            this.genericPropertyListControl1.AutoScroll = true;
            this.genericPropertyListControl1.Controls.Add(this.button3);
            this.genericPropertyListControl1.Controls.Add(this.button1);
            this.genericPropertyListControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.genericPropertyListControl1.Location = new System.Drawing.Point(0, 0);
            this.genericPropertyListControl1.Margin = new System.Windows.Forms.Padding(0);
            this.genericPropertyListControl1.Name = "genericPropertyListControl1";
            genericPropertyListOptions1.AutoSize = false;
            genericPropertyListOptions1.CategoryBottomMargin = 7;
            genericPropertyListOptions1.CategoryLeftMargin = 5;
            genericPropertyListOptions1.CategoryRightMargin = 5;
            genericPropertyListOptions1.CategorySeperatorColor = System.Drawing.Color.Silver;
            genericPropertyListOptions1.CategorySeperatorHeight = 1;
            genericPropertyListOptions1.CategoryTitleFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            genericPropertyListOptions1.CategoryTitleForeColor = System.Drawing.Color.Silver;
            genericPropertyListOptions1.CategoryTitleHeight = 21;
            genericPropertyListOptions1.CategoryTopMargin = 5;
            genericPropertyListOptions1.ItemGap = 3;
            genericPropertyListOptions1.ItemHeight = 25;
            genericPropertyListOptions1.ItemLabelForeColor = System.Drawing.Color.Empty;
            genericPropertyListOptions1.ItemLeftMargin = 5;
            genericPropertyListOptions1.ItemRightMargin = 5;
            genericPropertyListOptions1.ScrollBarPadding = 3;
            genericPropertyListOptions1.SeperatorOffset = 20;
            genericPropertyListOptions1.SeperatorOffsetType = Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Enum.SeperatorOffsetType.Percent;
            genericPropertyListOptions1.SeperatorPadding = 5;
            genericPropertyListOptions1.ViewMode = Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Enum.ViewMode.CategoryList;
            this.genericPropertyListControl1.Options = genericPropertyListOptions1;
            this.genericPropertyListControl1.Properties = null;
            this.genericPropertyListControl1.Size = new System.Drawing.Size(536, 314);
            this.genericPropertyListControl1.TabIndex = 0;
            this.genericPropertyListControl1.Values = null;
            this.genericPropertyListControl1.OnPropertyValueChanged += new System.EventHandler<Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Events.PropertyValueChangeEventArgs>(this.genericPropertyListControl1_OnPropertyValueChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(68, 245);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 66);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(413, 245);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 676);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.genericPropertyListControl1);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GenericPropertyListControl Test Application";
            this.genericPropertyListControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.GenericPropertyListControl genericPropertyListControl1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
    }
}

