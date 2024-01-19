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
    [Cmdlet(VerbsCommon.Get, "PnPDeletedContainer")]
    public class PnPDeletedContainer : PnPAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
        
            IList<SPDeletedContainerProperties> deletedContainers = Tenant.GetSPODeletedContainers();
            AdminContext.ExecuteQueryRetry();
            WriteObject(deletedContainers, true);
        }
    }
}
