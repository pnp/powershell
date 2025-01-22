using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365ExpiringGroup")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.Read.All")]
    public class GetMicrosoft365ExpiringGroup : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeSiteUrl;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeOwners;

        [Parameter(Mandatory = false)]
        public int Limit = 31;

        protected override void ExecuteCmdlet()
        {
            var expiringGroupsResults = Microsoft365GroupsUtility.GetExpiringGroup(RequestHelper, Limit, IncludeSiteUrl, IncludeOwners);

            WriteObject(expiringGroupsResults.Groups.OrderBy(p => p.DisplayName), true);
            if (expiringGroupsResults.Errors.Any())
            {
                throw new AggregateException($"{expiringGroupsResults.Errors.Count} error(s) occurred in a Graph batch request", expiringGroupsResults.Errors);
            }
        }
    }
}
