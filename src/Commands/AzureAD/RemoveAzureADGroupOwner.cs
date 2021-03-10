using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.AzureAD;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Remove, "PnPAzureADGroupOwner")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class RemoveAzureADGroupOwner : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADGroupPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string[] Users;

        protected override void ExecuteCmdlet()
        {
            AzureADGroup group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken);
            }

            if (group != null)
            {
                GroupsUtility.RemoveGroupOwners(group.Id, Users, AccessToken);
            }
        }
    }
}