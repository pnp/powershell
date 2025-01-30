using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPOrgNewsSite")]    
    public class RemoveOrgNewsSite : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SitePipeBind OrgNewsSiteUrl;

        protected override void ExecuteCmdlet()
        {
            Tenant.RemoveOrgNewsSite(OrgNewsSiteUrl.Url);
            AdminContext.ExecuteQueryRetry();
        }
    }
}