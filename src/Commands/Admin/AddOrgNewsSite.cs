using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Add, "PnPOrgNewsSite")]
    public class AddOrgNewsSite : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SitePipeBind OrgNewsSiteUrl;

        protected override void ExecuteCmdlet()
        {
            Tenant.SetOrgNewsSite(OrgNewsSiteUrl.Url);
            AdminContext.ExecuteQueryRetry();
        }
    }
}