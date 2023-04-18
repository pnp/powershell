using Microsoft.Online.SharePoint.TenantAdministration.Internal;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPTenantServicePrincipal", ConfirmImpact = ConfirmImpact.High)]
    public class EnableTenantServicePrincipal : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue("Do you want to enable the Tenant Service Principal?", "Continue?"))
            {
                var servicePrincipal = new SPOWebAppServicePrincipal(AdminContext);
                servicePrincipal.AccountEnabled = true;
                servicePrincipal.Update();
                AdminContext.Load(servicePrincipal);
                AdminContext.ExecuteQueryRetry();
                WriteObject(servicePrincipal);
            }
        }
    }
}