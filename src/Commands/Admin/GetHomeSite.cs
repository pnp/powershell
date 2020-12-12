using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPHomeSite")]
    public class GetHomeSite : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var results = Tenant.GetSPHSiteUrl();
            ClientContext.ExecuteQueryRetry();
            WriteObject(results.Value);
        }
    }
}