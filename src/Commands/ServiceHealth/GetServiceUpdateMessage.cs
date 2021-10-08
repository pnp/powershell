using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.ServiceHealth
{
    [Cmdlet(VerbsCommon.Get, "PnPServiceUpdateMessage")]
    [RequiredMinimalApiPermissions("ServiceMessage.Read.All")]
    public class GetServiceUpdateMessage : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(ServiceHealthUtility.GetServiceUpdateMessageByIdAsync(Identity, HttpClient, AccessToken).GetAwaiter().GetResult(), false);
            }
            else
            {
                WriteObject(ServiceHealthUtility.GetServiceUpdateMessagesAsync(HttpClient, AccessToken).GetAwaiter().GetResult(), true);
            }
        }        
    }
}