using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPAdaptiveScopeProperty")]
    public class RemoveAdaptiveScopeProperty : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Key;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            bool switchBack = false;
            var siteProps = Tenant.GetSitePropertiesByUrl(ClientContext.Url, false);
            AdminContext.Load(siteProps, s => s.DenyAddAndCustomizePages);
            AdminContext.ExecuteQueryRetry();

            if (Force || ShouldContinue($"Remove {Key}?", Properties.Resources.Confirm))
            {
                if (siteProps.DenyAddAndCustomizePages == Microsoft.Online.SharePoint.TenantAdministration.DenyAddAndCustomizePagesStatus.Enabled)
                {
                    siteProps.DenyAddAndCustomizePages = Microsoft.Online.SharePoint.TenantAdministration.DenyAddAndCustomizePagesStatus.Disabled;
                    siteProps.Update();
                    AdminContext.ExecuteQueryRetry();
                    switchBack = true;
                }

                ClientContext.Web.RemovePropertyBagValue(Key);
                ClientContext.Web.RemoveIndexedPropertyBagKey(Key);

                if (switchBack)
                {
                    siteProps.DenyAddAndCustomizePages = Microsoft.Online.SharePoint.TenantAdministration.DenyAddAndCustomizePagesStatus.Enabled;
                    siteProps.Update();
                    AdminContext.ExecuteQueryRetry();
                }
            }
        }
    }
}
