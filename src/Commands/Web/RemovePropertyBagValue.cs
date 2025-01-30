using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "PnPPropertyBagValue")]
    [OutputType(typeof(void))]
    public class RemovePropertyBagValue : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Key;

        [Parameter(Mandatory = false)]
        public string Folder;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            bool isScriptSettingUpdated = false;
            try
            {
                WriteVerbose("Checking if the site is a no-script site");
                var web = ClientContext.Web;
                web.EnsureProperties(w => w.Url, w => w.ServerRelativeUrl);
                if (web.IsNoScriptSite())
                {
                    if (Force || ShouldContinue("The current site is a no-script site. Do you want to temporarily enable scripting on it to allow setting property bag value?", Properties.Resources.Confirm))
                    {
                        WriteVerbose("Temporarily enabling scripting on the site");
                        var tenant = new Tenant(AdminContext);
                        tenant.SetSiteProperties(web.Url, noScriptSite: false);
                        isScriptSettingUpdated = true;
                    }
                    else
                    {
                        ThrowTerminatingError(new ErrorRecord(new Exception($"Site has NoScript enabled, this prevents removing property bag values."), "NoScriptEnabled", ErrorCategory.InvalidOperation, this));
                        return;
                    }
                }

                if (string.IsNullOrEmpty(Folder))
                {
                    if (web.PropertyBagContainsKey(Key))
                    {
                        if (Force || ShouldContinue(string.Format(Properties.Resources.Delete0, Key), Properties.Resources.Confirm))
                        {
                            web.RemovePropertyBagValue(Key);
                        }
                    }
                }
                else
                {
                    web.EnsureProperty(w => w.ServerRelativeUrl);

                    var folderUrl = UrlUtility.Combine(web.ServerRelativeUrl, Folder);
                    var folder = web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(folderUrl));

                    folder.EnsureProperty(f => f.Properties);

                    if (folder.Properties.FieldValues.ContainsKey(Key))
                    {
                        if (Force || ShouldContinue(string.Format(Properties.Resources.Delete0, Key), Properties.Resources.Confirm))
                        {

                            folder.Properties[Key] = null;
                            folder.Properties.FieldValues.Remove(Key);
                            folder.Update();
                            ClientContext.ExecuteQueryRetry();
                        }
                    }
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
