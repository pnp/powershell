using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Administration;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPBuiltInDesignPackageVisibility")]
    public class SetBuiltInDesignPackageVisibility : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public bool IsVisible;

        [Parameter(Mandatory = true)]
        public DesignPackageType DesignPackage;
        protected override void ExecuteCmdlet()
        {
            if (DesignPackage == DesignPackageType.None)
            {
                throw new PSArgumentException(nameof(DesignPackage));
            }
            Microsoft.Online.SharePoint.TenantAdministration.Tenant.SetBuiltInDesignPackageVisibility(AdminContext, DesignPackage, IsVisible);
            AdminContext.ExecuteQueryRetry();
        }
    }
}