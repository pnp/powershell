using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.MultiGeo;
using System;
using System.Globalization;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsLifecycle.Stop, "PnPTenantRename", SupportsShouldProcess = true)]
    [RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
    [OutputType(typeof(string))]
    public class StopTenantRename : PnPSharePointOnlineAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            if (!ShouldProcess(AdminContext.Url, "Stop the scheduled SharePoint Online tenant rename"))
            {
                return;
            }

            var multiGeoRestApiClient = new MultiGeoRestApiClient(AdminContext);
            multiGeoRestApiClient.CancelTenantRenameJob();

            // Same timestamp format as Stop-SPOTenantRename reports
            var stoppedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ", CultureInfo.InvariantCulture);
            WriteObject(string.Format(CultureInfo.CurrentCulture, Properties.Resources.TenantRenameStopRequested, stoppedAt));
        }
    }
}
