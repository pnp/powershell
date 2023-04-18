using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPTenantCdnPolicy")]
    public class SetTenantCdnPolicy : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SPOTenantCdnType CdnType;

        [Parameter(Mandatory = true)]
        public SPOTenantCdnPolicyType PolicyType;

        [Parameter(Mandatory = true)]
        public string PolicyValue;

        protected override void ExecuteCmdlet()
        {
            Tenant.SetTenantCdnPolicy(CdnType, PolicyType, PolicyValue);
            AdminContext.ExecuteQueryRetry();
        }
    }
}