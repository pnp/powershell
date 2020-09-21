using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.Framework.Sites;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Add, "PnPTenantCdnOrigin")]
    public class AddTenantCdnOrigin : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string OriginUrl;

        [Parameter(Mandatory = true)]
        public SPOTenantCdnType CdnType;

        protected override void ExecuteCmdlet()
        {
            Tenant.AddTenantCdnOrigin(CdnType, OriginUrl);
            ClientContext.ExecuteQueryRetry();
        }
    }
}