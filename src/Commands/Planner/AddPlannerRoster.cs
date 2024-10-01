using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;

namespace SharePointPnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Add, "PnPPlannerRoster")]
    [RequiredApiApplicationPermissions("https://graph.microsoft.com/Tasks.ReadWrite")]
    public class AddPlannerRoster : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            PlannerUtility.CreateRoster(this, Connection, AccessToken);
        }
    }
}