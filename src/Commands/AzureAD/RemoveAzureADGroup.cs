using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Remove, "PnPAzureADGroup")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class RemoveAzureADGroup : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADGroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                Group group = Identity.GetGroup(Connection, AccessToken);

                if (group != null)
                {
                    Microsoft365GroupsUtility.RemoveGroupAsync(Connection, new System.Guid(group.Id), AccessToken).GetAwaiter().GetResult();
                }
            }
        }
    }
}