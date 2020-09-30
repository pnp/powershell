using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.Framework.Sites;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsLifecycle.Register, "PnPHubSite")]
    public class RegisterHubSite : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SitePipeBind Site;

        protected override void ExecuteCmdlet()
        {
            Tenant.RegisterHubSite(Site.Url);
            ClientContext.ExecuteQueryRetry();
        }
    }
}