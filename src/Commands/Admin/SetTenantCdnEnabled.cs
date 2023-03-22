using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPTenantCdnEnabled")]
    public class SetTenantCdnEnabled : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public bool Enable;

        [Parameter(Mandatory = true)]
        public CdnType CdnType;

        [Parameter(Mandatory = false)]
        public SwitchParameter NoDefaultOrigins { get; set; }

        protected override void ExecuteCmdlet()
        {
            bool privateFlag = CdnType == CdnType.Both || CdnType == CdnType.Private;
            bool publicFlag= CdnType == CdnType.Both || CdnType == CdnType.Public;

            if (privateFlag)
            {
                Tenant.SetTenantCdnEnabled(SPOTenantCdnType.Private, Enable);
            }
            if(publicFlag)
            {
                Tenant.SetTenantCdnEnabled(SPOTenantCdnType.Public, Enable);
            }
            if (this.Enable && !this.NoDefaultOrigins)
            {
                if (privateFlag)
                {
                    Tenant.CreateTenantCdnDefaultOrigins(SPOTenantCdnType.Private);
                }
                if (publicFlag)
                {
                    Tenant.CreateTenantCdnDefaultOrigins(SPOTenantCdnType.Public);
                }
            }
            AdminContext.ExecuteQueryRetry();
        }
    }
}