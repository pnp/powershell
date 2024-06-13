using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Management.Automation;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADGroupOwner")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    [Alias("Get-PnPEntraIDGroupOwner")]
    public class GetAzureADGroupOwner : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADGroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            Group group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(this, Connection, AccessToken);
            }

            if (group != null)
            {
                // Get Owners of the group                
                var owners = Microsoft365GroupsUtility.GetOwnersAsync(this, Connection, new Guid(group.Id), AccessToken).GetAwaiter().GetResult();
                WriteObject(owners?.OrderBy(m => m.DisplayName), true);
            }
        }
    }
}