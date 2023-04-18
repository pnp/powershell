using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPHomeSite")]
    public class RemoveHomeSite : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var homesiteUrl = Tenant.GetSPHSiteUrl();
            AdminContext.ExecuteQueryRetry();
            if (!string.IsNullOrEmpty(homesiteUrl.Value))
            {
                if (Force || ShouldContinue($"Remove {homesiteUrl.Value} as the home site?", Properties.Resources.Confirm))
                {
                    Tenant.RemoveSPHSite();
                    AdminContext.ExecuteQueryRetry();
                }
            }
            else
            {
                WriteWarning("There is currently not site collection set as a home site in your tenant.");
            }
        }
    }
}