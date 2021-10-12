using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.AzureAD;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADGroup")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetAzureADGroup : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public AzureADGroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {

            if (Identity != null)
            {
                var group = Identity.GetGroup(AccessToken);
                if (group != null)
                {
                    WriteObject(group);
                }
            }
            else
            {
                var groups = GroupsUtility.GetGroups(AccessToken);
                if (groups != null && groups.Any())
                {
                    WriteObject(groups.Select(e => AzureADGroup.CreateFrom(e)), true);
                }
            }
        }
    }
}