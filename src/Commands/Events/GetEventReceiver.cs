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
                        WriteObject(Identity.GetEventReceiverOnList(list));
                    }
                    else
                    {
                        var query = ClientContext.LoadQuery(list.EventReceivers);
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
                                WriteObject(Identity.GetEventReceiverOnSite(ClientContext.Site));
                            }
                            else
                            {
                                var query = ClientContext.LoadQuery(ClientContext.Site.EventReceivers);
                                ClientContext.ExecuteQueryRetry();
                                WriteObject(query, true);
                            }
                            break;

                        case Enums.EventReceiverScope.Web:
                            if (ParameterSpecified(nameof(Identity)))
                            {
                                WriteObject(Identity.GetEventReceiverOnWeb(CurrentWeb));
                            }
                            else
                            {
                                var query = ClientContext.LoadQuery(CurrentWeb.EventReceivers);
                                ClientContext.ExecuteQueryRetry();
                                WriteObject(query, true);
                            }
                            break;

                        case Enums.EventReceiverScope.All:
                            var eventReceivers = new List<EventReceiverDefinition>();

                            if (ParameterSpecified(nameof(Identity)))
                            {
                                var webEventReceiver = Identity.GetEventReceiverOnWeb(CurrentWeb);
                                var siteReventReceiver = Identity.GetEventReceiverOnSite(ClientContext.Site);

                                eventReceivers.Add(webEventReceiver);
                                eventReceivers.Add(siteReventReceiver);
                            }
                            else
                            {
                                ClientContext.Load(CurrentWeb.EventReceivers);
                                ClientContext.Load(ClientContext.Site.EventReceivers);
                                ClientContext.ExecuteQueryRetry();

                                eventReceivers.AddRange(CurrentWeb.EventReceivers);
                                eventReceivers.AddRange(ClientContext.Site.EventReceivers);
                            }
                            
                            WriteObject(eventReceivers, true);
                            break;                            
                    }
                    break;
            }
        }
    }
}