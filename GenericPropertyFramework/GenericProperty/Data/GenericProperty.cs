using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Drawing;

namespace Quantum.Framework.GenericProperty.Data
{
    public class GenericProperty : IDeepCloneable<GenericProperty>
    {
        public event EventHandler<EventArgs> OnChange;

        public string ScopeName { get; set; }
        public string SectionName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDisplayName { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Browsable { get; set; }

        private GenericPropertyType type;
        public GenericPropertyType Type
        {
            get { return type; }
            set
            {
                type = value;

                if (type == GenericPropertyType.Integer)
                {
                    MinimumValue = int.MinValue;
                    MaximumValue = int.MaxValue;
                }
                else if (type == GenericPropertyType.Decimal)
                {
                    MinimumValue = decimal.MinValue;
                    MaximumValue = decimal.MaxValue;
                }
            }
        }

        private object value_;
        public object Value
        {
            get { return value_; }
            set
            {
                value_ = value;
                OnChange?.Invoke(this, EventArgs.Empty);
            }
        }

        public object DefaultValue { get; set; }
        public List<GenericPropertyEnumItem> EnumItems { get; set; }
        public object MinimumValue { get; set; }
        public object MaximumValue { get; set; }
        public GenericPropertySeperatorType SeperatorType { get; set; }

        public string PathDescription { get; set; }
        public bool CanUserResetToDefaultValue { get; set; }

        public GenericProperty()
        {
            Browsable = true;
        }

        public GenericProperty Clone()
        {
            var genericProperty = new GenericProperty()
            {
                ScopeName = ScopeName,
                SectionName = SectionName,
                CategoryName = CategoryName,
                CategoryDisplayName = CategoryDisplayName,
                Name = Name,
                DisplayName = DisplayName,
                Browsable = Browsable,
                Type = Type,
                Value = Value,
                DefaultValue = DefaultValue,
                MinimumValue = MinimumValue,
                MaximumValue = MaximumValue,
                SeperatorType = SeperatorType,
                PathDescription = PathDescription,
                CanUserResetToDefaultValue = CanUserResetToDefaultValue
            };

            if (EnumItems != null)
            {
                genericProperty.EnumItems = new List<GenericPropertyEnumItem>();
                foreach (var item in EnumItems)
                    genericProperty.EnumItems.Add(item.Clone());
            }

            return genericProperty;
        }

        public override string ToString()
        {
            return "GenericProperty [" + Name + "]";
        }

        public override bool Equals(object obj)
        {
            if (obj is GenericProperty genericProperty)
            {
                if (genericProperty.ScopeName == ScopeName &&
                    genericProperty.SectionName == SectionName &&
                    genericProperty.CategoryName == CategoryName &&
                    genericProperty.Name == Name)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
