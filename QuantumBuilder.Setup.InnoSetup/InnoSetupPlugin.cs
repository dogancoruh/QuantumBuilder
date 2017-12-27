using Quantum.Framework.GenericProperties.Data;
using Quantum.Framework.GenericProperties.Enum;
using QuantumBuilder.Shared.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Setup.InnoSetup
{
    public class InnoSetupPlugin : Plugin
    {
        public override string Name => "innoSetup";
        public override string DisplayName => "Inno Setup";
        public override string Description => "Inno Setup Packager Plugin";
        public override PluginType Type => PluginType.Setup;
        public override PluginParametersDisplayType ParametersDisplayType => PluginParametersDisplayType.CategoryList;

        public override GenericPropertyCollection GetProperties()
        {
            return new GenericPropertyCollection()
            {
                new GenericProperty()
                {
                    Browsable = true,
                    CategoryName = "publisher",
                    CategoryDisplayName = "Publisher",
                    Name = "appPublisher",
                    DisplayName = "Application Publisher",
                    Type = GenericPropertyType.String,
                    DefaultValue = string.Empty,
                    PlaceholderText = "Publisher name"
                },
                new GenericProperty()
                {
                    Browsable = true,
                    CategoryName = "publisher",
                    CategoryDisplayName = "Publisher",
                    Name = "appPublisherUrl",
                    DisplayName = "Application Publisher URL",
                    Type = GenericPropertyType.String,
                    DefaultValue = string.Empty,
                    PlaceholderText = "Publisher website URL"
                },
                new GenericProperty()
                {
                    Browsable = true,
                    CategoryName = "publisher",
                    CategoryDisplayName = "Publisher",
                    Name = "appSupportUrl",
                    DisplayName = "Application Support URL",
                    Type = GenericPropertyType.String,
                    DefaultValue = string.Empty
                },
                new GenericProperty()
                {
                    Browsable = true,
                    CategoryName = "publisher",
                    CategoryDisplayName = "Publisher",
                    Name = "appUpdatesUrl",
                    DisplayName = "Application Updates URL",
                    Type = GenericPropertyType.String,
                    DefaultValue = string.Empty
                },
                new GenericProperty()
                {
                    Browsable = true,
                    CategoryName = "application",
                    CategoryDisplayName = "Application",
                    Name = "defaultDirName",
                    DisplayName = "Default Application Directory",
                    Type = GenericPropertyType.Path,
                    DefaultValue = string.Empty
                },
                new GenericProperty()
                {
                    Browsable = true,
                    CategoryName = "package",
                    CategoryDisplayName = "Package",
                    Name = "compression",
                    DisplayName = "Compression",
                    Type = GenericPropertyType.Enumeration,
                    EnumItems = new List<GenericPropertyEnumItem>()
                    {
                        new GenericPropertyEnumItem()
                        {
                            Name = "Zip",
                            Value = "zip"
                        },
                        new GenericPropertyEnumItem()
                        {
                            Name = "BZip",
                            Value = "bzip"
                        },
                        new GenericPropertyEnumItem()
                        {
                            Name = "LZMA",
                            Value = "lzma"
                        },
                        new GenericPropertyEnumItem()
                        {
                            Name = "LZMA2",
                            Value = "lzma2"
                        },
                        new GenericPropertyEnumItem()
                        {
                            Name = "None",
                            Value = "none"
                        }
                    },
                    DefaultValue = "lzma"
                }
            };
        }
    }
}
