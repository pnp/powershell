using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Events
{
    [Cmdlet(VerbsCommon.Get, "EventReceiver")]
  
    public class GetEventReceiver : PnPWebRetrievalsCmdlet<EventReceiverDefinition>
    {
        [Parameter(Mandatory = false, ParameterSetName = "List")]
        public ListPipeBind List;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public EventReceiverPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == "List")
            {
                var list = List.GetList(SelectedWeb);

                if (list != null)
                {
                    if (!ParameterSpecified(nameof(Identity)))
                    {
                        var query = ClientContext.LoadQuery(list.EventReceivers);
                        ClientContext.ExecuteQueryRetry();
                        WriteObject(query, true);
                    }
                    else
                    {
                        WriteObject(Identity.GetEventReceiverOnList(list));
                    }
                }
            }
            else
            {
                if (!ParameterSpecified(nameof(Identity)))
                {
                    var query = ClientContext.LoadQuery(SelectedWeb.EventReceivers);
                    ClientContext.ExecuteQueryRetry();
                    WriteObject(query, true);
                }
                else
                {
                    WriteObject(Identity.GetEventReceiverOnWeb(SelectedWeb));
                }
            }
        }
    }
}