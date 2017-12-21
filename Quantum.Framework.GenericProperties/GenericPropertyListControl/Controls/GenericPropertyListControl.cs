using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Events;
using Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Data;
using Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Enum;
using Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Utilities;
using Quantum.Framework.GenericProperties.Enum;
using Quantum.Framework.GenericProperties.Data;
using Quantum.Framework.GenericProperties.GenericPropertyListControl.Enum;
using Quantum.Framework.GenericProperties.GenericPropertyListControl.Data;

namespace Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl
{
    public partial class GenericPropertyListControl : ScrollableControl
    {
        const int CATEGORY_TITLE_SEPERATOR_GAP = 5;
        const int ITEM_OFFSET = 8;
        const int ITEM_LABEL_OFFSET = 4;
        const int ITEM_EDITOR_GAP = 2;

        public event EventHandler<PropertyValueChangeEventArgs> OnPropertyValueChanged;

        #region Private Variables

        private List<Control> controls;

        private List<string> categoryNames;

        #endregion

        #region Properties

        public GenericPropertyListOptions Options { get; set; }

        private GenericPropertyCollection properties;

        [Browsable(false)]
        public GenericPropertyCollection Properties
        {
            get { return properties; }
            set
            {
                properties = value;

                categoryNames = properties != null ? properties.Select(x => x.CategoryName).Distinct().ToList() : null;
            }
        }

        [Browsable(false)]
        public Dictionary<string, object> Values { get; set; }

        #endregion

        public GenericPropertyListControl()
        {
            controls = new List<Control>();

            Margin = new Padding(0);
            Padding = new Padding(0);
            AutoScrollMargin = new Size(0, 0);
            AutoScrollMinSize = new Size(0, 0);

            Options = new GenericPropertyListOptions();

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

        private void CreateCategoryList()
        {
            // calculate seperator
            var seperatorXOffset = Options.SeperatorOffset;
            if (Options.SeperatorOffsetType == SeperatorOffsetType.Percent)
                seperatorXOffset = MathHelper.RoundInt((double)Width * (double)seperatorXOffset / (double)100);

            // layout categories and items
            int yOffset = Options.CategoryTopMargin;

            foreach (var categoryName in categoryNames)
            {
                var panelTitle = new Panel()
                {
                    BorderStyle = BorderStyle.None,
                    //BackColor = Color.Red,
                    Location = new Point(0, yOffset),
                    Size = new Size(Width, Options.ItemHeight)
                };
                Controls.Add(panelTitle);

                // category title
                var categoryLabel = new Label()
                {
                    Text = Properties.Where(x => x.CategoryName == categoryName).First().CategoryDisplayName,
                    Font = Options.CategoryTitleFont,
                    AutoSize = true,
                    Tag = new ItemControlInfo()
                    {
                        ControlType = ControlType.CategorySeperator
                    }
                };
                panelTitle.Controls.Add(categoryLabel);
                categoryLabel.Location = new Point(Options.CategoryLeftMargin, Options.ItemHeight / 2 - categoryLabel.Height / 2);
                categoryLabel.Size = new Size(Width - Options.CategoryLeftMargin - Options.CategoryRightMargin, categoryLabel.Height);

                yOffset += categoryLabel.Height + CATEGORY_TITLE_SEPERATOR_GAP;

                // category seperator
                var categorySeperator = new Panel()
                {
                    Location = new Point(Options.CategoryLeftMargin, Options.ItemHeight - 1),
                    Size = new Size(Width - Options.CategoryLeftMargin - Options.CategoryRightMargin, 1),
                    BackColor = Color.Black,
                    Tag = new ItemControlInfo()
                    {
                        ControlType = ControlType.CategorySeperator
                    }
                };
                panelTitle.Controls.Add(categorySeperator);

                yOffset += categorySeperator.Height + ITEM_OFFSET;

                var items = properties.Where(x => x.CategoryName == categoryName).ToList();
                foreach (var item in items)
                {
                    // label
                    var itemLabel = new Label()
                    {
                        Text = item.DisplayName,
                        ForeColor = Options.ItemLabelForeColor,
                        //BackColor = Color.Red,
                        AutoSize = true,
                        Tag = new ItemControlInfo()
                        {
                            ControlType = ControlType.ItemLabel,
                            Property = item
                        }
                    };

                    Controls.Add(itemLabel);
                    controls.Add(itemLabel);

                    itemLabel.Location = new Point(Options.ItemLeftMargin, yOffset + (Options.ItemHeight / 2 - itemLabel.Size.Height / 2));
                    itemLabel.Size = new Size(seperatorXOffset - Options.ItemLeftMargin - Options.SeperatorPadding, itemLabel.Size.Height);

                    // edit
                    var itemEditWidth = Width - seperatorXOffset - Options.ItemRightMargin;

                    switch (item.Type)
                    {
                        case GenericPropertyType.String:
                            var editorString = new TextBox()
                            {
                                Text = Convert.ToString(item.Value),
                                Tag = new ItemControlInfo()
                                {
                                    ControlType = ControlType.ItemEdit,
                                    Property = item
                                }
                            };
                            editorString.TextChanged += TextBox_TextChanged;

                            Controls.Add(editorString);
                            controls.Add(editorString);

                            editorString.Location = new Point(seperatorXOffset, yOffset + (Options.ItemHeight / 2 - editorString.Size.Height / 2));
                            editorString.Size = new Size(itemEditWidth, editorString.Size.Height);

                            break;
                        case GenericPropertyType.Integer:
                            var editorInteger = new NumericUpDown()
                            {
                                Value = Convert.ToInt32(item.Value),
                                Tag = new ItemControlInfo()
                                {
                                    ControlType = ControlType.ItemEdit,
                                    Property = item
                                }
                            };
                            editorInteger.ValueChanged += SpinEdit_Integer_ValueChanged;

                            editorInteger.Minimum = Convert.ToInt32(item.MinimumValue);
                            editorInteger.Maximum = Convert.ToInt32(item.MaximumValue);

                            Controls.Add(editorInteger);
                            controls.Add(editorInteger);

                            editorInteger.Location = new Point(seperatorXOffset, yOffset + Options.ItemHeight / 2 - editorInteger.Size.Height / 2);
                            editorInteger.Size = new Size(itemEditWidth, editorInteger.Size.Height);

                            break;
                        case GenericPropertyType.Decimal:
                            var editorDecimal = new NumericUpDown()
                            {
                                Value = Convert.ToDecimal(item.Value),
                                Tag = new ItemControlInfo()
                                {
                                    ControlType = ControlType.ItemEdit,
                                    Property = item
                                }
                            };

                            editorDecimal.ValueChanged += SpinEdit_Float_ValueChanged;

                            editorDecimal.Minimum = Convert.ToDecimal(item.MinimumValue);
                            editorDecimal.Maximum = Convert.ToDecimal(item.MaximumValue);

                            Controls.Add(editorDecimal);
                            controls.Add(editorDecimal);

                            editorDecimal.Location = new Point(seperatorXOffset, yOffset + Options.ItemHeight / 2 - editorDecimal.Size.Height / 2);
                            editorDecimal.Size = new Size(itemEditWidth, editorDecimal.Size.Height);

                            break;
                        case GenericPropertyType.Boolean:
                            var editorBoolean = new CheckBox()
                            {
                                Checked = Convert.ToBoolean(item.Value),
                                Text = string.Empty,
                                Tag = new ItemControlInfo()
                                {
                                    ControlType = ControlType.ItemEdit,
                                    Property = item
                                }
                            };

                            editorBoolean.CheckedChanged += CheckEdit_CheckedChanged;

                            Controls.Add(editorBoolean);
                            controls.Add(editorBoolean);

                            editorBoolean.Location = new Point(seperatorXOffset, yOffset + Options.ItemHeight / 2 - editorBoolean.Size.Height / 2);
                            editorBoolean.Size = new Size(itemEditWidth, editorBoolean.Size.Height);

                            break;
                        case GenericPropertyType.Enumeration:
                            var editorEnumeration = new ComboBox
                            {
                                Tag = new ItemControlInfo()
                                {
                                    ControlType = ControlType.ItemEdit,
                                    Property = item
                                }
                            };

                            editorEnumeration.DropDownStyle = ComboBoxStyle.DropDownList;

                            var selectedIndex = -1;
                            var index = 0;

                            foreach (var enumItem in item.EnumItems)
                            {
                                editorEnumeration.Items.Add(enumItem);

                                if (enumItem.Value.Equals(item.Value))
                                    selectedIndex = index;

                                index++;
                            }

                            editorEnumeration.SelectedIndex = selectedIndex;

                            editorEnumeration.SelectedValueChanged += ComboBoxEdit_SelectedValueChanged;

                            Controls.Add(editorEnumeration);
                            controls.Add(editorEnumeration);

                            editorEnumeration.Location = new Point(seperatorXOffset, yOffset + Options.ItemHeight / 2 - editorEnumeration.Size.Height / 2);
                            editorEnumeration.Size = new Size(itemEditWidth, editorEnumeration.Size.Height);

                            break;
                        case GenericPropertyType.Color:
                            var editorColor = new ColorEdit()
                            {
                                Color = Color.Red,
                                Height = 21,
                                Tag = new ItemControlInfo()
                                {
                                    ControlType = ControlType.ItemEdit,
                                    Property = item
                                }
                            };

                            editorColor.OnColorChanged += EditorColor_ColorChanged;

                            Controls.Add(editorColor);
                            controls.Add(editorColor);

                            editorColor.Location = new Point(seperatorXOffset, yOffset + (Options.ItemHeight / 2 - editorColor.Size.Height / 2));
                            editorColor.Size = new Size(itemEditWidth, editorColor.Size.Height);

                            break;
                        case GenericPropertyType.Path:
                            var editorPath = new ButtonEdit()
                            {
                                Text = Convert.ToString(item.Value),
                                Height = 20,
                                Tag = new ItemControlInfo()
                                {
                                    ControlType = ControlType.ItemEdit,
                                    Property = item
                                }
                            };

                            editorPath.OnTextChanged += EditorPath_OnTextChanged;
                            editorPath.OnButtonClick += EditorPath_OnButtonClick;

                            Controls.Add(editorPath);
                            controls.Add(editorPath);

                            editorPath.Location = new Point(seperatorXOffset, yOffset + (Options.ItemHeight / 2 - editorPath.Size.Height / 2));
                            editorPath.Size = new Size(itemEditWidth, editorPath.Size.Height);

                            break;
                        default:
                            break;
                    }

                    yOffset += Options.ItemHeight;
                }
            }
        }

        private void EditorColor_ColorChanged(object sender, EventArgs e)
        {
            var colorEdit = (ColorEdit)sender;
            var itemControlInfo = (ItemControlInfo)colorEdit.Tag;
            var property = itemControlInfo.Property;

            if (Values == null)
                property.Value = colorEdit.Color;
            else
                Values[property.Name] = colorEdit.Color;

            OnPropertyValueChanged?.Invoke(this, new PropertyValueChangeEventArgs()
            {
                Property = property,
                Value = property.Value
            });
        }

        private void EditorPath_OnTextChanged(object sender, EventArgs e)
        {
            var buttonEdit = (ButtonEdit)sender;
            var itemControlInfo = (ItemControlInfo)buttonEdit.Tag;
            var property = itemControlInfo.Property;

            if (Values == null)
                property.Value = buttonEdit.Text;
            else
                Values[property.Name] = buttonEdit.Text;

            OnPropertyValueChanged?.Invoke(this, new PropertyValueChangeEventArgs()
            {
                Property = property,
                Value = property.Value
            });
        }

        private void EditorPath_OnButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = (ButtonEdit)sender;
            var itemControlInfo = (ItemControlInfo)buttonEdit.Tag;
            var property = itemControlInfo.Property;

            var value = Convert.ToString(property.DefaultValue);

            if (Values == null)
                value = Convert.ToString(property.Value);
            else
            {
                if (Values.ContainsKey(property.Name))
                    value = Convert.ToString(Values[property.Name]);
            }

            var dialog = new FolderBrowserDialog()
            {
                Description = property.PathDescription,
                SelectedPath = value
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                buttonEdit.Text = dialog.SelectedPath;
            }
        }

        private void CreateTileList()
        {

        }

        private void CreateList()
        {
            // calculate seperator
            var seperatorXOffset = Options.SeperatorOffset;
            if (Options.SeperatorOffsetType == SeperatorOffsetType.Percent)
                seperatorXOffset = MathHelper.RoundInt((double)Width * (double)seperatorXOffset / (double)100);

            // layout categories and items
            int yOffset = Options.CategoryTopMargin;

            foreach (var item in properties)
            {
                // label
                var itemLabel = new Label()
                {
                    Text = item.DisplayName,
                    ForeColor = Options.ItemLabelForeColor,
                    //BackColor = Color.Red,
                    AutoSize = true,
                    Tag = new ItemControlInfo()
                    {
                        ControlType = ControlType.ItemLabel,
                        Property = item
                    }
                };

                Controls.Add(itemLabel);
                controls.Add(itemLabel);

                itemLabel.Location = new Point(Options.ItemLeftMargin, yOffset + (Options.ItemHeight / 2 - itemLabel.Size.Height / 2));
                itemLabel.Size = new Size(seperatorXOffset - Options.ItemLeftMargin - Options.SeperatorPadding, itemLabel.Size.Height);

                // edit
                var itemEditWidth = Width - seperatorXOffset - Options.ItemRightMargin;

                switch (item.Type)
                {
                    case GenericPropertyType.String:
                        var editorString = new TextBox()
                        {
                            Text = Convert.ToString(item.Value),
                            Tag = new ItemControlInfo()
                            {
                                ControlType = ControlType.ItemEdit,
                                Property = item
                            }
                        };
                        editorString.TextChanged += TextBox_TextChanged;

                        Controls.Add(editorString);
                        controls.Add(editorString);

                        editorString.Location = new Point(seperatorXOffset, yOffset + (Options.ItemHeight / 2 - editorString.Size.Height / 2));
                        editorString.Size = new Size(itemEditWidth, editorString.Size.Height);

                        break;
                    case GenericPropertyType.Integer:
                        var editorInteger = new NumericUpDown()
                        {
                            Value = Convert.ToInt32(item.Value),
                            Tag = new ItemControlInfo()
                            {
                                ControlType = ControlType.ItemEdit,
                                Property = item
                            }
                        };
                        editorInteger.ValueChanged += SpinEdit_Integer_ValueChanged;

                        editorInteger.Minimum = Convert.ToInt32(item.MinimumValue);
                        editorInteger.Maximum = Convert.ToInt32(item.MaximumValue);

                        Controls.Add(editorInteger);
                        controls.Add(editorInteger);

                        editorInteger.Location = new Point(seperatorXOffset, yOffset + Options.ItemHeight / 2 - editorInteger.Size.Height / 2);
                        editorInteger.Size = new Size(itemEditWidth, editorInteger.Size.Height);

                        break;
                    case GenericPropertyType.Decimal:
                        var editorDecimal = new NumericUpDown()
                        {
                            Value = Convert.ToDecimal(item.Value),
                            Tag = new ItemControlInfo()
                            {
                                ControlType = ControlType.ItemEdit,
                                Property = item
                            }
                        };

                        editorDecimal.ValueChanged += SpinEdit_Float_ValueChanged;

                        editorDecimal.Minimum = Convert.ToDecimal(item.MinimumValue);
                        editorDecimal.Maximum = Convert.ToDecimal(item.MaximumValue);

                        Controls.Add(editorDecimal);
                        controls.Add(editorDecimal);

                        editorDecimal.Location = new Point(seperatorXOffset, yOffset + Options.ItemHeight / 2 - editorDecimal.Size.Height / 2);
                        editorDecimal.Size = new Size(itemEditWidth, editorDecimal.Size.Height);

                        break;
                    case GenericPropertyType.Boolean:
                        var editorBoolean = new CheckBox()
                        {
                            Checked = Convert.ToBoolean(item.Value),
                            Text = string.Empty,
                            Tag = new ItemControlInfo()
                            {
                                ControlType = ControlType.ItemEdit,
                                Property = item
                            }
                        };

                        editorBoolean.CheckedChanged += CheckEdit_CheckedChanged;

                        Controls.Add(editorBoolean);
                        controls.Add(editorBoolean);

                        editorBoolean.Location = new Point(seperatorXOffset, yOffset + Options.ItemHeight / 2 - editorBoolean.Size.Height / 2);
                        editorBoolean.Size = new Size(itemEditWidth, editorBoolean.Size.Height);

                        break;
                    case GenericPropertyType.Enumeration:
                        var editorEnumeration = new ComboBox
                        {
                            Tag = new ItemControlInfo()
                            {
                                ControlType = ControlType.ItemEdit,
                                Property = item
                            }
                        };

                        editorEnumeration.DropDownStyle = ComboBoxStyle.DropDownList;

                        var selectedIndex = -1;
                        var index = 0;

                        foreach (var enumItem in item.EnumItems)
                        {
                            editorEnumeration.Items.Add(enumItem);

                            if (enumItem.Value.Equals(item.Value))
                                selectedIndex = index;

                            index++;
                        }

                        editorEnumeration.SelectedIndex = selectedIndex;

                        editorEnumeration.SelectedValueChanged += ComboBoxEdit_SelectedValueChanged;

                        Controls.Add(editorEnumeration);
                        controls.Add(editorEnumeration);

                        editorEnumeration.Location = new Point(seperatorXOffset, yOffset + Options.ItemHeight / 2 - editorEnumeration.Size.Height / 2);
                        editorEnumeration.Size = new Size(itemEditWidth, editorEnumeration.Size.Height);

                        break;
                    case GenericPropertyType.Color:
                        var editorColor = new ColorEdit()
                        {
                            Color = Color.Red,
                            Height = 21,
                            Tag = new ItemControlInfo()
                            {
                                ControlType = ControlType.ItemEdit,
                                Property = item
                            }
                        };

                        editorColor.OnColorChanged += EditorColor_ColorChanged;

                        Controls.Add(editorColor);
                        controls.Add(editorColor);

                        editorColor.Location = new Point(seperatorXOffset, yOffset + (Options.ItemHeight / 2 - editorColor.Size.Height / 2));
                        editorColor.Size = new Size(itemEditWidth, editorColor.Size.Height);

                        break;
                    case GenericPropertyType.Path:
                        var editorPath = new ButtonEdit()
                        {
                            Text = Convert.ToString(item.Value),
                            Height = 20,
                            Tag = new ItemControlInfo()
                            {
                                ControlType = ControlType.ItemEdit,
                                Property = item
                            }
                        };

                        editorPath.OnTextChanged += EditorPath_OnTextChanged;
                        editorPath.OnButtonClick += EditorPath_OnButtonClick;

                        Controls.Add(editorPath);
                        controls.Add(editorPath);

                        editorPath.Location = new Point(seperatorXOffset, yOffset + (Options.ItemHeight / 2 - editorPath.Size.Height / 2));
                        editorPath.Size = new Size(itemEditWidth, editorPath.Size.Height);

                        break;
                    default:
                        break;
                }

                yOffset += Options.ItemHeight;
            }

            if (Options.AutoSize)
                Height = yOffset;
        }

        public new void Refresh()
        {
            if (Properties != null && Properties.Count > 0)
            {
                SuspendLayout();

                ClearListControls();

                if (Options.ViewMode == ViewMode.CategoryList)
                    CreateCategoryList();
                else if (Options.ViewMode == ViewMode.TileList)
                    CreateTileList();
                else if (Options.ViewMode == ViewMode.List)
                    CreateList();

                ResumeLayout();
            }
        }

        private void ClearListControls()
        {
            foreach (var control in controls)
                Controls.Remove(control);
        }

        private void LayoutControls()
        {
        /*
            var offsetY = 0;

            if (Options.ViewMode == ViewMode.CategoryList && items.Count > 0)
            {
                offsetY += Options.CategoryTopMargin;


                if (controls[ControlType.CategoryLabel] != null &&
                    controls[ControlType.CategorySeperator] != null)
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

            if (Options.AutoSize)
                Height = offsetY + 50;
        */
        }

        #region Control Events

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            var itemControlInfo = (ItemControlInfo)textBox.Tag;
            var property = itemControlInfo.Property;

            if (Values == null)
                property.Value = textBox.Text;
            else
                Values[property.Name] = textBox.Text;

            OnPropertyValueChanged?.Invoke(this, new PropertyValueChangeEventArgs()
            {
                Property = property,
                Value = property.Value
            });
        }

        private void SpinEdit_Integer_ValueChanged(object sender, EventArgs e)
        {
            var spinEdit = (NumericUpDown)sender;
            var itemControlInfo = (ItemControlInfo)spinEdit.Tag;
            var property = itemControlInfo.Property;

            if (Values == null)
                property.Value = Convert.ToInt32(spinEdit.Value);
            else
                Values[property.Name] = Convert.ToInt32(spinEdit.Value);

            OnPropertyValueChanged?.Invoke(this, new PropertyValueChangeEventArgs()
            {
                Property = property,
                Value = property.Value
            });
        }

        private void SpinEdit_Float_ValueChanged(object sender, EventArgs e)
        {
            var spinEdit = (NumericUpDown)sender;
            var itemControlInfo = (ItemControlInfo)spinEdit.Tag;
            var property = itemControlInfo.Property;

            if (Values == null)
                property.Value = spinEdit.Value;
            else
                Values[property.Name] = spinEdit.Value;

            OnPropertyValueChanged?.Invoke(this, new PropertyValueChangeEventArgs()
            {
                Property = property,
                Value = property.Value
            });
        }

        private void CheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = (CheckBox)sender;
            var itemControlInfo = (ItemControlInfo)checkEdit.Tag;
            var property = itemControlInfo.Property;

            if (Values == null)
                property.Value = Convert.ToBoolean(checkEdit.Checked);
            else
                Values[property.Name] = Convert.ToBoolean(checkEdit.Checked);

            OnPropertyValueChanged?.Invoke(this, new PropertyValueChangeEventArgs()
            {
                Property = property,
                Value = property.Value
            });
        }

        private void ComboBoxEdit_SelectedValueChanged(object sender, EventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var itemControlInfo = (ItemControlInfo)comboBox.Tag;
            var property = itemControlInfo.Property;

            foreach (var enumItem in property.EnumItems)
            {
                if (enumItem.Name == comboBox.Text)
                {
                    if (Values == null)
                        property.Value = enumItem.Value;
                    else
                        Values[property.Name] = enumItem.Value;

                    OnPropertyValueChanged?.Invoke(this, new PropertyValueChangeEventArgs()
                    {
                        Property = property,
                        Value = property.Value
                    });

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
            var itemControlInfo = (ItemControlInfo)textBox.Tag;
            var property = itemControlInfo.Property;

            if (Values == null)
                property.Value = textBox.Text;
            else
                Values[property.Name] = textBox.Text;

            OnPropertyValueChanged?.Invoke(this, new PropertyValueChangeEventArgs()
            {
                Property = property,
                Value = property.Value
            });
        }

        private void ButtonEdit_OnButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = (ButtonEdit)sender;
            var itemControlInfo = (ItemControlInfo)buttonEdit.Tag;
            var property = itemControlInfo.Property;

            if (e.ButtonInfo.Tag == null)
            {
                var dialog = new FolderBrowserDialog()
                {
                    Description = property.PathDescription,
                    SelectedPath = Convert.ToString(property.Value)
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (Values == null)
                        property.Value = dialog.SelectedPath;
                    else
                        Values[property.Name] = dialog.SelectedPath;

                    buttonEdit.Text = dialog.SelectedPath;

                    OnPropertyValueChanged?.Invoke(this, new PropertyValueChangeEventArgs()
                    {
                        Property = property,
                        Value = property.Value
                    });
                }
            }
            else
            {
                var buttonName = (string)e.ButtonInfo.Tag;
                if (buttonName == "clear")
                {
                    buttonEdit.Text = Convert.ToString(property.Value);

                    if (Values == null)
                        property.Value = property.DefaultValue;
                    else
                        Values[property.Name] = property.DefaultValue;

                    OnPropertyValueChanged?.Invoke(this, new PropertyValueChangeEventArgs()
                    {
                        Property = property,
                        Value = property.Value
                    });
                }
            }
        }

        #endregion

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            //LayoutControls();
        }

        private void ClearControls()
        {
            foreach (var control in controls)
                Controls.Remove(control);
        }
    }
}
