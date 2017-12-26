using Quantum.Framework.GenericProperties.Data;
using Quantum.Framework.GenericProperties.Enum;
using QuantumBuilder.Shared.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Signing.CertificateSigner
{
    public class CertificateSignerPlugin : Plugin
    {
        public override string Name => "certificateSigner";
        public override string DisplayName => "Certificate Signer (.pfx and other certificates)";
        public override string Description => "Certificate Signer Plugin";
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
                    Name = "keyPassword",
                    DisplayName = "Key Password",
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
    }
}
