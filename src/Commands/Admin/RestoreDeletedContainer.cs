using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsData.Restore, "PnPDeletedContainer")]
    public class RestoreDeletedContainer : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Identity { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue($"Restore container {Identity}?", Properties.Resources.Confirm))
                {
                    LogDebug($"Restoring container {Identity}");
                    Tenant.RestoreSPODeletedContainerByContainerId(Identity);
                    AdminContext.ExecuteQueryRetry();
                    LogDebug($"Restored container {Identity}");
            }
        }
    }
}
