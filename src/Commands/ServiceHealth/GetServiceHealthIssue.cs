using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.ServiceHealth
{
    [Cmdlet(VerbsCommon.Get, "PnPServiceHealthIssue")]
    [RequiredMinimalApiPermissions("ServiceHealth.Read.All")]
    public class GetServiceHealthIssue : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(ServiceHealthUtility.GetServiceHealthIssueById(this, Identity, Connection, AccessToken), false);
            }
            else
            {
                WriteObject(ServiceHealthUtility.GetServiceHealthIssues(this, Connection, AccessToken), true);
            }
        }
    }
}