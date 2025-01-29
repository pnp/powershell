using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPContainerType")]

    public class PnPContainerType : PnPSharePointOnlineAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            IList<SPContainerTypeProperties> containerTypes = Tenant.GetSPOContainerTypes(SPContainerTypeTenantType.OwningTenant);
            AdminContext.ExecuteQueryRetry();
            WriteObject(containerTypes, true);
        }
    }
}
