using PnP.Framework.Provisioning.Model.Configuration;

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

        internal ExtractConfiguration GetConfiguration(string currentFileSystemLocation)
        {
            if (objectValue != null)
            {
                return objectValue;
            }
            return ConfigurationPipeBindHelper.Resolve(value, currentFileSystemLocation, ExtractConfiguration.FromString);
        }
    }
}
