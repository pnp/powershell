using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsLifecycle.Register, "PnPAppCatalogSite")]
    public class RegisterAppCatalogSite : PnPAdminCmdlet
    {

        [Parameter(Mandatory = true)]
        public string Url;

        [Parameter(Mandatory = true)]
        public string Owner;

        [Parameter(Mandatory = true)]
        public int TimeZoneId;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            WriteWarning("Notice that this cmdlet can take considerate time to finish executing.");
            Tenant.EnsureAppCatalogAsync(Url, Owner, TimeZoneId, Force).GetAwaiter().GetResult();
        }
    }
}