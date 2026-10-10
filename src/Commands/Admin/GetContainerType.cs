using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPContainerType")]
    [OutputType(typeof(SPContainerTypeProperties))]
    public class PnPContainerType : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        [Alias("ContainerTypeId")]
        public Guid Identity { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var containerType = Tenant.GetSPOContainerTypeById(Identity, SPContainerTypeTenantType.OwningTenant);
                AdminContext.ExecuteQueryRetry();
                if (containerType.Value == null)
                {
                    ThrowTerminatingError(new ErrorRecord(new PSArgumentException(string.Format(Properties.Resources.ContainerTypeNotFound, Identity)), "ContainerTypeNotFound", ErrorCategory.ObjectNotFound, Identity));
                }
                WriteObject(containerType.Value);
                return;
            }

            IList<SPContainerTypeProperties> containerTypes = Tenant.GetSPOContainerTypes(SPContainerTypeTenantType.OwningTenant);
            AdminContext.ExecuteQueryRetry();
            WriteObject(containerTypes, true);
        }
    }
}
