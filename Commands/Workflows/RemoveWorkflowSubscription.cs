using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Workflows
{
    [Cmdlet(VerbsCommon.Remove, "PnPWorkflowSubscription")]
    public class RemoveWorkflowSubscription : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public WorkflowSubscriptionPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var identity = Identity.GetWorkflowSubscription(SelectedWeb)
                ?? throw new PSArgumentException($"No workflow subscription found for '{Identity}'", nameof(Identity));
            identity.Delete();
        }
    }

}
