using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;
using Microsoft.SharePoint.Client.WorkflowServices;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Workflows
{
    [Cmdlet(VerbsLifecycle.Start, "PnPWorkflowInstance")]
    public class StartWorkflowInstance : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public WorkflowSubscriptionPipeBind Subscription;

        [Parameter(Mandatory = true, Position = 1)]
        public ListItemPipeBind ListItem;

        protected override void ExecuteCmdlet()
        {
            int ListItemID;
            if (ListItem != null)
            {
                if (ListItem.Id != uint.MinValue)
                {
                    ListItemID = (int)ListItem.Id;
                }
                else if (ListItem.Item != null)
                {
                    ListItemID = ListItem.Item.Id;
                }
                else
                {
                    throw new PSArgumentException("No valid list item specified.", nameof(ListItem));
                }
            }
            else
            {
                throw new PSArgumentException("List Item is required", nameof(ListItem));
            }

            var subscription = Subscription.GetWorkflowSubscription(SelectedWeb)
                ?? throw new PSArgumentException($"No workflow subscription found for '{Subscription}'", nameof(Subscription));

            var inputParameters = new Dictionary<string, object>();

            WorkflowServicesManager workflowServicesManager = new WorkflowServicesManager(ClientContext, SelectedWeb);
            WorkflowInstanceService instanceService = workflowServicesManager.GetWorkflowInstanceService();

            instanceService.StartWorkflowOnListItem(subscription, ListItemID, inputParameters);
            ClientContext.ExecuteQueryRetry();
        }
    }
}
