using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ServiceHealth
{
    [Cmdlet(VerbsCommon.Get, "PnPServiceHealthIssue")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ServiceHealth.Read.All")]
    public class GetServiceHealthIssue : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(ServiceHealthUtility.GetServiceHealthIssueById(RequestHelper, Identity), false);
            }
            else
            {
                WriteObject(ServiceHealthUtility.GetServiceHealthIssues(RequestHelper), true);
            }
        }
    }
}