using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.New, "PnPSdnProvider")]
    public class NewSdnProvider : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Identity;

        [Parameter(Mandatory = true)]
        public string License;

        protected override void ExecuteCmdlet()
        {
            if (ShouldContinue("Add a new SDN Provider", Properties.Resources.Confirm))
            {
                Tenant.AddSdnProvider(Identity, License);
                AdminContext.ExecuteQueryRetry();
            }
        }
    }
}