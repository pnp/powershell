using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPHomeSite", DefaultParameterSetName = ParameterSet_Basic)]
    public class GetHomeSite : PnPAdminCmdlet
    {
        private const string ParameterSet_Detailed = "Detailed";
        private const string ParameterSet_Basic = "Basic";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_Basic)]
        public SwitchParameter IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Detailed)]
        public SwitchParameter Detailed;

        protected override void ExecuteCmdlet()
        {
            if(Detailed.ToBool())
            {
                var results  = Tenant.GetHomeSitesDetails();
                AdminContext.ExecuteQueryRetry();
                WriteObject(results, true);
            }
            else
            {
                if (IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled)
                {
                    var results = Tenant.IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled();
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
}