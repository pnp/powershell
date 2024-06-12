using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPContainer")]
    [OutputType(typeof(SPContainerProperties))]
    public class GetContainer : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public ContainerPipeBind Identity { get; set; }

        [Parameter(Mandatory = false)]
        public Guid OwningApplicationId;

        [Parameter(Mandatory = false)]
        public SwitchParameter Paged { get; set; }

        [Parameter(Mandatory = false)]
        public string PagingToken { get; set; }

        [Parameter(Mandatory = false)]
        public SortOrder? SortByStorage { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                var containerProperties = Identity.GetContainer(Tenant);            
                WriteObject(containerProperties);
            }
            else if (OwningApplicationId != Guid.Empty)
            {
                ClientResult<SPContainerCollection> clientResult;
                if (SortByStorage.HasValue)
                {
                    bool ascending = SortByStorage == SortOrder.Ascending;
                    clientResult = Tenant.GetSortedSPOContainersByApplicationId(OwningApplicationId, ascending, Paged, PagingToken);
                }
                else
                {
                    clientResult = Tenant.GetSPOContainersByApplicationId(OwningApplicationId, Paged, PagingToken);
                }
                AdminContext.ExecuteQueryRetry();
                IList<SPContainerProperties> containerCollection = clientResult.Value.ContainerCollection;
                if (containerCollection != null && containerCollection.Count > 0)
                {
                    foreach (SPContainerProperties item in containerCollection)
                    {
                        WriteObject(new Model.SharePoint.SPConsumingTenantContainerByIdentity(item));
                    }
                    if (Paged)
                    {
                        if (!string.IsNullOrWhiteSpace(clientResult.Value.PagingToken))
                        {
                            WriteObject($"Retrieve remaining containers with token: {clientResult.Value.PagingToken}");
                        }
                        else
                        {
                            WriteObject("End of containers view.");
                        }
                    }                    
                }                
            }
            else
            {
                throw new PSArgumentException($"Please specify the parameter {nameof(OwningApplicationId)} or {nameof(Identity)} when invoking this cmdlet");
            }
        }
    }
}