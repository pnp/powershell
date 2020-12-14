using System.Management.Automation;
using Microsoft.SharePoint.Client;


namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Invoke,"PnPQuery")]
    public class InvokeQuery : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false)]
        public int RetryCount = 10;

        [Parameter(Mandatory = false)]
        public int RetryWait = 1;

        protected override void ProcessRecord()
        {
            if(MyInvocation.InvocationName.ToLower() == "execute-pnpquery")
            {
                WriteWarning("Execute-PnPQuery has been deprecated. Use Invoke-PnPQuery instead.");
            }
            ClientContext.ExecuteQueryRetry(RetryCount, RetryWait);
        }
    }
}
