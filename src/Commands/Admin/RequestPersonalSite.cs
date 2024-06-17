using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsLifecycle.Request, "PnPPersonalSite")]
    public class RequestPersonalSite : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNull]
        [ValidateCount(1, 200)]
        public string[] UserEmails;

        [Parameter(Mandatory = false)]
        public SwitchParameter NoWait;

        protected override void ExecuteCmdlet()
        {
            foreach (var email in UserEmails)
            {
                if (string.IsNullOrEmpty(email))
                {
                    throw new PSArgumentException("UserEmails contains an empty value");
                }
            }
            var operation = this.Tenant.RequestPersonalSites(UserEmails);
            AdminContext.Load(operation);
            AdminContext.ExecuteQueryRetry();
            if (NoWait.IsPresent)
            {
                PollOperation(operation);
            }
        }
    }
}