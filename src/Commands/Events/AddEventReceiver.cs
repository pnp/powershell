using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Events
{
    [Cmdlet(VerbsCommon.Add, "PnPEventReceiver", DefaultParameterSetName = ParameterSet_SCOPE)]
    [OutputType(typeof(EventReceiverDefinition))]
    public class AddEventReceiver : PnPWebCmdlet
    {
        private const string ParameterSet_LIST = "On a list";
        private const string ParameterSet_SCOPE = "On a web or site";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_LIST)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(ParameterSetName = ParameterSet_LIST)]
        [Parameter(ParameterSetName = ParameterSet_SCOPE)]
        [Parameter(Mandatory = true)]
        public string Name;

        [Parameter(ParameterSetName = ParameterSet_LIST)]
        [Parameter(ParameterSetName = ParameterSet_SCOPE)]
        [Parameter(Mandatory = true)]
        public string Url;

        [Parameter(ParameterSetName = ParameterSet_LIST)]
        [Parameter(ParameterSetName = ParameterSet_SCOPE)]
        [Parameter(Mandatory = true)]
        public EventReceiverType EventReceiverType;

        [Parameter(ParameterSetName = ParameterSet_LIST)]
        [Parameter(ParameterSetName = ParameterSet_SCOPE)]
        [Parameter(Mandatory = true)]
        public EventReceiverSynchronization Synchronization;

        [Parameter(ParameterSetName = ParameterSet_LIST)]
        [Parameter(ParameterSetName = ParameterSet_SCOPE)]
        [Parameter(Mandatory = false)]
        public int SequenceNumber = 1000;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SCOPE)]
        public Enums.EventReceiverScope Scope = Enums.EventReceiverScope.Web;              

        [Parameter(ParameterSetName = ParameterSet_LIST)]
        [Parameter(ParameterSetName = ParameterSet_SCOPE)]
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

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
                    
                    WriteObject(list.AddRemoteEventReceiver(Name, Url, EventReceiverType, Synchronization, SequenceNumber, Force));
                    break;

                case ParameterSet_SCOPE:
                    switch (Scope)
                    {
                        case Enums.EventReceiverScope.Site:
                            WriteObject(ClientContext.Site.AddRemoteEventReceiver(Name, Url, EventReceiverType, Synchronization, SequenceNumber, Force));
                            break;

                        case Enums.EventReceiverScope.Web:
                            WriteObject(CurrentWeb.AddRemoteEventReceiver(Name, Url, EventReceiverType, Synchronization, SequenceNumber, Force));
                            break;

                        default:
                            throw new PSArgumentException($"An event receiver cannot be addedd to the scope {Scope}", nameof(Scope));
                    }
                    break;
            }
        }
    }
}