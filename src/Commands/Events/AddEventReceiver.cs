using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Events
{
    [Cmdlet(VerbsCommon.Add, "PnPEventReceiver")]
    public class AddEventReceiver : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
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

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(List)))
            {
                var list = List.GetList(CurrentWeb);
                WriteObject(list.AddRemoteEventReceiver(Name, Url, EventReceiverType, Synchronization, SequenceNumber, Force));
            }
            else
            {
                WriteObject(CurrentWeb.AddRemoteEventReceiver(Name, Url, EventReceiverType, Synchronization, SequenceNumber, Force));
            }
        }
    }
}