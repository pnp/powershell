using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPLargeListOperationStatus")]
    public class GetLargeListOperationStatus : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public Guid Identity;

        [Parameter(Mandatory = true)]
        public Guid OperationId;

        protected override void ExecuteCmdlet()
        {
            var operation = CurrentWeb.GetListOperation(Identity, OperationId);
            ClientContext.Load(operation);
            ClientContext.ExecuteQueryRetry();
            WriteObject(new RecycleBinLargeOperationResult { RecycleBinLargeOperationType = operation.OperationType, RecycleBinLargeOperationResourceLocation = operation.ResourceLocation, RecycleBinLargeOperationStatus = operation.Status, RecycleBinLargeOperationProgressPercentage = operation.ProgressPercentage });
        }
    }
}