using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPQuery")]
    [OutputType(typeof(void))]
    public class InvokeQuery : PnPSharePointCmdlet
    {
        protected override bool ShouldRefreshContextWithPendingRequest => false;

        [Parameter(Mandatory = false)]
        public int RetryCount = 10;

        protected override void ProcessRecord()
        {
            ClientContext.ExecuteQueryRetry(RetryCount);
        }
    }
}
