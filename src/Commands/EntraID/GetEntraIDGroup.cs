using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.EntraID
{
    [Cmdlet(VerbsCommon.Get, "PnPEntraIDGroup")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    [Alias("Get-PnPAzureADGroup")]
    public class GetEntraIDGroup : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public EntraIDGroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                var group = Identity.GetGroup(Connection, AccessToken);
                if (group != null)
                {
                    WriteObject(group);
                }
            }
            else
            {
                var groups = EntraIDGroupsUtility.GetGroupsAsync(Connection, AccessToken).GetAwaiter().GetResult();
                if (groups != null)
                {
                    WriteObject(groups?.OrderBy(m => m.DisplayName), true);
                }
            }
        }
    }
}