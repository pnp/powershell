using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPAdaptiveScopeProperty")]
    public class RemoveAdaptiveScopeProperty : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Key;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            bool switchBack = false;
            var siteProps = Tenant.GetSitePropertiesByUrl(SiteContext.Url, false);
            ClientContext.Load(siteProps, s => s.DenyAddAndCustomizePages);
            ClientContext.ExecuteQueryRetry();

            if (Force || ShouldContinue($"Remove {Key}?", Properties.Resources.Confirm))
            {
                if (siteProps.DenyAddAndCustomizePages == Microsoft.Online.SharePoint.TenantAdministration.DenyAddAndCustomizePagesStatus.Enabled)
                {
                    siteProps.DenyAddAndCustomizePages = Microsoft.Online.SharePoint.TenantAdministration.DenyAddAndCustomizePagesStatus.Disabled;
                    siteProps.Update();
                    ClientContext.ExecuteQueryRetry();
                    switchBack = true;
                }

                SiteContext.Web.RemovePropertyBagValue(Key);
                SiteContext.Web.RemoveIndexedPropertyBagKey(Key);

                if (switchBack)
                {
                    siteProps.DenyAddAndCustomizePages = Microsoft.Online.SharePoint.TenantAdministration.DenyAddAndCustomizePagesStatus.Enabled;
                    siteProps.Update();
                    ClientContext.ExecuteQueryRetry();
                }
            }
        }
    }
}
