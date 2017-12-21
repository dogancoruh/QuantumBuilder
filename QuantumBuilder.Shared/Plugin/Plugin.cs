using Newtonsoft.Json.Linq;
using Quantum.Framework.GenericProperties.Data;
using QuantumBuilder.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumBuilder.Shared.Plugin
{
    public class Plugin
    {
        public event EventHandler<PluginLogEventArgs> OnLog;
        public event EventHandler<PluginErrorEventArgs> OnError;

        public virtual string Name { get; }
        public virtual string DisplayName { get; }
        public virtual string Description { get; }

        public virtual PluginType Type { get; }

        public virtual GenericPropertyCollection GetProperties()
        {
            throw new NotImplementedException();
        }

        public virtual void Execute(Dictionary<string, object> dictionary)
        {

        }

        public virtual void Terminate()
        {

        }

        public virtual void DeserializeProperties(JArray jArray)
        {

        }

        public virtual JArray SerializeProperties()
        {
            return null;
        }

        protected void Log(string prefix, string text)
        {
            OnLog?.Invoke(this, new PluginLogEventArgs()
            {
                Prefix = prefix,
                Text = text
            });
        }

        protected void Error(string prefix, string text)
        {
            OnError?.Invoke(this, new PluginErrorEventArgs()
            {
                Prefix = prefix,
                Text = text
            });
        }
    }
}
