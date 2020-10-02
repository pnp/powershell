using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.WorkflowServices;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Workflows
{
    [Cmdlet(VerbsCommon.Get, "WorkflowSubscription")]
    public class GetWorkflowSubscription : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public string Name;

        [Parameter(Mandatory = false, Position = 1)]
        public ListPipeBind List;
        protected override void ExecuteCmdlet()
        {
            if (List != null)
            {
                var list = List.GetList(SelectedWeb);
                if (list == null)
                    throw new PSArgumentException($"No list found with id, title or url '{List}'", "List");

                if (string.IsNullOrEmpty(Name))
                {
                    var servicesManager = new WorkflowServicesManager(ClientContext, SelectedWeb);
                    var subscriptionService = servicesManager.GetWorkflowSubscriptionService();
                    var subscriptions = subscriptionService.EnumerateSubscriptionsByList(list.Id);

                    ClientContext.Load(subscriptions);

                    ClientContext.ExecuteQueryRetry();
                    WriteObject(subscriptions, true);
                }
                else
                {
                    WriteObject(list.GetWorkflowSubscription(Name));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Name))
                {
                    var servicesManager = new WorkflowServicesManager(ClientContext, SelectedWeb);
                    var subscriptionService = servicesManager.GetWorkflowSubscriptionService();
                    var subscriptions = subscriptionService.EnumerateSubscriptions();

                    ClientContext.Load(subscriptions);

                    ClientContext.ExecuteQueryRetry();
                    WriteObject(subscriptions, true);
                }
                else
                {
                    WriteObject(SelectedWeb.GetWorkflowSubscription(Name));
                }
            }
        }
    }

}
