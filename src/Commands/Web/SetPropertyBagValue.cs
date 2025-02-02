using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPPropertyBagValue")]
    [Alias("Add-PnPPropertyBagValue")]
    [OutputType(typeof(void))]
    public class SetPropertyBagValue : PnPSharePointOnlineAdminCmdlet
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
            bool isScriptSettingUpdated = false;
            try
            {
                WriteVerbose("Checking if AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled is set to true at the tenant level");
                var tenant = new Tenant(AdminContext);
                AdminContext.Load(tenant);
                AdminContext.Load(tenant, t => t.AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled);
                AdminContext.ExecuteQueryRetry();
               
                var web = ClientContext.Web;
                if (!tenant.AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled)
                {
                    WriteVerbose("Checking if the site is a no-script site");
                    
                    web.EnsureProperties(w => w.Url, w => w.ServerRelativeUrl);

                    if (web.IsNoScriptSite())
                    {
                        if (Force || ShouldContinue("The current site is a no-script site. Do you want to temporarily enable scripting on it to allow setting property bag value?", Properties.Resources.Confirm))
                        {
                            WriteVerbose("Temporarily enabling scripting on the site");
                            tenant.SetSiteProperties(web.Url, noScriptSite: false);
                            isScriptSettingUpdated = true;
                        }
                        else
                        {
                            ThrowTerminatingError(new ErrorRecord(new Exception($"Site has NoScript enabled, this prevents setting some property bag values."), "NoScriptEnabled", ErrorCategory.InvalidOperation, this));
                            return;
                        }
                    }
                }
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
            catch
            {
                throw;
            }
            finally
            {
                if (isScriptSettingUpdated)
                {
                    WriteVerbose("Disabling scripting on the site");
                    var tenant = new Tenant(AdminContext);
                    tenant.SetSiteProperties(ClientContext.Web.Url, noScriptSite: true);
                }
            }
        }
    }
}
