using System.Management.Automation;
using System.Linq;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPDeletedMicrosoft365Group")]
    [RequiredApiApplicationPermissions("graph/Group.Read.All")]
    [RequiredApiApplicationPermissions("graph/Group.ReadWrite.All")]
    public class GetDeletedMicrosoft365Group : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                WriteObject(Identity.GetDeletedGroup(this, Connection, AccessToken));
            }
            else
            {
                var groups = ClearOwners.GetDeletedGroups(this, Connection, AccessToken);
                WriteObject(groups.OrderBy(g => g.DisplayName), true);
            }
        }
    }
}