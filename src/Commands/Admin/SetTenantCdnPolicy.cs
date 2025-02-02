using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPTenantCdnPolicy")]
    public class SetTenantCdnPolicy : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public SPOTenantCdnType CdnType;

        [Parameter(Mandatory = true)]
        public SPOTenantCdnPolicyType PolicyType;

        [AllowEmptyString]
        [AllowNull]
        [Parameter(Mandatory = true)]
        public string PolicyValue;

        protected override void ExecuteCmdlet()
        {
            // A null PolicyValue throws an exception Microsoft.SharePoint.Client.UnknownError
            if (PolicyValue == null)
                PolicyValue = string.Empty;

            Tenant.SetTenantCdnPolicy(CdnType, PolicyType, PolicyValue);
            AdminContext.ExecuteQueryRetry();
        }
    }
}