using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPContainer")]
    [OutputType(typeof(SPContainerProperties))]
    [OutputType(typeof(Model.SharePoint.SPConsumingTenantContainerByIdentity))]
    public class GetContainer : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull]
        public ContainerPipeBind Identity { get; set; }

        [Parameter(Mandatory = false)]
        public Guid OwningApplicationId;

        [Parameter(Mandatory = false)]
        public SwitchParameter Paged { get; set; }

        [Parameter(Mandatory = false)]
        public string PagingToken { get; set; }

        [Parameter(Mandatory = false)]
        public SortOrder? SortByStorage { get; set; }

        [Parameter(Mandatory = false)]
        public SPContainerArchiveStatusFilterProperties ArchiveStatus { get; set; } = SPContainerArchiveStatusFilterProperties.NotArchived;

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                var containerProperties = Identity.GetContainer(Tenant);
                WriteObject(containerProperties);
                return;
            }

            if (ParameterSpecified(nameof(OwningApplicationId)) && OwningApplicationId == Guid.Empty)
            {
                ThrowTerminatingError(new ErrorRecord(new PSArgumentException($"-{nameof(OwningApplicationId)} cannot be an empty GUID.", nameof(OwningApplicationId)), "EmptyOwningApplicationId", ErrorCategory.InvalidArgument, OwningApplicationId));
            }

            // Without -Paged, follows the paging token until every container has been written
            var pagingToken = PagingToken;
            var seenPagingTokens = new HashSet<string>(StringComparer.Ordinal);
            while (!Stopping)
            {
                var containers = GetContainerPage(pagingToken);
                var containerCollection = containers.ContainerCollection;
                if (containerCollection == null || containerCollection.Count == 0)
                {
                    return;
                }

                foreach (SPContainerProperties item in containerCollection)
                {
                    WriteObject(new Model.SharePoint.SPConsumingTenantContainerByIdentity(item));
                }

                if (Paged)
                {
                    WriteObject(string.IsNullOrWhiteSpace(containers.PagingToken) ? "End of containers view." : $"Retrieve remaining containers with token: {containers.PagingToken}");
                    return;
                }

                if (string.IsNullOrWhiteSpace(containers.PagingToken) || !seenPagingTokens.Add(containers.PagingToken))
                {
                    return;
                }
                pagingToken = containers.PagingToken;
            }
        }

        private SPContainerCollection GetContainerPage(string pagingToken)
        {
            ClientResult<SPContainerCollection> clientResult;
            if (ParameterSpecified(nameof(OwningApplicationId)))
            {
                clientResult = SortByStorage.HasValue
                    ? Tenant.GetSortedSPOContainersByApplicationId(OwningApplicationId, SortByStorage == SortOrder.Ascending, true, pagingToken, ArchiveStatus)
                    : Tenant.GetSPOContainersByApplicationId(OwningApplicationId, true, pagingToken, ArchiveStatus);
            }
            else
            {
                clientResult = Tenant.GetAllSPOContainersFromAdminList(new SPOContainerQueryParams
                {
                    FilterByColumnsList =
                    [
                        new()
                        {
                            FilteringField = SPContainerFilterProperties.ArchiveStatus,
                            ArchiveStatus = ArchiveStatus
                        }
                    ],
                    OrderByColumnsList =
                    [
                        new()
                        {
                            SortingField = SortByStorage.HasValue ? SPContainerSortProperties.StorageUsed : SPContainerSortProperties.CreationDateTime,
                            Ascending = SortByStorage == SortOrder.Ascending
                        }
                    ],
                    PagingToken = pagingToken
                });
            }
            AdminContext.ExecuteQueryRetry();
            return clientResult.Value;
        }
    }
}
