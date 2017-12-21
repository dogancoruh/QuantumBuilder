//#define COLORIFY

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quantum.GenericPropertyList.Events;
using Quantum.GenericPropertyList.Enum;
using Quantum.GenericPropertyList.Data;
using Quantum.GenericProperty.Enum;
using Quantum.GenericProperty.Utilities;
using Quantum.GenericPropertyList.Utilities;
using Quantum.GenericProperty.Data;
using QuantumBuilder.Shared.GenericPropertyList.Controls;
using QuantumBuilder.Shared.GenericPropertyList.Events;
using QuantumBuilder.Shared.GenericPropertyList.Data;

namespace Quantum.GenericPropertyList.Controls
{
    public partial class GenericPropertyListControl : ScrollableControl
    {
        public event EventHandler<PropertyValueChangeEventArgs> OnPropertyValueChange;
        public event EventHandler<PropertyValueChangingEventArgs> OnPropertyValueChanging;
        public event EventHandler<Quantum.GenericPropertyList.Events.PropertyValueChangedEventArgs> OnPropertyValueChanged;

        #region Private Variables

        private List<Control> categoryLabels;
        private List<Control> categorySeperators;
        private List<Control> itemLabels;
        private List<Control> itemEdits;

        #endregion

        #region Properties

        public GenericPropertyListOptions Options { get; set; }
        public GenericPropertyCollection Properties { get; set; }

        #endregion

        public GenericPropertyListControl()
        {
            Margin = new Padding(0);
            Padding = new Padding(0);
            AutoScrollMargin = new Size(0, 0);
            AutoScrollMinSize = new Size(0, 0);

            Options = new GenericPropertyListOptions();

            Layout += GenericPropertyListControl_Layout;

            InitializeComponent();
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

        private void GenericPropertyListControl_Layout(object sender, LayoutEventArgs e)
        {
            LayoutControls();
        }

        public new void Refresh()
        {
            if (Properties != null)
            {
                SuspendLayout();
                // dispose existing controls

                // create controls for categories and items
                categoryLabels = new List<Control>();
                categorySeperators = new List<Control>();
                itemLabels = new List<Control>();
                itemEdits = new List<Control>();

                var categoryOffsetX = Options.CategoryLeftMargin;

                var seperatorOffsetX = Options.SeperatorOffset;
                if (Options.SeperatorOffsetType == SeperatorOffsetType.Percent)
                    seperatorOffsetX = MathHelper.RoundInt((double)Width * (double)seperatorOffsetX / (double)100);

                var offsetY = 0;

                var categoryNames = Properties.Select(x => x.CategoryName).Distinct();
                foreach (var categoryName in categoryNames)
                {
                    var items = Properties.Where(d => d.CategoryName == categoryName && d.Browsable).ToList();

                    if (Options.ViewMode == ViewMode.CategoriesAndItems && items.Count > 0)
                    {
                        offsetY += Options.CategoryTopMargin;

                        // category title
                        var labelCategoryTitle = new Label()
                        {
                            Text = Properties.Where(x => x.CategoryName == categoryName).First().CategoryDisplayName,
                            Font = Options.CategoryTitleFont,
                            AutoSize = true,
                            Location = new Point(categoryOffsetX, offsetY),
                            Size = new Size(Width - Options.CategoryLeftMargin - Options.CategoryRightMargin, Options.CategoryTitleHeight),
#if COLORIFY
                            BackColor = Color.Red
#endif
                        };
                        Controls.Add(labelCategoryTitle);
                        categoryLabels.Add(labelCategoryTitle);

                        offsetY += labelCategoryTitle.Size.Height;

                        // category seperator
                        var categorySeperatorWidth = Width - Options.CategoryLeftMargin - Options.CategoryRightMargin;
                        //if (VerticalScroll.Visible)
                        //    seperatorWidth -= SystemInformation.VerticalScrollBarWidth + SCROLLBAR_PADDING;

                        var panelCategorySeperator = new Panel
                        {
                            BackColor = Options.CategorySeperatorColor,
                            Location = new Point(categoryOffsetX, offsetY),
                            Size = new Size(categorySeperatorWidth, Options.CategorySeperatorHeight)
                        };
                        Controls.Add(panelCategorySeperator);
                        categorySeperators.Add(panelCategorySeperator);

                        offsetY += Options.CategoryBottomMargin;
                    }

                    // items
                    foreach (var item in items)
                    {
                        // label
                        var labelPropertyTitle = new Label()
                        {
                            Text = item.DisplayName,
                            AutoSize = true,
                            AutoEllipsis = true,
                            Location = new Point(Options.ItemLeftMargin, offsetY),
                            Size = new Size(seperatorOffsetX - Options.ItemLeftMargin - Options.SeperatorPadding, Options.ItemHeight),
                            
#if COLORIFY
                            BackColor = ColorRandomizer.GetRandomColor()
#endif
                        };
                        Controls.Add(labelPropertyTitle);
                        itemLabels.Add(labelPropertyTitle);

                        // editor
                        var itemEditWidth = Width - seperatorOffsetX - Options.ItemRightMargin;
                        //if (VerticalScroll.Visible)
                        //    itemEditWidth -= SystemInformation.VerticalScrollBarWidth + SCROLLBAR_PADDING;

                        if (item.Type == GenericPropertyType.String)
                        {
                            var textBox = new TextBox()
                            {
                                Text = Convert.ToString(item.Value),
                                Tag = item,
                                Location = new Point(seperatorOffsetX, offsetY),
                                Size = new Size(itemEditWidth, Options.ItemHeight)
                            };
                            textBox.TextChanged += TextBox_TextChanged;
                            Controls.Add(textBox);
                            itemEdits.Add(textBox);
                        }
                        else if (item.Type == GenericPropertyType.Integer)
                        {
                            var spinEdit = new NumericUpDown()
                            {
                                Value = Convert.ToInt32(item.Value),
                                Tag = item,
                                Location = new Point(seperatorOffsetX, offsetY),
                                Size = new Size(itemEditWidth, Options.ItemHeight)
                            };
                            spinEdit.ValueChanged += SpinEdit_Integer_ValueChanged;

                            spinEdit.Minimum = Convert.ToInt32(item.MinimumValue);
                            spinEdit.Maximum = Convert.ToInt32(item.MaximumValue);

                            Controls.Add(spinEdit);
                            itemEdits.Add(spinEdit);
                        }
                        else if (item.Type == GenericPropertyType.Decimal)
                        {
                            var numericUpDown = new NumericUpDown()
                            {
                                Value = Convert.ToDecimal(item.Value),
                                Tag = item,
                                Location = new Point(seperatorOffsetX, offsetY),
                                Size = new Size(itemEditWidth, Size.Height)
                            };
                            numericUpDown.ValueChanged += SpinEdit_Float_ValueChanged;

                            numericUpDown.Minimum = Convert.ToDecimal(item.MinimumValue);
                            numericUpDown.Maximum = Convert.ToDecimal(item.MaximumValue);

                            Controls.Add(numericUpDown);
                            itemEdits.Add(numericUpDown);
                        }
                        else if (item.Type == GenericPropertyType.Boolean)
                        {
                            var checkEdit = new CheckBox()
                            {
                                Checked = Convert.ToBoolean(item.Value),
                                Text = string.Empty,
                                Tag = item,
                                Location = new Point(seperatorOffsetX, offsetY),
                                Size = new Size(itemEditWidth, Size.Height)
                            };
                            checkEdit.CheckedChanged += CheckEdit_CheckedChanged;

                            Controls.Add(checkEdit);
                            itemEdits.Add(checkEdit);
                        }
                        else if (item.Type == GenericPropertyType.Enumeration)
                        {
                            var comboBox = new ComboBox
                            {
                                Tag = item,
                                Location = new Point(seperatorOffsetX, offsetY),
                                Size = new Size(itemEditWidth, Size.Height),
                            };
                            comboBox.SelectedValueChanged += ComboBoxEdit_SelectedValueChanged;

                            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

                            var selectedIndex = -1;
                            var index = 0;

                            foreach (var enumItem in item.EnumItems)
                            {
                                comboBox.Items.Add(enumItem);

                                if (enumItem.Value.Equals(item.Value))
                                    selectedIndex = index;

                                index++;
                            }

                            comboBox.SelectedIndex = selectedIndex;

                            Controls.Add(comboBox);
                            itemEdits.Add(comboBox);
                        }
                        else if (item.Type == GenericPropertyType.Size)
                        {
                            // width
                            var spinEditWidth = new NumericUpDown()
                            {
                                Value = Convert.ToInt32(item.Value),
                                Tag = item,
                                Location = new Point(seperatorOffsetX, offsetY),
                                Size = new Size(itemEditWidth, Size.Height),
                            };
                            spinEditWidth.ValueChanged += SpinEdit_Integer_ValueChanged;
                            //spinEditWidth.Properties.IsFloatValue = false;

                            Controls.Add(spinEditWidth);
                            itemEdits.Add(spinEditWidth);
                        }
                        /*
                        else if (item.Type == GenericPropertyType.Color)
                        {
                            var colorPickEdit = new ColorPickEdit()
                            {
                                Color = ColorHelper.FromHtml(Convert.ToString(item.Value)),
                                Text = string.Empty,
                                Tag = item,
                                Location = new Point(seperatorOffsetX, offsetY),
                                Size = new Size(itemEditWidth, Size.Height)
                            };

                            colorPickEdit.ColorChanged += ColorPickEdit_ColorChanged;

                            Controls.Add(colorPickEdit);
                            itemEdits.Add(colorPickEdit);
                        }
                        */
                        else if (item.Type == GenericPropertyType.Path)
                        {
                            var buttonEdit = new ButtonEdit()
                            {
                                Text = Convert.ToString(item.Value),
                                Tag = item,
                                Location = new Point(seperatorOffsetX, offsetY),
                                Size = new Size(itemEditWidth, Options.ItemHeight),
                            };

                            buttonEdit.TextChanged += ButtonEdit_TextChanged;
                            buttonEdit.OnButtonClick += ButtonEdit_OnButtonClick;

                            if (item.CanUserResetToDefaultValue)
                            {
                                var buttonInfo = new ButtonInfo()
                                {
                                    Tag = "clear"
                                };

                                buttonEdit.Buttons.Insert(0, buttonInfo);
                            }

                            Controls.Add(buttonEdit);
                            itemEdits.Add(buttonEdit);
                        }

                        offsetY += Options.ItemHeight + Options.ItemGap;
                    }
                }

                ResumeLayout();
            }
        }

        private void LayoutControls()
        {
            //SuspendLayout();

            if (categoryLabels != null && categorySeperators != null)
            {
                // category labels and seperators
                var widthForCategory = Width - Options.CategoryLeftMargin - Options.CategoryRightMargin;

                if (VerticalScroll.Visible)
                    widthForCategory -= SystemInformation.VerticalScrollBarWidth + Options.ScrollBarPadding;

                foreach (var categoryLabel in categoryLabels)
                    categoryLabel.Width = widthForCategory;
                foreach (var categorySeperator in categorySeperators)
                    categorySeperator.Width = widthForCategory;
            }

            if (itemLabels != null && itemEdits != null)
            {
                // item labels and edits
                var widthForItem = Width - Options.ItemLeftMargin - Options.ItemRightMargin;

                var seperatorOffsetX = Options.SeperatorOffset;
                if (Options.SeperatorOffsetType == SeperatorOffsetType.Percent)
                    seperatorOffsetX = MathHelper.RoundInt((double)Width * (double)seperatorOffsetX / (double)100);

                if (VerticalScroll.Visible)
                    seperatorOffsetX -= SystemInformation.VerticalScrollBarWidth + Options.ScrollBarPadding;

                foreach (var itemLabel in itemLabels)
                    itemLabel.Width = seperatorOffsetX - Options.ItemLeftMargin - Options.SeperatorPadding;
                foreach (var itemEdit in itemEdits)
                {
                    itemEdit.Left = seperatorOffsetX;

                    var editWidth = Width - Options.ItemRightMargin - seperatorOffsetX;
                    if (VerticalScroll.Visible)
                        editWidth -= SystemInformation.VerticalScrollBarWidth + Options.ScrollBarPadding;

                    itemEdit.Width = editWidth;
                }
            }
            
            //ResumeLayout();
        }

        #region Control Events

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            var property = (GenericProperty.Data.GenericProperty)textBox.Tag;
            property.Value = textBox.Text;
        }

        private void SpinEdit_Integer_ValueChanged(object sender, EventArgs e)
        {
            var spinEdit = (NumericUpDown)sender;
            var property = (GenericProperty.Data.GenericProperty)spinEdit.Tag;
            property.Value = Convert.ToInt32(spinEdit.Value);
        }

        private void SpinEdit_Float_ValueChanged(object sender, EventArgs e)
        {
            var spinEdit = (NumericUpDown)sender;
            var property = (GenericProperty.Data.GenericProperty)spinEdit.Tag;
            property.Value = spinEdit.Value;
        }

        private void CheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = (CheckBox)sender;
            var property = (GenericProperty.Data.GenericProperty)checkEdit.Tag;
            property.Value = Convert.ToBoolean(checkEdit.Checked);
        }

        private void ComboBoxEdit_SelectedValueChanged(object sender, EventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var property = (GenericProperty.Data.GenericProperty)comboBox.Tag;
            foreach (var enumItem in property.EnumItems)
            {
                if (enumItem.Name == comboBox.Text)
                {
                    property.Value = enumItem.Value;
                    break;
                }
            }
        }

        /*
        private void ColorPickEdit_ColorChanged(object sender, EventArgs e)
        {
            var colorPickEdit = (ColorPickEdit)sender;
            var property = (GenericProperty)colorPickEdit.Tag;
            property.Value = colorPickEdit.Color;
        }
        */

        private void ButtonEdit_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            var property = (GenericProperty.Data.GenericProperty)textBox.Tag;
            property.Value = textBox.Text;
        }

        private void ButtonEdit_OnButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = (ButtonEdit)sender;
            var property = (GenericProperty.Data.GenericProperty)buttonEdit.Tag;

            if (e.ButtonInfo.Tag == null)
            {
                var dialog = new FolderBrowserDialog()
                {
                    Description = property.PathDescription,
                    SelectedPath = Convert.ToString(property.Value)
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    buttonEdit.Text = dialog.SelectedPath;
                    property.Value = dialog.SelectedPath;
                }
            }
            else
            {
                var buttonName = (string)e.ButtonInfo.Tag;
                if (buttonName == "clear")
                {
                    property.Value = property.DefaultValue;
                    buttonEdit.Text = Convert.ToString(property.Value);
                }
            }
        }

        #endregion

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            LayoutControls();
        }
    }
}
