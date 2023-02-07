using Microsoft.Extensions.Logging.Abstractions;
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
            if (ParameterSpecified(nameof(IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled)))
            {
                var results  = Tenant.IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled();
                ClientContext.ExecuteQueryRetry();
                WriteObject(results.Value);
            }
            else 
            {
                var results = Tenant.GetSPHSiteUrl();
                ClientContext.ExecuteQueryRetry();
                WriteObject(results.Value);
            }
            

        }
    }
}