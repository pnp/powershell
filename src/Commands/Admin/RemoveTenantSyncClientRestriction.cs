using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPTenantSyncClientRestriction")]
    public class RemovePnPTenantSyncClientRestriction : PnPSharePointOnlineAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            Tenant.IsUnmanagedSyncClientForTenantRestricted = false;
            Tenant.BlockMacSync = false;
            Tenant.ExcludedFileExtensionsForSyncClient = new List<string>();
            Tenant.OptOutOfGrooveBlock = false;
            Tenant.OptOutOfGrooveSoftBlock = false;
            Tenant.DisableReportProblemDialog = false;
            AdminContext.ExecuteQueryRetry();
            WriteObject(new SPOTenantSyncClientRestriction(Tenant));
        }
    }
}