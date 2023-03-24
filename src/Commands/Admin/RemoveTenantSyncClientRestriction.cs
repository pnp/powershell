using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPTenantSyncClientRestriction")]
    public class RemovePnPTenantSyncClientRestriction : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            this.Tenant.IsUnmanagedSyncClientForTenantRestricted = false;
            this.Tenant.BlockMacSync = false;
            this.Tenant.ExcludedFileExtensionsForSyncClient = new List<string>();
            this.Tenant.OptOutOfGrooveBlock = false;
            this.Tenant.OptOutOfGrooveSoftBlock = false;
            this.Tenant.DisableReportProblemDialog = false;
            AdminContext.ExecuteQueryRetry();
            WriteObject(new SPOTenantSyncClientRestriction(Tenant));
        }
    }
}