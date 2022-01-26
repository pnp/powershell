using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPAdaptiveScopeProperty")]
    [Alias("Add-PnPAdaptiveScopeProperty")]
    public class SetAdaptiveScopeProperty : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Key;

        [Parameter(Mandatory = true)]
        public string Value;

        protected override void ExecuteCmdlet()
        {
            bool switchBack = false;
            var siteProps = Tenant.GetSitePropertiesByUrl(SiteContext.Url, false);
            ClientContext.Load(siteProps, s => s.DenyAddAndCustomizePages);
            ClientContext.ExecuteQueryRetry();

            if (siteProps.DenyAddAndCustomizePages == Microsoft.Online.SharePoint.TenantAdministration.DenyAddAndCustomizePagesStatus.Enabled)
            {
                siteProps.DenyAddAndCustomizePages = Microsoft.Online.SharePoint.TenantAdministration.DenyAddAndCustomizePagesStatus.Disabled;
                siteProps.Update();
                ClientContext.ExecuteQueryRetry();
                switchBack = true;
            }

            SiteContext.Web.SetPropertyBagValue(Key, Value);
            SiteContext.Web.AddIndexedPropertyBagKey(Key);

            if (switchBack)
            {
                siteProps.DenyAddAndCustomizePages = Microsoft.Online.SharePoint.TenantAdministration.DenyAddAndCustomizePagesStatus.Enabled;
                siteProps.Update();
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}
