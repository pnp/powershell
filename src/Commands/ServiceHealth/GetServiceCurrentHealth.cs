using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ServiceHealth
{
    [Cmdlet(VerbsCommon.Get, "PnPServiceCurrentHealth")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ServiceHealth.Read.All")]
    public class GetServiceCurrentHealth : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(ServiceHealthUtility.GetServiceCurrentHealthById(RequestHelper, Identity), false);
            }
            else
            {
                WriteObject(ServiceHealthUtility.GetServiceCurrentHealth(RequestHelper), true);
            }
        }
    }
}