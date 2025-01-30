using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsData.Restore, "PnPTenantSite")]
    public class RestoreTenantSite : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        [Alias("Url")]
        public SPOSitePipeBind Identity { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter NoWait;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Identity == null || string.IsNullOrEmpty(Identity.Url))
            {
                throw new ArgumentNullException($"{nameof(Identity)} must be provided");
            }
            else
            {
                if (Force || ShouldContinue($"Restore site collection {Identity.Url}?", Properties.Resources.Confirm))
                {
                    WriteVerbose($"Restoring site collection {Identity.Url}");

                    SpoOperation spoOperation = Tenant.RestoreDeletedSite(Identity.Url);
                    AdminContext.Load(spoOperation);
                    AdminContext.ExecuteQueryRetry();
                    if (!NoWait.ToBool())
                    {
                        PollOperation(spoOperation);
                    }
                }
            }
        }
    }
}