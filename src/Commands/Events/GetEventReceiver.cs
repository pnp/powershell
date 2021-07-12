using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Events
{
    [Cmdlet(VerbsCommon.Get, "PnPEventReceiver")]
  
    public class GetEventReceiver : PnPWebRetrievalsCmdlet<EventReceiverDefinition>
    {
        [Parameter(Mandatory = true, ParameterSetName = "List")]
        public ListPipeBind List;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public EventReceiverPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = "Scope")]
        public SwitchParameter Site;
        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == "List")
            {
                var list = List.GetList(CurrentWeb);

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
                if (Site)
                {
                    var site = ClientContext.Site;
                    if (site != null)
                    {
                        if (!ParameterSpecified(nameof(Identity)))
                        {
                            var query = ClientContext.LoadQuery(site.EventReceivers);
                            ClientContext.ExecuteQueryRetry();
                            WriteObject(query, true);
                        }
                        else
                        {
                            WriteObject(Identity.GetEventReceiverOnSite(site));
                        }
                    }
                }
                else
                {   
                    if (!ParameterSpecified(nameof(Identity)))
                    {
                        var query = ClientContext.LoadQuery(CurrentWeb.EventReceivers);
                        ClientContext.ExecuteQueryRetry();
                        WriteObject(query, true);
                    }
                    else
                    {
                        WriteObject(Identity.GetEventReceiverOnWeb(CurrentWeb));
                    }
                }
            }
        }
    }
}