using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Workflows
{
    [Cmdlet(VerbsCommon.Get, "PnPWorkflowInstance", DefaultParameterSetName = ParameterSet_BYLISTITEM)]
    public class GetWorkflowInstance : PnPWebCmdlet
    {
        private const string ParameterSet_BYLISTITEM = "By List and ListItem";
        private const string ParameterSet_BYSUBSCRIPTION = "By WorkflowSubscription";

        protected override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case ParameterSet_BYLISTITEM:
                    ExecuteCmdletByListItem();
                    break;
                case ParameterSet_BYSUBSCRIPTION:
                    ExecuteCmdletBySubscription();
                    break;
                default:
                    throw new NotImplementedException($"{nameof(ParameterSetName)}: {ParameterSetName}");
            }
        }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYLISTITEM, Position = 0)]
        public ListPipeBind List;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYLISTITEM, Position = 1)]
        public ListItemPipeBind ListItem;

        private void ExecuteCmdletByListItem()
        {
            List list = null;
            ListItem listitem = null;

            if (List != null)
            {
                list = List.GetList(SelectedWeb);
                if (list == null)
                {
                    throw new PSArgumentException($"No list found with id, title or url '{List}'", nameof(List));
                }
            }
            else
            {
                throw new PSArgumentException("List required");
            }

            if (ListItem != null)
            {
                listitem = ListItem.GetListItem(list);
                if (listitem == null)
                {
                    throw new PSArgumentException($"No list item found with id, or title '{ListItem}'", nameof(ListItem));
                }
            }
            else
            {
                throw new PSArgumentException("List Item required");
            }

            var workflowServicesManager = new Microsoft.SharePoint.Client.WorkflowServices.WorkflowServicesManager(ClientContext, SelectedWeb);
            var workflowInstanceService = workflowServicesManager.GetWorkflowInstanceService();
            var workflows = workflowInstanceService.EnumerateInstancesForListItem(list.Id, listitem.Id);
            ClientContext.Load(workflows);
            ClientContext.ExecuteQueryRetry();
            WriteObject(workflows, true);
        }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYSUBSCRIPTION, Position = 0, ValueFromPipeline = true)]
        public WorkflowSubscriptionPipeBind WorkflowSubscription;

        private void ExecuteCmdletBySubscription()
        {
            var workflowSubscription = WorkflowSubscription.GetWorkflowSubscription(SelectedWeb);
            if (workflowSubscription == null)
            {
                throw new PSArgumentException($"No workflow subscription found for '{WorkflowSubscription}'", nameof(WorkflowSubscription));
            }

            var workflows = workflowSubscription.GetInstances();
            WriteObject(workflows, true);
        }
    }
}
