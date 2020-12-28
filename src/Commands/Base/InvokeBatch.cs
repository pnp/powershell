using System.Management.Automation;
using PnP.Core.Services;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPBatch")]
    public class InvokeBatch : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public Batch Batch;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;


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
                PnPContext.Execute(Batch);
            }
        }
    }
}