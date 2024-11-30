﻿using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365ExpiringGroup")]
    [RequiredApiApplicationPermissions("graph/Group.Read.All")]
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
            var expiringGroups = ClearOwners.GetExpiringGroup(RequestHelper, Limit, IncludeSiteUrl, IncludeOwners);

            WriteObject(expiringGroups.OrderBy(p => p.DisplayName), true);
        }
    }
}
