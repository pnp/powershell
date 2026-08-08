using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Events
{
    [Cmdlet(VerbsCommon.Get, "PnPEventReceiver", DefaultParameterSetName = ParameterSet_SCOPE)]
    [OutputType(typeof(EventReceiverDefinition))]

    public class GetEventReceiver : PnPWebRetrievalsCmdlet<EventReceiverDefinition>
    {
        private const string ParameterSet_LIST = "On a list";
        private const string ParameterSet_SCOPE = "On a web or site";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_LIST)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(ParameterSetName = ParameterSet_LIST)]
        [Parameter(ParameterSetName = ParameterSet_SCOPE)]
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public EventReceiverPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SCOPE)]
        public Enums.EventReceiverScope Scope = Enums.EventReceiverScope.Web;  
        protected override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case ParameterSet_LIST:
                    var list = List.GetList(CurrentWeb);
                   
                    if(list == null)
                    {
                        throw new PSArgumentException("The provided List could not be found", nameof(List));
                    }

                    if (ParameterSpecified(nameof(Identity)))
                    {
                        WriteObject(EnsureRetrievals(Identity.GetEventReceiverOnList(list)));
                    }
                    else
                    {
                        var query = ClientContext.LoadQuery(list.EventReceivers.IncludeWithDefaultProperties(RetrievalExpressions));
                        ClientContext.ExecuteQueryRetry();
                        WriteObject(query, true);
                    }
                    break;

                case ParameterSet_SCOPE:
                    switch (Scope)
                    {
                        case Enums.EventReceiverScope.Site:
                            if (ParameterSpecified(nameof(Identity)))
                            {
                                WriteObject(EnsureRetrievals(Identity.GetEventReceiverOnSite(ClientContext.Site)));
                            }
                            else
                            {
                                var query = ClientContext.LoadQuery(ClientContext.Site.EventReceivers.IncludeWithDefaultProperties(RetrievalExpressions));
                                ClientContext.ExecuteQueryRetry();
                                WriteObject(query, true);
                            }
                            break;

                        case Enums.EventReceiverScope.Web:
                            if (ParameterSpecified(nameof(Identity)))
                            {
                                WriteObject(EnsureRetrievals(Identity.GetEventReceiverOnWeb(CurrentWeb)));
                            }
                            else
                            {
                                var query = ClientContext.LoadQuery(CurrentWeb.EventReceivers.IncludeWithDefaultProperties(RetrievalExpressions));
                                ClientContext.ExecuteQueryRetry();
                                WriteObject(query, true);
                            }
                            break;

                        case Enums.EventReceiverScope.All:
                            var eventReceivers = new List<EventReceiverDefinition>();

                            if (ParameterSpecified(nameof(Identity)))
                            {
                                var webEventReceiver = EnsureRetrievals(Identity.GetEventReceiverOnWeb(CurrentWeb));
                                var siteReventReceiver = EnsureRetrievals(Identity.GetEventReceiverOnSite(ClientContext.Site));

                                eventReceivers.Add(webEventReceiver);
                                eventReceivers.Add(siteReventReceiver);
                            }
                            else
                            {
                                var webEventReceivers = ClientContext.LoadQuery(CurrentWeb.EventReceivers.IncludeWithDefaultProperties(RetrievalExpressions));
                                var siteEventReceivers = ClientContext.LoadQuery(ClientContext.Site.EventReceivers.IncludeWithDefaultProperties(RetrievalExpressions));
                                ClientContext.ExecuteQueryRetry();

                                eventReceivers.AddRange(webEventReceivers);
                                eventReceivers.AddRange(siteEventReceivers);
                            }
                            
                            WriteObject(eventReceivers, true);
                            break;                            
                    }
                    break;
            }
        }

        /// <summary>
        /// Loads the properties requested through -Includes on an event receiver which has been retrieved by its id or name. Returns the event receiver so it can be used inline.
        /// </summary>
        private EventReceiverDefinition EnsureRetrievals(EventReceiverDefinition eventReceiver)
        {
            if (eventReceiver != null && RetrievalExpressions.Length > 0)
            {
                eventReceiver.EnsureProperties(RetrievalExpressions);
            }
            return eventReceiver;
        }
    }
}