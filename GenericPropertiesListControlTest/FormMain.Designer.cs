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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.genericPropertyListControl1 = new Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.GenericPropertyListControl();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(71, 107);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(442, 499);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(442, 468);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.genericPropertyListControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 31);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // genericPropertyListControl1
            // 
            this.genericPropertyListControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.genericPropertyListControl1.Location = new System.Drawing.Point(3, 16);
            this.genericPropertyListControl1.Margin = new System.Windows.Forms.Padding(0);
            this.genericPropertyListControl1.MaximumSize = new System.Drawing.Size(0, 200);
            this.genericPropertyListControl1.Name = "genericPropertyListControl1";
            genericPropertyListOptions1.AutoSize = true;
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
            this.genericPropertyListControl1.Size = new System.Drawing.Size(436, 12);
            this.genericPropertyListControl1.TabIndex = 2;
            this.genericPropertyListControl1.Values = null;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 676);
            this.Controls.Add(this.panel1);
            this.Name = "FormMain";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GenericPropertyListControl Test Application";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.GenericPropertyListControl genericPropertyListControl1;
    }
}

