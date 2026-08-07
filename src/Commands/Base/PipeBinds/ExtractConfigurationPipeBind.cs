using PnP.Framework.Provisioning.Model.Configuration;
using System;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ExtractConfigurationPipeBind
    {
        readonly ExtractConfiguration objectValue;
        readonly string value;

        public ExtractConfigurationPipeBind(string str)
        {
            value = str;
        }

        public ExtractConfigurationPipeBind(ExtractConfiguration configuration)
        {
            objectValue = configuration;
        }

        internal ExtractConfiguration GetConfiguration(string currentFileSystemLocation, Action<string> logWarning = null)
        {
            if (objectValue != null)
            {
                return objectValue;
            }
            return ConfigurationPipeBindHelper.Resolve(value, currentFileSystemLocation, ExtractConfiguration.FromString, logWarning);
        }
    }
}
