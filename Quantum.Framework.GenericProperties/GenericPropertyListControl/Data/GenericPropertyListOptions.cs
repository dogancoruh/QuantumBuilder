using Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Data
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GenericPropertyListOptions
    {
        public ViewMode ViewMode { get; set; }

        public int CategoryTopMargin { get; set; }
        public int CategoryLeftMargin { get; set; }
        public int CategoryRightMargin { get; set; }
        public int CategoryBottomMargin { get; set; }

        public int CategoryTitleHeight { get; set; }
        public Color CategoryTitleForeColor { get; set; }
        public Font CategoryTitleFont { get; set; }
        public int CategorySeperatorHeight { get; set; }
        public Color CategorySeperatorColor { get; set; }

        public int ItemLeftMargin { get; set; }
        public int ItemRightMargin { get; set; }
        public int ItemHeight { get; set; }
        public int ItemGap { get; set; }

        public SeperatorOffsetType SeperatorOffsetType { get; set; }
        public int SeperatorOffset { get; set; }
        public int SeperatorPadding { get; set; }

        public Color ItemLabelForeColor { get; set; }

        public int ScrollBarPadding { get; set; }

        public bool AutoSize { get; set; }

        public GenericPropertyListOptions()
        {
            ViewMode = ViewMode.CategoryList;

            CategoryTopMargin = 5;
            CategoryLeftMargin = 5;
            CategoryRightMargin = 5;
            CategoryTitleHeight = 21;
            CategoryTitleForeColor = Color.Silver;
            CategoryTitleFont = new Font("Tahoma", 8, FontStyle.Bold);
            CategorySeperatorHeight = 1;
            CategorySeperatorColor = Color.Silver;
            CategoryBottomMargin = 7;

            SeperatorOffsetType = SeperatorOffsetType.Percent;
            SeperatorOffset = 40;
            SeperatorPadding = 5;

            ItemLeftMargin = 5;
            ItemRightMargin = 5;
            ItemHeight = 21;
            ItemGap = 3;

            ScrollBarPadding = 3;
        }

        public override string ToString()
        {
            return "[List Options]";
        }
    }
}
