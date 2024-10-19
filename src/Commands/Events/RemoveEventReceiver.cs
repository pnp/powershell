using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Events
{
    [Cmdlet(VerbsCommon.Remove, "PnPEventReceiver", DefaultParameterSetName = ParameterSet_SCOPE)]
    [OutputType(typeof(void))]
    public class RemoveEventReceiver : PnPWebCmdlet
    {
        private const string ParameterSet_LIST = "From a list";
        private const string ParameterSet_SCOPE = "From a web or site";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_LIST)]
        public ListPipeBind List;

        [Parameter(ParameterSetName = ParameterSet_LIST)]
        [Parameter(ParameterSetName = ParameterSet_SCOPE)]
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public EventReceiverPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SCOPE)]
        public Enums.EventReceiverScope Scope = Enums.EventReceiverScope.Web;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            // Keep a list with all event receivers to remove for better performance and to avoid the collection changing when removing an item in the collection
            var eventReceiversToDelete = new List<EventReceiverDefinition>();

            switch (ParameterSetName)
            {
                case ParameterSet_LIST:
                    var list = List.GetList(CurrentWeb);

                    if (list == null)
                    {
                        throw new PSArgumentException("The provided List could not be found", nameof(List));
                    }

                    if (ParameterSpecified(nameof(Identity)))
                    {
                        var eventReceiver = Identity.GetEventReceiverOnList(list);
                        if (eventReceiver != null)
                        {
                            eventReceiversToDelete.Add(eventReceiver);
                        }
                    }
                    else
                    {
                        ClientContext.Load(list.EventReceivers);
                        ClientContext.ExecuteQueryRetry();

                        foreach (var eventReceiver in list.EventReceivers)
                        {
                            eventReceiversToDelete.Add(eventReceiver);
                        }
                    }
                    break;

                case ParameterSet_SCOPE:
                    switch (Scope)
                    {
                        case Enums.EventReceiverScope.Site:
                            if (ParameterSpecified(nameof(Identity)))
                            {
                                eventReceiversToDelete.Add(Identity.GetEventReceiverOnSite(ClientContext.Site));
                            }
                            else
                            {
                                ClientContext.Load(ClientContext.Site.EventReceivers);
                                ClientContext.ExecuteQueryRetry();
                                eventReceiversToDelete.AddRange(ClientContext.Site.EventReceivers);
                            }
                            break;

                        case Enums.EventReceiverScope.Web:
                            if (ParameterSpecified(nameof(Identity)))
                            {
                                eventReceiversToDelete.Add(Identity.GetEventReceiverOnWeb(CurrentWeb));
                            }
                            else
                            {
                                ClientContext.Load(CurrentWeb.EventReceivers);
                                ClientContext.ExecuteQueryRetry();
                                eventReceiversToDelete.AddRange(CurrentWeb.EventReceivers);
                            }
                            break;

                        case Enums.EventReceiverScope.All:
                            if (ParameterSpecified(nameof(Identity)))
                            {
                                var webEventReceiver = Identity.GetEventReceiverOnWeb(CurrentWeb);
                                var siteReventReceiver = Identity.GetEventReceiverOnSite(ClientContext.Site);

                                eventReceiversToDelete.Add(webEventReceiver);
                                eventReceiversToDelete.Add(siteReventReceiver);
                            }
                            else
                            {
                                ClientContext.Load(CurrentWeb.EventReceivers);
                                ClientContext.Load(ClientContext.Site.EventReceivers);
                                ClientContext.ExecuteQueryRetry();

                                eventReceiversToDelete.AddRange(CurrentWeb.EventReceivers);
                                eventReceiversToDelete.AddRange(ClientContext.Site.EventReceivers);
                            }
                            break;
                    }
                    break;
            }

            if (eventReceiversToDelete.Count == 0)
            {
                WriteVerbose("No Event Receivers to remove");
                return;
            }

            for (var x = 0; x < eventReceiversToDelete.Count; x++)
            {
                var eventReceiver = eventReceiversToDelete[x];

                if (Force || ShouldContinue(string.Format(Properties.Resources.RemoveEventReceiver, eventReceiver.ReceiverName, eventReceiver.ReceiverId), Properties.Resources.Confirm))
                {
                    WriteVerbose($"Removing Event Receiver with Id {eventReceiver.ReceiverId} named {eventReceiver.ReceiverName}");
                    eventReceiver.DeleteObject();
                }
            }
            ClientContext.ExecuteQueryRetry();
        }
    }
}


