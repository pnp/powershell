using Microsoft.Online.SharePoint.TenantAdministration.Internal;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsLifecycle.Disable, "PnPTenantServicePrincipal", ConfirmImpact = ConfirmImpact.High)]
    public class DisableTenantServicePrincipal : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (ShouldContinue("Do you want to disable the Tenant Service Principal?", Properties.Resources.Confirm))
            {
                var servicePrincipal = new SPOWebAppServicePrincipal(AdminContext);
                servicePrincipal.AccountEnabled = false;
                servicePrincipal.Update();
                AdminContext.Load(servicePrincipal);
                AdminContext.ExecuteQueryRetry();
                WriteObject(servicePrincipal);
            }
        }
    }
}