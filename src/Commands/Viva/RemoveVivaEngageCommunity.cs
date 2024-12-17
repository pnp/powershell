using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Viva
{
    [Cmdlet(VerbsCommon.Remove, "PnPVivaEngageCommunity")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Community.ReadWrite.All")]
    public class RemoveVivaEngageCommunity : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            string endpointUrl = $"/v1.0/employeeExperience/communities/{Identity}";
            RequestHelper.Delete(endpointUrl);
        }
    }
}
