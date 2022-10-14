using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPExpiringMicrosoft365Groups")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetExpiringMicrosoft365Groups : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeSiteUrl;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeOwners;

        [Parameter(Mandatory = false)]
        public int Limit = 31;
        
        protected override void ExecuteCmdlet()
        {       
            var expiringGroups = Microsoft365GroupsUtility.GetExpiringGroupsAsync(Connection, AccessToken, Limit, IncludeSiteUrl, IncludeOwners).GetAwaiter().GetResult();

            WriteObject(expiringGroups.OrderBy(p => p.DisplayName), true);
        }
    }
}