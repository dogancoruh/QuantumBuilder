using Quantum.Framework.GenericProperties.Controls.GenericPropertyListControl.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Shared.Plugin
{
    public static class PluginParametersDisplayTypeExtensions
    {
        public static ViewMode ToViewMode(this PluginParametersDisplayType displayType)
        {
            switch (displayType)
            {
                default:
                case PluginParametersDisplayType.List:
                    return ViewMode.List;
                case PluginParametersDisplayType.CategoryList:
                    return ViewMode.CategoryList;
                case PluginParametersDisplayType.TileList:
                    return ViewMode.TileList;
            }
        }
    }
}
