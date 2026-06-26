using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "PnPPropertyBagValue")]
    [OutputType(typeof(void))]
    public class RemovePropertyBagValue : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Key;

        [Parameter(Mandatory = false)]
        public string Folder;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            try
            {
                if (Force || ShouldContinue(string.Format(Properties.Resources.Delete0, Key), Properties.Resources.Confirm))
                {
                    RemovePropertyBagValueInternal();
                }
            }
            catch (Exception ex)
            {
                if (ex is ServerUnauthorizedAccessException)
                {
                    if (Force || ShouldContinue("This is a no-script site. You need SharePoint admin permissions to allow scripts to set property bag values. Do you want to enable scripting on it temporarily?", Properties.Resources.Confirm))
                    {
                        var tenantUrl = Connection.TenantAdminUrl ?? UrlUtilities.GetTenantAdministrationUrl(ClientContext.Url);
                        using var tenantContext = ClientContext.Clone(tenantUrl);

                        LogDebug("Checking if AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled is set to true at the tenant level");
                        var tenant = new Tenant(tenantContext);
                        tenantContext.Load(tenant);
                        tenantContext.Load(tenant, t => t.AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled);
                        tenantContext.ExecuteQueryRetry();

                        if (!tenant.AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled)
                        {
                            LogDebug("Temporarily enabling scripting on the site");
                            bool isScriptSettingUpdated = false;
                            var site = ClientContext.Site;
                            if (site.IsNoScriptSite())
                            {
                                try
                                {
                                    tenant.SetSiteProperties(ClientContext.Url, noScriptSite: false);
                                    isScriptSettingUpdated = true;

                                    RemovePropertyBagValueInternal();
                                }
                                catch (Exception innerEx)
                                {
                                    LogError(innerEx);
                                    return;
                                }
                                finally
                                {
                                    if (isScriptSettingUpdated)
                                    {
                                        LogDebug("Reverting scripting setting on the site back to no-script");
                                        tenant.SetSiteProperties(ClientContext.Url, noScriptSite: true);
                                    }
                                }
                            }
                            else
                            {
                                RemovePropertyBagValueInternal();
                            }
                        }
                        else
                        {
                            RemovePropertyBagValueInternal();
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
        }

        private void RemovePropertyBagValueInternal()
        {
            var web = ClientContext.Web;
            web.EnsureProperties(w => w.Url, w => w.ServerRelativeUrl);
            if (string.IsNullOrEmpty(Folder))
            {
                if (web.PropertyBagContainsKey(Key))
                {
                    web.RemovePropertyBagValue(Key);
                }
            }
            else
            {
                var folderUrl = UrlUtility.Combine(web.ServerRelativeUrl, Folder);
                var folder = web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(folderUrl));

                folder.EnsureProperty(f => f.Properties);

                if (folder.Properties.FieldValues.ContainsKey(Key))
                {
                    folder.Properties[Key] = null;
                    folder.Properties.FieldValues.Remove(Key);
                    folder.Update();
                    ClientContext.ExecuteQueryRetry();
                }
            }
        }
    }
}
