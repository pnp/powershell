using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPHomeSite")]
    public class GetHomeSite : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled;
        
        protected override void ExecuteCmdlet()
        {
            if (IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled)
            {
                var results  = Tenant.IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled();
                AdminContext.ExecuteQueryRetry();
                WriteObject(results.Value);
            }
            else 
            {
                var results = Tenant.GetSPHSiteUrl();
                AdminContext.ExecuteQueryRetry();
                WriteObject(results.Value);
            }
        }
    }
}