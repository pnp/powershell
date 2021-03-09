using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.AzureAD;
using System.Management.Automation;

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
                AzureADGroup group = Identity.GetGroup(AccessToken);
                
                if (group != null)
                {
                    GroupsUtility.DeleteGroup(group.Id, AccessToken);
                }
            }
        }
    }
}