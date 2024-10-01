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
    [RequiredApiApplicationPermissions("https://graph.microsoft.com/Group.Read.All")]
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
                var owners = ClearOwners.GetOwners(this, Connection, new Guid(group.Id), AccessToken);
                WriteObject(owners?.OrderBy(m => m.DisplayName), true);
            }
        }
    }
}