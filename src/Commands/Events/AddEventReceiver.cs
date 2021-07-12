using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Events
{
    [Cmdlet(VerbsCommon.Add, "PnPEventReceiver")]
    public class AddEventReceiver : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "List")]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public string Name;

        [Parameter(Mandatory = true)]
        public string Url;

        [Parameter(Mandatory = true)]
        public EventReceiverType EventReceiverType;

        [Parameter(Mandatory = true)]
        public EventReceiverSynchronization Synchronization;

        [Parameter(Mandatory = false)]
        public int SequenceNumber = 1000;

        [Parameter(Mandatory = false, ParameterSetName = "Scope")]
        public SwitchParameter Site;        

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == "List")
            {
                if (ParameterSpecified(nameof(List)))
                {
                    var list = List.GetList(CurrentWeb);
                    WriteObject(list.AddRemoteEventReceiver(Name, Url, EventReceiverType, Synchronization, SequenceNumber, Force));
                }
                else
                {
                    WriteWarning("Provide a list");
                }
            }
            else
            {
                if (Site)
                {
                    var site = ClientContext.Site;
                    if (site != null)
                    {
                        WriteObject(site.AddRemoteEventReceiver(Name, Url, EventReceiverType, Synchronization, SequenceNumber, Force));
                    }
                }
                else
                {
                    WriteObject(CurrentWeb.AddRemoteEventReceiver(Name, Url, EventReceiverType, Synchronization, SequenceNumber, Force));
                }
            }
        }
    }
}