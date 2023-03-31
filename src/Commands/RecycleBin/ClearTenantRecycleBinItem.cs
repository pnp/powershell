using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework;
using PnP.PowerShell.Commands.Base;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.RecycleBin
{
    [Cmdlet(VerbsCommon.Clear, "PnPTenantRecycleBinItem")]
    [OutputType(typeof(void))]
    public class ClearTenantRecycleBinItem : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = false)]
        public string Url;

        [Parameter(Mandatory = false, ValueFromPipeline = false)]
        public SwitchParameter Wait = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue(string.Format(Resources.ClearTenantRecycleBinItem, Url), Resources.Confirm))
            {
                Func<TenantOperationMessage, bool> timeoutFunction = TimeoutFunction;

                Tenant.DeleteSiteCollectionFromRecycleBin(Url, Wait, Wait ? timeoutFunction : null);
            }
        }

        private bool TimeoutFunction(TenantOperationMessage message)
        {
            if (message == TenantOperationMessage.RemovingDeletedSiteCollectionFromRecycleBin)
            {
                this.Host.UI.Write(".");
            }
            return Stopping;
        }
    }
}