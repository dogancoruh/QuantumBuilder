using QuantumBuilder.Shared.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quantum.Framework.GenericProperties.Data;
using Quantum.Framework.GenericProperties.Enum;
using QuantumBuilder.Shared.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace QuantumBuilder.Obfuscation.ConfuserEx
{
    public class ConfuserExPlugin : Plugin
    {
        public override string Name => "confuserEx";
        public override string DisplayName => "ConfuserEx";
        public override string Description => "ConfuserEx Obfuscation Plugin";
        public override PluginType Type => PluginType.Obfuscation;

        private Process process;

        public override GenericPropertyCollection GetProperties()
        {
            return new GenericPropertyCollection()
            {
                new GenericProperty()
                {
                    ScopeName = "options",
                    CategoryName = "protection",
                    CategoryDisplayName = "Protection",
                    Name = ConfuserExPropertyName.AntiILDasmProtection,
                    DisplayName = "Anti ILDasm",
                    Type = GenericPropertyType.Boolean,
                    DefaultValue = false
                },
                new GenericProperty()
                {
                    ScopeName = "options",
                    CategoryName = "protection",
                    CategoryDisplayName = "Protection",
                    Name = ConfuserExPropertyName.AntiTamperProtection,
                    DisplayName = "Anti Tampering",
                    Type = GenericPropertyType.Boolean,
                    DefaultValue = false
                },
                new GenericProperty()
                {
                    ScopeName = "options",
                    CategoryName = "protection",
                    CategoryDisplayName = "Protection",
                    Name = ConfuserExPropertyName.ConstantProtection,
                    DisplayName = "Constant Encryption",
                    Type = GenericPropertyType.Boolean,
                    DefaultValue = false
                },
                new GenericProperty()
                {
                    ScopeName = "options",
                    CategoryName = "protection",
                    CategoryDisplayName = "Protection",
                    Name = ConfuserExPropertyName.ControlFlowProtection,
                    DisplayName = "Control Flow",
                    Type = GenericPropertyType.Boolean,
                    DefaultValue = false
                },
                new GenericProperty()
                {
                    ScopeName = "options",
                    CategoryName = "protection",
                    CategoryDisplayName = "Protection",
                    Name = ConfuserExPropertyName.AntiDumpProtection,
                    DisplayName = "Anti Dump",
                    Type = GenericPropertyType.Boolean,
                    DefaultValue = false
                },
                new GenericProperty()
                {
                    ScopeName = "options",
                    CategoryName = "protection",
                    CategoryDisplayName = "Protection",
                    Name = ConfuserExPropertyName.AntiDebugProtection,
                    DisplayName = "Anti Debug",
                    Type = GenericPropertyType.Boolean,
                    DefaultValue = false
                },
                new GenericProperty()
                {
                    ScopeName = "options",
                    CategoryName = "protection",
                    CategoryDisplayName = "Protection",
                    Name = ConfuserExPropertyName.InvalidMetadataProtection,
                    DisplayName = "Invalid Metadata",
                    Type = GenericPropertyType.Boolean,
                    DefaultValue = false
                },
                new GenericProperty()
                {
                    ScopeName = "options",
                    CategoryName = "protection",
                    CategoryDisplayName = "Protection",
                    Name = ConfuserExPropertyName.ReferenceProxyProtection,
                    DisplayName = "Reference Proxy",
                    Type = GenericPropertyType.Boolean,
                    DefaultValue = false
                },
                new GenericProperty()
                {
                    ScopeName = "options",
                    CategoryName = "protection",
                    CategoryDisplayName = "Protection",
                    Name = ConfuserExPropertyName.ResourcesProtection,
                    DisplayName = "Resources Encryption",
                    Type = GenericPropertyType.Boolean,
                    DefaultValue = false
                },
                new GenericProperty()
                {
                    ScopeName = "options",
                    CategoryName = "protection",
                    CategoryDisplayName = "Protection",
                    Name = ConfuserExPropertyName.SymbolRenameProtection,
                    DisplayName = "Name Encryption",
                    Type = GenericPropertyType.Boolean,
                    DefaultValue = false
                }
            };
        }

        public override void Execute(Dictionary<string, object> dictionary)
        {
            base.Execute(dictionary);

            var project = (Project)dictionary["project"];
            var buildPath = (string)dictionary["buildPath"];

            var obfuscationPath = Path.Combine(buildPath, "obfuscation");
            if (!Directory.Exists(obfuscationPath))
                Directory.CreateDirectory(obfuscationPath);

            var obfuscationProjectFileName = Path.Combine(obfuscationPath, project.Name + ".crproj");
            var obfuscationOutputPath = Path.Combine(obfuscationPath, "confused");

            // generate confuserEx project
            ConfuserExProjectBuilder.CreateProjectFile(obfuscationProjectFileName,
                                                       obfuscationOutputPath,
                                                       project.ProjectPath,
                                                       project.Obfuscation.ProfileParameters,
                                                       project.Obfuscation.Items);

            // execute with ConfuserEx
            var startInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Obfuscation\ConfuserEx\Confuser.CLI.exe"),
                Arguments = string.Format("-n {0} -o {1}", obfuscationProjectFileName, obfuscationOutputPath),
                WindowStyle = ProcessWindowStyle.Maximized,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            process = Process.Start(startInfo);
            while(!process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine();
                Log("obfuscation", line);
            }
            if (process.ExitCode != 0)
                Error("obfuscation", "Obfuscation failed!");

            process.Close();
            process = null;
        }

        public override void Terminate()
        {
            base.Terminate();

            if (process != null)
                process.Kill();
        }
    }
}
