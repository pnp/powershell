using Microsoft.SharePoint.Client;
using PnP.Framework;
using PnP.Framework.Entities;

using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.New, "PnPTenantSite")]
    public class NewTenantSite : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = true)]
        public string Url;

        [Parameter(Mandatory = true)]
        public string Owner = string.Empty;

        [Parameter(Mandatory = false)]
        public uint Lcid = 1033;

        [Parameter(Mandatory = false)]
        public string Template = "STS#0";

        [Parameter(Mandatory = true)]
        public int TimeZone;

        [Parameter(Mandatory = false)]
        public double ResourceQuota = 0;

        [Parameter(Mandatory = false)]
        public double ResourceQuotaWarningLevel = 0;

        [Parameter(Mandatory = false)]
        public long StorageQuota = 100;

        [Parameter(Mandatory = false)]
        public long StorageQuotaWarningLevel = 100;

        [Parameter(Mandatory = false)]
        public SwitchParameter RemoveDeletedSite;
     
        [Parameter(Mandatory = false)]
        public SwitchParameter Wait;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            bool shouldContinue = true;
            if (!Url.ToLower().StartsWith("https://") && !Url.ToLower().StartsWith("http://"))
            {
                Uri uri = BaseUri;
                Url = $"{uri.ToString().TrimEnd('/')}/{Url.TrimStart('/')}";
                if (!Force)
                {
                    shouldContinue = ShouldContinue(string.Format(Resources.CreateSiteWithUrl0, Url), Resources.Confirm);
                }
            }
            if (shouldContinue)
            {
                Func<TenantOperationMessage, bool> timeoutFunction = TimeoutFunction;

                Tenant.CreateSiteCollection(Url, Title, Owner, Template, (int)StorageQuota,
                    (int)StorageQuotaWarningLevel, TimeZone, (int)ResourceQuota, (int)ResourceQuotaWarningLevel, Lcid,
                    RemoveDeletedSite, Wait, Wait == true ? timeoutFunction : null);
            }
        }

        private bool TimeoutFunction(TenantOperationMessage message)
        {
            if (message == TenantOperationMessage.CreatingSiteCollection)
            {
                Host.UI.Write(".");
            }
            return Stopping;
        }
    }
}
