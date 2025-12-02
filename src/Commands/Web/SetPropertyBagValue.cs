using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPPropertyBagValue")]
    [Alias("Add-PnPPropertyBagValue")]
    [OutputType(typeof(void))]
    public class SetPropertyBagValue : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Web")]
        [Parameter(Mandatory = true, ParameterSetName = "Folder")]
        public string Key;

        [Parameter(Mandatory = true, ParameterSetName = "Web")]
        [Parameter(Mandatory = true, ParameterSetName = "Folder")]
        [Parameter(Mandatory = true)]
        public string Value;

        [Parameter(Mandatory = true, ParameterSetName = "Web")]
        public SwitchParameter Indexed;

        [Parameter(Mandatory = false, ParameterSetName = "Folder")]
        public string Folder;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            try
            {
                SetPropertyBagValueInternal();
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

                                    SetPropertyBagValueInternal();
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
                                SetPropertyBagValueInternal();
                            }
                        }
                        else
                        {
                            SetPropertyBagValueInternal();
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

        private void SetPropertyBagValueInternal()
        {
            var web = ClientContext.Web;
            web.EnsureProperties(w => w.Url, w => w.ServerRelativeUrl);
            if (!ParameterSpecified(nameof(Folder)))
            {
                if (!Indexed)
                {
                    // If it is already an indexed property we still have to add it back to the indexed properties
                    Indexed = !string.IsNullOrEmpty(web.GetIndexedPropertyBagKeys().FirstOrDefault(k => k == Key));
                }

                web.SetPropertyBagValue(Key, Value);
                if (Indexed)
                {
                    web.AddIndexedPropertyBagKey(Key);
                }
                else
                {
                    web.RemoveIndexedPropertyBagKey(Key);
                }
            }
            else
            {
                var folderUrl = UrlUtility.Combine(web.ServerRelativeUrl, Folder);
                var folder = web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(folderUrl));

                folder.EnsureProperty(f => f.Properties);

                folder.Properties[Key] = Value;
                folder.Update();
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}
