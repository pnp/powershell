using PnP.Framework.Provisioning.Model.Configuration;
using System;
using System.Management.Automation;
using CoreExtractConfiguration = PnP.Core.Provisioning.Model.Configuration.ExtractConfiguration;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ExtractConfigurationPipeBind
    {
        readonly ExtractConfiguration objectValue;
        readonly CoreExtractConfiguration coreObjectValue;
        readonly string value;

        public ExtractConfigurationPipeBind(string str)
        {
            value = str;
        }

        public ExtractConfigurationPipeBind(ExtractConfiguration configuration)
        {
            objectValue = configuration;
        }

        public ExtractConfigurationPipeBind(CoreExtractConfiguration configuration)
        {
            coreObjectValue = configuration;
        }

        internal ExtractConfiguration GetConfiguration(string currentFileSystemLocation, Action<string> logWarning = null)
        {
            if (objectValue != null)
            {
                return objectValue;
            }
            if (coreObjectValue != null)
            {
                throw new PSArgumentException("The configuration passed in belongs to the experimental PnP.Core.Provisioning engine. Add -Experimental to use it, or pass the path to the JSON configuration file instead.");
            }
            return ConfigurationPipeBindHelper.Resolve(value, currentFileSystemLocation, ExtractConfiguration.FromString, logWarning);
        }

        /// <summary>
        /// Returns the configuration as a PnP.Core.Provisioning configuration. 
        /// </summary>
        /// <param name="currentFileSystemLocation">The location to resolve a relative path against</param>
        /// <param name="logWarning">Reports parts of the configuration which are not recognized</param>
        internal CoreExtractConfiguration GetCoreConfiguration(string currentFileSystemLocation, Action<string> logWarning = null)
        {
            if (coreObjectValue != null)
            {
                return coreObjectValue;
            }
            if (objectValue != null)
            {
                throw new PSArgumentException("The configuration passed in belongs to the PnP Framework engine and cannot be used with -Experimental. Pass the path to the JSON configuration file instead.");
            }
            return ConfigurationPipeBindHelper.Resolve(value, currentFileSystemLocation, CoreExtractConfiguration.FromString, logWarning);
        }
    }
}
