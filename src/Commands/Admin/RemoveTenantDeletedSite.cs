using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPTenantDeletedSite")]
    public class RemoveTenantDeletedSite : PnPAdminCmdlet
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
                if (Force || ShouldContinue($"Remove site collection {Identity.Url}?", "Confirm"))
                {
                    SpoOperation spoOperation = Tenant.RemoveDeletedSite(Identity.Url);
                    ClientContext.Load(spoOperation);
                    ClientContext.ExecuteQueryRetry();
                    if (!NoWait.ToBool())
                    {
                        PollOperation(spoOperation);
                    }
                }
            }
        }
    }
}
