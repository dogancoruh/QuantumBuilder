using Newtonsoft.Json.Linq;
using Quantum.Framework.GenericProperties.Data;
using Quantum.Framework.GenericProperties.Utilities;
using QuantumBuilder.Shared.Data;
using QuantumBuilder.Shared.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Utilities
{
    public class ProjectLoader
    {
        public static Project Load(string fileName)
        {
            var contents = File.ReadAllText(fileName, Encoding.UTF8);
            var jObject = JObject.Parse(contents);
            var project = new Project
            {
                Name = JObjectHelper.GetStringValue(jObject, "name"),
                ProjectPath = JObjectHelper.GetStringValue(jObject, "projectPath"),
                OutputPath = JObjectHelper.GetStringValue(jObject, "outputPath")
            };

            // obfuscation
            if (jObject["obfuscation"] != null)
            {
                var jObjectObfuscation = (JObject)jObject["obfuscation"];

                project.Obfuscation.Enabled = JObjectHelper.GetBoolValue(jObjectObfuscation, "enabled");
                project.Obfuscation.ProfileName = JObjectHelper.GetStringValue(jObjectObfuscation, "profileName");

                if (jObjectObfuscation["profileParameters"] != null)
                {
                    var pluginInfo = PluginManager.Instance.GetPluginInfoByName(project.Obfuscation.ProfileName);
                    if (pluginInfo != null)
                    {
                        var pluginProperties = pluginInfo.Plugin.GetProperties();
                        project.Obfuscation.ProfileParameters = GenericPropertySerializer.DeserializeFromJArrayToDictionary(pluginProperties, (JArray)jObjectObfuscation["profileParameters"]);
                    }
                }

                if (jObjectObfuscation["items"] != null)
                {
                    var jArrayObfuscationItems = (JArray)jObjectObfuscation["items"];

                    foreach (JObject jObjectObfuscationItem in jArrayObfuscationItems)
                    {
                        project.Obfuscation.Items.Add(new ObfuscationItem()
                        {
                            Selected = JObjectHelper.GetBoolValue(jObjectObfuscationItem, "selected"),
                            FileName = JObjectHelper.GetStringValue(jObjectObfuscationItem, "fileName")
                        });
                    }
                }
            }

            // signing
            if (jObject["signing"] != null)
            {
                var jObjectSigning = (JObject)jObject["signing"];

                project.Signing.Enabled = JObjectHelper.GetBoolValue(jObjectSigning, "enabled");

                project.Signing.ProfileName = JObjectHelper.GetStringValue(jObjectSigning, "profileName");

                if (jObjectSigning["items"] != null)
                {
                    var jArraySigningItems = (JArray)jObjectSigning["items"];

                    foreach (JObject jObjectSigningItem in jArraySigningItems)
                    {
                        project.Signing.Items.Add(new SigningItem()
                        {
                            Selected = JObjectHelper.GetBoolValue(jObjectSigningItem, "selected"),
                            FileName = JObjectHelper.GetStringValue(jObjectSigningItem, "fileName")
                        });
                    }
                }
            }

            return project;
        }

        public static void Save(string fileName, Project project)
        {
            var jObjectProject = new JObject
            {
                ["name"] = project.Name,
                ["projectPath"] = project.ProjectPath,
                ["outputPath"] = project.OutputPath
            };

            // obfuscation
            var jObjectObfuscation = new JObject()
            {
                ["enabled"] = project.Obfuscation.Enabled,
                ["profileName"] = project.Obfuscation.ProfileName
            };
            var pluginInfo = PluginManager.Instance.GetPluginInfoByName(project.Obfuscation.ProfileName);
            if (pluginInfo != null)
            {
                var pluginProperties = pluginInfo.Plugin.GetProperties();
                jObjectObfuscation["profileParameters"] = GenericPropertySerializer.SerializeFromDictionaryToJArray(pluginProperties, project.Obfuscation.ProfileParameters);
            }

            var jArrayObfuscationItems = new JArray();
            foreach (var obfuscationItem in project.Obfuscation.Items)
            {
                jArrayObfuscationItems.Add(new JObject()
                {
                    ["selected"] = obfuscationItem.Selected,
                    ["fileName"] = obfuscationItem.FileName
                });
            }
            jObjectObfuscation["items"] = jArrayObfuscationItems;

            jObjectProject["obfuscation"] = jObjectObfuscation;

            // signing
            var jObjectSigning = new JObject()
            {
                ["enabled"] = project.Signing.Enabled,
                ["profileName"] = project.Signing.ProfileName
            };
            pluginInfo = PluginManager.Instance.GetPluginInfoByName(project.Obfuscation.ProfileName);
            if (pluginInfo != null)
            {
                var pluginProperties = pluginInfo.Plugin.GetProperties();
                jObjectSigning["profileParameters"] = GenericPropertySerializer.SerializeFromDictionaryToJArray(pluginProperties, project.Signing.ProfileParameters);
            }

            var jArraySigningItems = new JArray();
            foreach (var signinItem in project.Signing.Items)
            {
                jArraySigningItems.Add(new JObject()
                {
                    ["selected"] = signinItem.Selected,
                    ["fileName"] = signinItem.FileName
                });
            }
            jObjectSigning["items"] = jArraySigningItems;

            jObjectProject["signing"] = jObjectSigning;

            File.WriteAllText(fileName, jObjectProject.ToString(), Encoding.UTF8);
        }
    }
}
