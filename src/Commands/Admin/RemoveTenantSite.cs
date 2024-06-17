using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using System;
using PnP.Framework;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "PnPTenantSite")]
    public class RemoveSite : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Url;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipRecycleBin;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public SwitchParameter FromRecycleBin;

        protected override void ExecuteCmdlet()
        {
            if (!Url.ToLower().StartsWith("https://") && !Url.ToLower().StartsWith("http://"))
            {
                Uri uri = BaseUri;
                Url = $"{uri.ToString().TrimEnd('/')}/{Url.TrimStart('/')}";
            }

            bool dodelete = true;
            // Check if not deleting the root web
            var siteUri = new Uri(Url);
            if ($"{siteUri.Scheme}://{siteUri.Host}".Equals(Url, StringComparison.OrdinalIgnoreCase) && !Force)
            {
                dodelete = false;
                dodelete = ShouldContinue("You are trying to delete the root site collection. Be aware that you need to contact Office 365 Support in order to create a new root site collection. Also notice that some CSOM and REST operations require the root site collection to be present. Removing this site can affect all your remote processing code, even when accessing non-root site collections.", Resources.Confirm);
            }

            if (dodelete && (Force || ShouldContinue(string.Format(Resources.RemoveSiteCollection0, Url), Resources.Confirm)))
            {

                Func<TenantOperationMessage, bool> timeoutFunction = TimeoutFunction;

                if (!FromRecycleBin)
                {
                    Tenant.DeleteSiteCollection(Url, !ParameterSpecified(nameof(SkipRecycleBin)), timeoutFunction);
                }
                else
                {
                    Tenant.DeleteSiteCollectionFromRecycleBin(Url, true, timeoutFunction);
                }
            }
        }

        private bool TimeoutFunction(TenantOperationMessage message)
        {
            switch (message)
            {
                case TenantOperationMessage.DeletingSiteCollection:
                case TenantOperationMessage.RemovingDeletedSiteCollectionFromRecycleBin:
                    Host.UI.Write(".");
                    break;
            }
            return Stopping;
        }
    }
}