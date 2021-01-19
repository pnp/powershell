using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.Framework.Sites;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPTenantCdnOrigin")]
    public class RemoveTenantCdnOrigin : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string OriginUrl;

        [Parameter(Mandatory = true)]
        public SPOTenantCdnType CdnType = SPOTenantCdnType.Public;

        protected override void ExecuteCmdlet()
        {
            Tenant.RemoveTenantCdnOrigin(CdnType, OriginUrl);
            ClientContext.ExecuteQueryRetry();
        }
    }
}