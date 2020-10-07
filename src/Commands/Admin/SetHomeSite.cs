using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "HomeSite")]
    public class SetHomeSite : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Url;

        protected override void ExecuteCmdlet()
        {
            Tenant.SetSPHSite(Url);
            ClientContext.ExecuteQueryRetry();
        }
    }
}