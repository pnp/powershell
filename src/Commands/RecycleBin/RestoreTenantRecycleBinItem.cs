using System.Management.Automation;
using System.Threading;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.RecycleBin
{
    [Cmdlet(VerbsData.Restore, "PnPTenantRecycleBinItem")]
    [OutputType(typeof(void))]
    public class RestoreTenantRecycleBinItem : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = false)]
        public string Url;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public SwitchParameter Wait = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue(string.Format(Resources.ResetTenantRecycleBinItem, Url), Resources.Confirm))
            {
                var spOperation = Tenant.RestoreDeletedSite(Url);
                Tenant.Context.Load(spOperation, spo => spo.PollingInterval, spo => spo.IsComplete);
                Tenant.Context.ExecuteQueryRetry();

                if (Wait)
                {
                    while (!spOperation.IsComplete)
                    {
                        Thread.Sleep(spOperation.PollingInterval);
                        Tenant.Context.Load(spOperation, spo => spo.PollingInterval, spo => spo.IsComplete);
                        Tenant.Context.ExecuteQueryRetry();
                        Host.UI.Write(".");
                        if (Stopping)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}