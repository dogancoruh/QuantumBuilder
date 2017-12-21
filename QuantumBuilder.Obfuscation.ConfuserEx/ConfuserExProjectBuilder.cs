using Quantum.Framework.GenericProperties.Data;
using QuantumBuilder.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QuantumBuilder.Obfuscation.ConfuserEx
{
    public class ConfuserExProjectBuilder
    {
        public static void CreateProjectFile(string fileName, string outputDir, string baseDir, Dictionary<string, object> parameters, List<ObfuscationItem> items)
        {
            var xmlTextWriter = new XmlTextWriter(fileName, Encoding.UTF8);
            xmlTextWriter.Formatting = Formatting.Indented;

            xmlTextWriter.WriteStartElement("project");

            xmlTextWriter.WriteAttributeString("outputDir", outputDir);
            xmlTextWriter.WriteAttributeString("baseDir", baseDir);

            xmlTextWriter.WriteStartElement("rule");
            xmlTextWriter.WriteAttributeString("pattern", "true");
            xmlTextWriter.WriteAttributeString("inherit", "false");

            xmlTextWriter.WriteStartElement("protection");
            xmlTextWriter.WriteAttributeString("id", "resources");
            xmlTextWriter.WriteEndElement();

            if (parameters.ContainsKey(ConfuserExPropertyName.AntiILDasmProtection))
            {
                var selected = (bool)parameters[ConfuserExPropertyName.AntiILDasmProtection];
                if (selected)
                {
                    xmlTextWriter.WriteStartElement("protection");
                    xmlTextWriter.WriteAttributeString("id", "anti ildasm");
                    xmlTextWriter.WriteEndElement();
                }
            }

            if (parameters.ContainsKey(ConfuserExPropertyName.AntiTamperProtection))
            {
                var selected = (bool)parameters[ConfuserExPropertyName.AntiTamperProtection];
                if (selected)
                {
                    xmlTextWriter.WriteStartElement("protection");
                    xmlTextWriter.WriteAttributeString("id", "anti tamper");
                    xmlTextWriter.WriteEndElement();
                }
            }

            if (parameters.ContainsKey(ConfuserExPropertyName.ConstantProtection))
            {
                var selected = (bool)parameters[ConfuserExPropertyName.ConstantProtection];
                if (selected)
                {
                    xmlTextWriter.WriteStartElement("protection");
                    xmlTextWriter.WriteAttributeString("id", "constants");
                    xmlTextWriter.WriteEndElement();
                }
            }

            if (parameters.ContainsKey(ConfuserExPropertyName.ControlFlowProtection))
            {
                var selected = (bool)parameters[ConfuserExPropertyName.ControlFlowProtection];
                if (selected)
                {
                    xmlTextWriter.WriteStartElement("protection");
                    xmlTextWriter.WriteAttributeString("id", "ctrl flow");
                    xmlTextWriter.WriteEndElement();
                }
            }

            if (parameters.ContainsKey(ConfuserExPropertyName.AntiDumpProtection))
            {
                var selected = (bool)parameters[ConfuserExPropertyName.AntiDumpProtection];
                if (selected)
                {
                    xmlTextWriter.WriteStartElement("protection");
                    xmlTextWriter.WriteAttributeString("id", "anti dump");
                    xmlTextWriter.WriteEndElement();
                }
            }

            if (parameters.ContainsKey(ConfuserExPropertyName.AntiDebugProtection))
            {
                var selected = (bool)parameters[ConfuserExPropertyName.AntiDebugProtection];
                if (selected)
                {
                    xmlTextWriter.WriteStartElement("protection");
                    xmlTextWriter.WriteAttributeString("id", "anti debug");
                    xmlTextWriter.WriteEndElement();
                }
            }

            if (parameters.ContainsKey(ConfuserExPropertyName.InvalidMetadataProtection))
            {
                var selected = (bool)parameters[ConfuserExPropertyName.InvalidMetadataProtection];
                if (selected)
                {
                    xmlTextWriter.WriteStartElement("protection");
                    xmlTextWriter.WriteAttributeString("id", "invalid metadata");
                    xmlTextWriter.WriteEndElement();
                }
            }

            if (parameters.ContainsKey(ConfuserExPropertyName.ReferenceProxyProtection))
            {
                var selected = (bool)parameters[ConfuserExPropertyName.ReferenceProxyProtection];
                if (selected)
                {
                    xmlTextWriter.WriteStartElement("protection");
                    xmlTextWriter.WriteAttributeString("id", "ref proxy");
                    xmlTextWriter.WriteEndElement();
                }
            }

            if (parameters.ContainsKey(ConfuserExPropertyName.SymbolRenameProtection))
            {
                var selected = (bool)parameters[ConfuserExPropertyName.SymbolRenameProtection];
                if (selected)
                {
                    xmlTextWriter.WriteStartElement("protection");
                    xmlTextWriter.WriteAttributeString("id", "rename");
                    xmlTextWriter.WriteEndElement();
                }
            }

            xmlTextWriter.WriteEndElement();

            foreach (var item in items)
            {
                if (item.Selected)
                {
                    xmlTextWriter.WriteStartElement("module");
                    xmlTextWriter.WriteAttributeString("path", item.FileName);
                    xmlTextWriter.WriteEndElement();
                }
            }

            xmlTextWriter.WriteEndElement();

            xmlTextWriter.Flush();
            xmlTextWriter.Close();
            xmlTextWriter.Dispose();
            xmlTextWriter = null;
        }
    }
}
