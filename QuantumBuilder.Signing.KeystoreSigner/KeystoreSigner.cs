using Quantum.Framework.GenericProperties.Data;
using Quantum.Framework.GenericProperties.Enum;
using QuantumBuilder.Shared.Data;
using QuantumBuilder.Shared.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Signing.KeystoreSigner
{
    public class KeystoreSigner : Plugin
    {
        public override string Name => "keystoreSigner";
        public override string DisplayName => "Keystore Signer (Registered certificates and USB dongles)";
        public override string Description => "Keystore Signer Plugin";
        public override PluginType Type => PluginType.Signing;
        public override PluginParametersDisplayType ParametersDisplayType => PluginParametersDisplayType.List;

        public override GenericPropertyCollection GetProperties()
        {
            return new GenericPropertyCollection()
            {
                new GenericProperty()
                {
                    ScopeName = "options",
                    CategoryName = "signing",
                    CategoryDisplayName = "Signing",
                    Name = "tokenPassword",
                    DisplayName = "Token Password",
                    Type = GenericPropertyType.String,
                    DefaultValue = string.Empty
                },
                new GenericProperty()
                {
                    ScopeName = "options",
                    CategoryName = "signing",
                    CategoryDisplayName = "Signing",
                    Name = "signinType",
                    DisplayName = "Signing Type",
                    Type = GenericPropertyType.Enumeration,
                    EnumItems = new List<GenericPropertyEnumItem>()
                    {
                        new GenericPropertyEnumItem()
                        {
                            Name = "Dual Signing (SHA1, SHA256)",
                            Value = "dual"
                        },
                        new GenericPropertyEnumItem()
                        {
                            Name = "SHA1",
                            Value = "sha1"
                        },
                        new GenericPropertyEnumItem()
                        {
                            Name = "SHA256",
                            Value = "sha256"
                        }
                    },
                    DefaultValue = "dual"
                }
            };
        }

        public override void Execute(Dictionary<string, object> dictionary)
        {
            base.Execute(dictionary);

            var project = (Project)dictionary["project"];
            var buildPath = (string)dictionary["buildPath"];

            if (project.Signing.Enabled)
            {
                foreach (var signinItem in project.Signing.Items)
                {

                }
            }
        }
    }
}
