using Quantum.Framework.GenericProperties.Data;
using Quantum.Framework.GenericProperties.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenericPropertiesListControlTest
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            var properties = new GenericPropertyCollection();

            properties.Add(new GenericProperty()
            {
                ScopeName = "options",
                CategoryName = "person",
                CategoryDisplayName = "Personal Information",
                Name = "firstName",
                DisplayName = "First Name",
                Type = GenericPropertyType.String,
                DefaultValue = "David"
            });

            properties.Add(new GenericProperty()
            {
                ScopeName = "options",
                CategoryName = "person",
                CategoryDisplayName = "Personal Information",
                Name = "lastName",
                DisplayName = "Last Name",
                Type = GenericPropertyType.String,
                DefaultValue = "Brown"
            });

            properties.Add(new GenericProperty()
            {
                ScopeName = "options",
                CategoryName = "detailedInformation",
                CategoryDisplayName = "Detailed Information",
                Name = "age",
                DisplayName = "Age",
                Type = GenericPropertyType.Integer,
                DefaultValue = 0
            });

            properties.Add(new GenericProperty()
            {
                ScopeName = "options",
                CategoryName = "detailedInformation",
                CategoryDisplayName = "Detailed Information",
                Name = "gender",
                DisplayName = "Gender",
                Type = GenericPropertyType.Enumeration,
                DefaultValue = "male",
                EnumItems = new List<GenericPropertyEnumItem>()
                {
                    new GenericPropertyEnumItem()
                    {
                        Name = "Male",
                        Value = "male"
                    },
                    new GenericPropertyEnumItem()
                    {
                        Name = "Female",
                        Value = "female"
                    }
                }
            });

            properties.Add(new GenericProperty()
            {
                ScopeName = "options",
                CategoryName = "detailedInformation",
                CategoryDisplayName = "Detailed Information",
                Name = "retarded",
                DisplayName = "Retarded",
                Type = GenericPropertyType.Boolean,
                DefaultValue = true
            });

            properties.Add(new GenericProperty()
            {
                ScopeName = "options",
                CategoryName = "detailedInformation",
                CategoryDisplayName = "Detailed Information",
                Name = "outputPath",
                DisplayName = "Output Path",
                PathDescription = "Select path",
                Type = GenericPropertyType.Path,
                DefaultValue = "c:\\"
            });

            properties.Add(new GenericProperty()
            {
                ScopeName = "options",
                CategoryName = "detailedInformation",
                CategoryDisplayName = "Detailed Information",
                Name = "eyeColor",
                DisplayName = "Eye Color",
                Type = GenericPropertyType.Color,
                DefaultValue = Color.Blue
            });

            genericPropertyListControl1.Properties = properties;
            genericPropertyListControl1.Refresh();
        }

        private void genericPropertyListControl1_OnPropertyValueChanged(object sender, Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Events.PropertyValueChangeEventArgs e)
        {
            listBox1.Items.Add(e.Property.Name + " " + e.Property.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            genericPropertyListControl1.Options.ViewMode = Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Enum.ViewMode.List;
            genericPropertyListControl1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            genericPropertyListControl1.Options.ViewMode = Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Enum.ViewMode.CategoryList;
            genericPropertyListControl1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            genericPropertyListControl1.LayoutControls();
        }
    }
}
