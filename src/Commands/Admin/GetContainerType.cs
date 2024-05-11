using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPContainerType")]

    public class PnPContainerType : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            IList<SPContainerTypeProperties> containerTypes = Tenant.GetSPOContainerTypes(SPContainerTypeTenantType.OwningTenant);
            AdminContext.ExecuteQueryRetry();
            WriteObject(containerTypes, true);
        }
    }
}
