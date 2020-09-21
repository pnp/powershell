using Microsoft.SharePoint.Client;
using System.Management.Automation;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "PnPPropertyBagValue", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
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
            if (string.IsNullOrEmpty(Folder))
            {
                if (SelectedWeb.PropertyBagContainsKey(Key))
                {
                    if (Force || ShouldContinue(string.Format(Properties.Resources.Delete0, Key), Properties.Resources.Confirm))
                    {
                        SelectedWeb.RemovePropertyBagValue(Key);
                    }
                }
            }
            else
            {
                SelectedWeb.EnsureProperty(w => w.ServerRelativeUrl);

                var folderUrl = UrlUtility.Combine(SelectedWeb.ServerRelativeUrl, Folder);
                var folder = SelectedWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(folderUrl));

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
    }
}
