using QuantumBuilder.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Shared.Plugin
{
    public class PluginManager : Singleton<PluginManager>
    {
        public List<PluginInfo> PluginInfos { get; set; }

        public PluginManager()
        {
            PluginInfos = new List<PluginInfo>();
        }

        public void LoadPluginsFromPath(string path)
        {
            var fileNames = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly);
            foreach (var fileName in fileNames)
            {
                var assemblyName = AssemblyName.GetAssemblyName(fileName);
                var assembly = Assembly.Load(assemblyName);

                var assemblyTypes = assembly.GetTypes();
                foreach (var assemblyType in assemblyTypes)
                {
                    if (assemblyType.IsSubclassOf(typeof(Plugin)))
                    {
                        var plugin = (Plugin)Activator.CreateInstance(assemblyType);
                        
                        PluginInfos.Add(new PluginInfo()
                        {
                            FileName = fileName,
                            Plugin = plugin,
                            PluginProperties = plugin.GetProperties()
                        });
                    }
                }
            }
        }

        public List<PluginInfo> GetPluginInfosForType(PluginType pluginType)
        {
            var result = new List<PluginInfo>();

            foreach (var pluginInfo in PluginInfos)
                if (pluginInfo.Plugin.Type == pluginType)
                    result.Add(pluginInfo);

            return result;
        }

        public PluginInfo GetPluginInfoByName(string pluginName)
        {
            foreach (var pluginInfo in PluginInfos)
                if (pluginInfo.Plugin.Name == pluginName)
                    return pluginInfo;

            return null;
        }
    }
}
