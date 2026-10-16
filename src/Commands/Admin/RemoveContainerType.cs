using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPContainerType", SupportsShouldProcess = true)]
    public class RemoveContainerType : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public Guid Identity { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (!ShouldProcess(Identity.ToString(), "Remove container type"))
            {
                return;
            }

            SPDeletedContainerTypeProperties sPDeletedContainerTypeProperties = new SPDeletedContainerTypeProperties();
            sPDeletedContainerTypeProperties.ContainerTypeId = Identity;
            Tenant.RemoveSPOContainerType(sPDeletedContainerTypeProperties);
            AdminContext.ExecuteQueryRetry();
        }
    }
}
