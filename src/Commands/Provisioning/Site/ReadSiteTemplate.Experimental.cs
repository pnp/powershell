using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Provisioning
{
    public partial class ReadSiteTemplate
    {
        private void ProcessRecordExperimental()
        {
            if (ParameterSpecified(nameof(TemplateProviderExtensions)))
            {
                LogWarning("-TemplateProviderExtensions has no effect with -Experimental: template provider extensions are not run by the PnP.Core.Provisioning engine.");
            }

            switch (ParameterSetName)
            {
                case ParameterSet_PATH:
                    if (!System.IO.Path.IsPathRooted(Path))
                    {
                        Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                    }
                    WriteObject(CoreProvisioningHelper.LoadSiteTemplateFromFile(Path, LogError));
                    break;

                case ParameterSet_XML:
                    WriteObject(CoreProvisioningHelper.LoadSiteTemplateFromString(Xml, LogError));
                    break;

                case ParameterSet_STREAM:
                    WriteObject(CoreProvisioningHelper.LoadSiteTemplatesFromStream(Stream, LogError), true);
                    break;
            }
        }
    }
}
