using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.New, "SdnProvider", SupportsShouldProcess = true)]
    public class NewSdnProvider : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Identity;

        [Parameter(Mandatory = true)]
        public string License;

        protected override void ExecuteCmdlet()
        {
            if (ShouldProcess("Adds a new SDN Provider"))
            {
                this.Tenant.AddSdnProvider(Identity, License);
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}