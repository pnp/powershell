using System.Management.Automation;
using PnP.Core.Services;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPBatch")]
    public class InvokeBatch : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public Batch Batch;

        protected override void ExecuteCmdlet()
        {
            PnPContext.Execute(Batch);
        }
    }
}