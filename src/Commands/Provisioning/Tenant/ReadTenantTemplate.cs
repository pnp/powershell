using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.IO;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommunications.Read, "PnPTenantTemplate", DefaultParameterSetName = ParameterSet_PATH)]
    public class ReadTenantTemplate : BasePSCmdlet
    {
        const string ParameterSet_STREAM = "By Stream";
        const string ParameterSet_PATH = "By Path";
        const string ParameterSet_XML = "By XML";

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_STREAM)]
        public Stream Stream;

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_PATH)]
        public string Path;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSet_XML)]
        public string Xml;

        protected override void ProcessRecord()
        {
            switch (ParameterSetName)
            {
                case ParameterSet_PATH:
                    {

                        if (!System.IO.Path.IsPathRooted(Path))
                        {
                            Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                        }
                        WriteObject(ProvisioningHelper.LoadTenantTemplateFromFile(Path, (e) =>
                        {
                            LogError(e);
                        }));
                        break;
                    }
                case ParameterSet_XML:
                    {
                        WriteObject(ProvisioningHelper.LoadTenantTemplateFromString(Xml, (e) =>
                        {
                            LogError(e);
                        }));
                        break;
                    }
                case ParameterSet_STREAM:
                    {
                        WriteObject(ProvisioningHelper.LoadTenantTemplatesFromStream(Stream, (e) =>
                        {
                            LogError(e);
                        }), true);
                        break;
                    }                    
            }
        }           
    }
}