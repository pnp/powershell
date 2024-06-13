using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.ServiceHealth
{
    [Cmdlet(VerbsCommon.Get, "PnPMessageCenterAnnouncement")]
    [RequiredMinimalApiPermissions("ServiceMessage.Read.All")]
    public class GetMessageCenterAnnouncement : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(ServiceHealthUtility.GetServiceUpdateMessageByIdAsync(this, Identity, Connection, AccessToken).GetAwaiter().GetResult(), false);
            }
            else
            {
                WriteObject(ServiceHealthUtility.GetServiceUpdateMessagesAsync(this, Connection, AccessToken).GetAwaiter().GetResult(), true);
            }
        }        
    }
}