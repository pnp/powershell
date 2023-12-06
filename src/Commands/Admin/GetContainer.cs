using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPContainer")]
    [OutputType(typeof(SPContainerProperties))]
    public class GetContainer : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, Position=0, ValueFromPipeline=true)]
        public Guid OwningApplicationId;

        [Parameter(Mandatory = false)]
        public ContainerPipeBind Identity { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Paged { get; set; }

        [Parameter(Mandatory = false)]
        public string PagingToken { get; set; }
        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                var containerProperties = Identity.GetContainer(Tenant);            
                WriteObject(containerProperties);
            }
            else if (OwningApplicationId != Guid.Empty)
            {
                var containersProperties = Tenant.GetSPOContainersByApplicationId(OwningApplicationId, Paged, PagingToken);
                AdminContext.ExecuteQueryRetry();
                WriteObject(containersProperties.Value.ContainerCollection);
            }
            else
            {
                throw new PSArgumentException($"Please specify the parameter OwningApplicationId or Identity when invoking this cmdlet");
            }
        }
    }
}