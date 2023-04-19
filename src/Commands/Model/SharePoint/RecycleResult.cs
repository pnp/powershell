using System;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    public sealed class RecycleResult
    {
        public Guid RecycleBinItemId { get; set; }
    }

    public sealed class RecycleBinLargeOperation
    {
        public Guid ListId { get; set; }
        public Guid RecycleBinLargeOperationId { get; set; }
    }

    public sealed class RecycleBinLargeOperationResult
    {
        public string RecycleBinLargeOperationType { get; set; }
        public string RecycleBinLargeOperationResourceLocation { get; set; }
        public string RecycleBinLargeOperationStatus { get; set; }
        public double RecycleBinLargeOperationProgressPercentage { get; set; }
    }
}
