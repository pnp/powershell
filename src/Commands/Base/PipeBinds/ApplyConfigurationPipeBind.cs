using PnP.Framework.Provisioning.Model.Configuration;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ApplyConfigurationPipeBind
    {
        readonly ApplyConfiguration objectValue;
        readonly string value;

        public ApplyConfigurationPipeBind(string str)
        {
            value = str;
        }

        public ApplyConfigurationPipeBind(ApplyConfiguration configuration)
        {
            objectValue = configuration;
        }

        internal ApplyConfiguration GetConfiguration(string currentFileSystemLocation)
        {
            if (objectValue != null)
            {
                return objectValue;
            }
            return ConfigurationPipeBindHelper.Resolve(value, currentFileSystemLocation, ApplyConfiguration.FromString);
        }
    }
}
