using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPContainer")]    
    public class RemoveContainer : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public ContainerPipeBind Identity { get; set; }

        protected override void ExecuteCmdlet()
        {
            var containerProperties = Identity.GetContainer(Tenant); 
            Tenant.RemoveSPOContainerByContainerId(containerProperties.ContainerId);
            AdminContext.ExecuteQueryRetry();
        }
    }
}