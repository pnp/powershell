using System;
using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPPageCopyProgress")]
    [OutputType(typeof(SPSitePageCopyJobProgress))]
    public class GetPageCopyProgress : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public SPOSitePipeBind DestinationSite;

        [Parameter(Mandatory = true)]
        public Guid WorkItemId;

        protected override void ExecuteCmdlet()
        {
            var result = Tenant.GetSitePageCopyJobProgress(DestinationSite.Url, WorkItemId);
            AdminContext.ExecuteQueryRetry();
            WriteObject(result.Value);
        }
    }
}