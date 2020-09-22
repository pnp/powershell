using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Events
{
    [Cmdlet(VerbsCommon.Remove, "PnPEventReceiver")]
    public class RemoveEventReceiver : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public EventReceiverPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName="List")]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            // Keep a list with all event receivers to remove for better performance and to avoid the collection changing when removing an item in the collection
            var eventReceiversToDelete = new List<EventReceiverDefinition>();

            if (ParameterSetName == "List")
            {
                var list = List.GetList(SelectedWeb);
                if (list == null)
                    throw new PSArgumentException($"No list found with id, title or url '{List}'", "List");

                if (ParameterSpecified(nameof(Identity)))
                {
                    var eventReceiver = Identity.GetEventReceiverOnList(list);
                    if (eventReceiver != null)
                    {
                        if (Force || (ParameterSpecified("Confirm") && !bool.Parse(MyInvocation.BoundParameters["Confirm"].ToString())) || ShouldContinue(string.Format(Properties.Resources.RemoveEventReceiver, eventReceiver.ReceiverName, eventReceiver.ReceiverId), Properties.Resources.Confirm))
                        {
                            eventReceiversToDelete.Add(eventReceiver);
                        }
                    }
                }
                else
                {
                    var eventReceivers = list.EventReceivers;
                    SelectedWeb.Context.Load(eventReceivers);
                    SelectedWeb.Context.ExecuteQueryRetry();

                    foreach (var eventReceiver in eventReceivers)
                    {
                        if (Force || (ParameterSpecified("Confirm") && !bool.Parse(MyInvocation.BoundParameters["Confirm"].ToString())) || ShouldContinue(string.Format(Properties.Resources.RemoveEventReceiver, eventReceiver.ReceiverName, eventReceiver.ReceiverId), Properties.Resources.Confirm))
                        {
                            eventReceiversToDelete.Add(eventReceiver);
                        }
                    }
                    ClientContext.ExecuteQueryRetry();
                }
            }
            else
            {
                if (ParameterSpecified(nameof(Identity)))
                {
                    var eventReceiver = Identity.GetEventReceiverOnWeb(SelectedWeb);
                    if (eventReceiver != null)
                    {
                        if (Force || (ParameterSpecified("Confirm") && !bool.Parse(MyInvocation.BoundParameters["Confirm"].ToString())) || ShouldContinue(string.Format(Properties.Resources.RemoveEventReceiver, eventReceiver.ReceiverName, eventReceiver.ReceiverId), Properties.Resources.Confirm))
                        {
                            eventReceiversToDelete.Add(eventReceiver);
                        }
                    }
                }
                else
                {
                    var eventReceivers = SelectedWeb.EventReceivers;
                    SelectedWeb.Context.Load(eventReceivers);
                    SelectedWeb.Context.ExecuteQueryRetry();

                    foreach (var eventReceiver in eventReceivers)
                    {
                        if (Force || (ParameterSpecified("Confirm") && !bool.Parse(MyInvocation.BoundParameters["Confirm"].ToString())) || ShouldContinue(string.Format(Properties.Resources.RemoveEventReceiver, eventReceiver.ReceiverName, eventReceiver.ReceiverId), Properties.Resources.Confirm))
                        {
                            eventReceiversToDelete.Add(eventReceiver);
                        }
                    }
                }
            }

            if (eventReceiversToDelete.Count == 0)
            {
                WriteVerbose("No Event Receivers to remove");
                return;
            }

            for(var x = 0; x < eventReceiversToDelete.Count; x++)
            {
                WriteVerbose($"Removing Event Receiver with Id {eventReceiversToDelete[x].ReceiverId} named {eventReceiversToDelete[x].ReceiverName}");
                eventReceiversToDelete[x].DeleteObject();
            }
            SelectedWeb.Context.ExecuteQueryRetry();
        }
    }
}


