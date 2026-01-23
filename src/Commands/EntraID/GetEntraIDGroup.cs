using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.EntraID
{
    [Cmdlet(VerbsCommon.Get, "PnPEntraIDGroup")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    [Alias("Get-PnPAzureADGroup")]
    public class GetAzureADGroup : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public EntraIDGroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                var group = Identity.GetGroup(GraphRequestHelper);
                if (group != null)
                {
                    WriteObject(group);
                }
            }
            else
            {
                var groups = AzureADGroupsUtility.GetGroups(GraphRequestHelper);
                if (groups != null)
                {
                    WriteObject(groups?.OrderBy(m => m.DisplayName), true);
                }
            }
        }
    }
}