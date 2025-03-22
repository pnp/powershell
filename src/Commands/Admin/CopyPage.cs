using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Copy, "PnPPage")]
    [OutputType(typeof(SPSitePageCopyJobProgress))]
    public class CopyPage : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public SPOSitePipeBind SourceSite;

        [Parameter(Mandatory = true)]
        public SPOSitePipeBind DestinationSite;

        [Parameter(Mandatory = false)]
        public string PageName;

        protected override void ExecuteCmdlet()
        {
            var result = Tenant.CopyPersonalSitePage(SourceSite.Url, DestinationSite.Url, PageName, false);
            AdminContext.ExecuteQueryRetry();
            WriteObject(result.Value);
        }
    }
}