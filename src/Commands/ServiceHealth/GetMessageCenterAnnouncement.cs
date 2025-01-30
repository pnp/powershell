using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ServiceHealth
{
    [Cmdlet(VerbsCommon.Get, "PnPMessageCenterAnnouncement")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ServiceMessage.Read.All")]
    public class GetMessageCenterAnnouncement : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(ServiceHealthUtility.GetServiceUpdateMessageById(GraphRequestHelper, Identity), false);
            }
            else
            {
                WriteObject(ServiceHealthUtility.GetServiceUpdateMessages(GraphRequestHelper), true);
            }
        }
    }
}