using System;
using System.Management.Automation;

using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "LargeLisRemovalStatus")]
    public class GetLargeLisRemovalStatus : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public Guid ListId;

        [Parameter(Mandatory = true)]
        public Guid OperationId;

        protected override void ExecuteCmdlet()
        {
            var operation = CurrentWeb.GetListOperation(ListId, OperationId);
            Console.WriteLine($"OperationType: {operation.OperationType}, ResourceLocation: {operation.ResourceLocation}, Status: {operation.Status}, ProgressPercentage: {operation.ProgressPercentage}");
        }
    }
}