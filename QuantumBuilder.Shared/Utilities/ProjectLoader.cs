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

namespace QuantumBuilder.Shared.Utilities
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

                project.Obfuscation.PluginName = JObjectHelper.GetStringValue(jObjectObfuscation, "pluginName");

                if (jObjectObfuscation["pluginParameters"] != null)
                {
                    var pluginInfo = PluginManager.Instance.GetPluginInfoByName(project.Obfuscation.PluginName);
                    if (pluginInfo != null)
                    {
                        var pluginProperties = pluginInfo.Plugin.GetProperties();
                        project.Obfuscation.PluginParameters = GenericPropertySerializer.DeserializeFromJArrayToDictionary(pluginProperties, (JArray)jObjectObfuscation["pluginParameters"]);
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

                project.Signing.PluginName = JObjectHelper.GetStringValue(jObjectSigning, "pluginName");

                if (jObjectSigning["pluginParameters"] != null)
                {
                    var pluginInfo = PluginManager.Instance.GetPluginInfoByName(project.Signing.PluginName);
                    if (pluginInfo != null)
                    {
                        var pluginProperties = pluginInfo.Plugin.GetProperties();
                        project.Signing.PluginParameters = GenericPropertySerializer.DeserializeFromJArrayToDictionary(pluginProperties, (JArray)jObjectSigning["pluginParameters"]);
                    }
                }

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

            // setup
            if (jObject["setup"] != null)
            {
                var jObjectSetup = (JObject)jObject["setup"];

                project.Setup.Enabled = JObjectHelper.GetBoolValue(jObjectSetup, "enabled");

                project.Setup.PluginName = JObjectHelper.GetStringValue(jObjectSetup, "pluginName");

                if (jObjectSetup["pluginParameters"] != null)
                {
                    var pluginInfo = PluginManager.Instance.GetPluginInfoByName(project.Setup.PluginName);
                    if (pluginInfo != null)
                    {
                        var pluginProperties = pluginInfo.Plugin.GetProperties();
                        project.Setup.PluginParameters = GenericPropertySerializer.DeserializeFromJArrayToDictionary(pluginProperties, (JArray)jObjectSetup["pluginParameters"]);
                    }
                }

                if (jObjectSetup["items"] != null)
                {
                    var jArraySetupItems = (JArray)jObjectSetup["items"];

                    foreach (JObject jObjectSetupItem in jArraySetupItems)
                    {
                        project.Setup.Items.Add(new SetupItem()
                        {
                            Selected = JObjectHelper.GetBoolValue(jObjectSetupItem, "selected"),
                            FileName = JObjectHelper.GetStringValue(jObjectSetupItem, "fileName")
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
                ["pluginName"] = project.Obfuscation.PluginName
            };
            var pluginInfo = PluginManager.Instance.GetPluginInfoByName(project.Obfuscation.PluginName);
            if (pluginInfo != null)
            {
                var pluginProperties = pluginInfo.Plugin.GetProperties();
                jObjectObfuscation["pluginParameters"] = GenericPropertySerializer.SerializeFromDictionaryToJArray(pluginProperties, project.Obfuscation.PluginParameters);
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
                ["pluginName"] = project.Signing.PluginName
            };
            pluginInfo = PluginManager.Instance.GetPluginInfoByName(project.Signing.PluginName);
            if (pluginInfo != null)
            {
                var pluginProperties = pluginInfo.Plugin.GetProperties();
                jObjectSigning["pluginParameters"] = GenericPropertySerializer.SerializeFromDictionaryToJArray(pluginProperties, project.Signing.PluginParameters);
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

            // setup
            var jObjectSetup = new JObject()
            {
                ["enabled"] = project.Setup.Enabled,
                ["pluginName"] = project.Setup.PluginName
            };
            pluginInfo = PluginManager.Instance.GetPluginInfoByName(project.Setup.PluginName);
            if (pluginInfo != null)
            {
                var pluginProperties = pluginInfo.Plugin.GetProperties();
                jObjectSetup["pluginParameters"] = GenericPropertySerializer.SerializeFromDictionaryToJArray(pluginProperties, project.Signing.PluginParameters);
            }

            var jArraySetupItems = new JArray();
            foreach (var signinItem in project.Signing.Items)
            {
                jArraySetupItems.Add(new JObject()
                {
                    ["selected"] = signinItem.Selected,
                    ["fileName"] = signinItem.FileName
                });
            }
            jObjectSetup["items"] = jArraySetupItems;

            jObjectProject["setup"] = jObjectSetup;

            // write all into output file
            File.WriteAllText(fileName, jObjectProject.ToString(), Encoding.UTF8);
        }
    }
}
