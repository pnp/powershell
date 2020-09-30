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
        [Alias("Type")]
        public EventReceiverType EventReceiverType;

        [Parameter(Mandatory = true)]
        [Alias("Sync")]
        public EventReceiverSynchronization Synchronization;

        [Parameter(Mandatory = false)]
        public int SequenceNumber = 1000;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(List)))
            {
                var list = List.GetList(SelectedWeb);
                WriteObject(list.AddRemoteEventReceiver(Name, Url, EventReceiverType, Synchronization, SequenceNumber, Force));
            }
            else
            {
                WriteObject(SelectedWeb.AddRemoteEventReceiver(Name, Url, EventReceiverType, Synchronization, SequenceNumber, Force));
            }
        }
    }
}