using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.VivaEngage;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Viva
{
    [Cmdlet(VerbsCommon.Get, "PnPVivaEngageCommunity")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Community.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Community.ReadWrite.All")]
    public class GetVivaEngageCommunity : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public string Identity;
        protected override void ExecuteCmdlet()
        {
            string endpointUrl = "/v1.0/employeeExperience/communities";
            if (!string.IsNullOrEmpty(Identity))
            {
                endpointUrl += $"/{Identity}";
                var community = RequestHelper.Get<VivaEngageCommunity>(endpointUrl);
                WriteObject(community);
            }
            else
            {
                var communities = RequestHelper.GetResultCollection<VivaEngageCommunity>(endpointUrl);
                WriteObject(communities, true);
            }
        }
    }
}
