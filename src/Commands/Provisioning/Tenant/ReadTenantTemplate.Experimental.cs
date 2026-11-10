using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    public partial class ReadTenantTemplate
    {
        private void ProcessRecordExperimental()
        {
            switch (ParameterSetName)
            {
                case ParameterSet_PATH:
                    if (!System.IO.Path.IsPathRooted(Path))
                    {
                        Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                    }
                    WriteObject(CoreProvisioningHelper.LoadTenantTemplateFromFile(Path, LogError));
                    break;

                case ParameterSet_XML:
                    WriteObject(CoreProvisioningHelper.LoadTenantTemplateFromString(Xml, LogError));
                    break;

                case ParameterSet_STREAM:
                    WriteObject(CoreProvisioningHelper.LoadTenantTemplateFromStream(Stream, LogError));
                    break;
            }
        }
    }
}
