using System.Collections.Generic;
using System.Management.Automation;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPBatch")]
    public class InvokeBatch : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public PnPBatch Batch;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public SwitchParameter Details;

        protected override void ExecuteCmdlet()
        {
            bool batchExecuted = Batch.Executed;
            if (batchExecuted)
            {
                if (Force || ShouldContinue($"Batch has been invoked before with {Batch.Requests.Count} requests. Invoke again?", "Invoke Batch"))
                {
                    batchExecuted = false;
                }
            }
            if (!batchExecuted)
            {
                Batch.Execute();
                if (Details)
                {
                    var requests = new List<Model.BatchRequest>();
                    foreach (var request in Batch.Requests)
                    {
                        requests.Add(new Model.BatchRequest() { HttpStatusCode = request.Value.ResponseHttpStatusCode, ResponseJson = request.Value.ResponseJson });
                    }
                    WriteObject(requests);
                }
            }
        }
    }
}