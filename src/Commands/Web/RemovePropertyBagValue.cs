using Microsoft.SharePoint.Client;
using System.Management.Automation;

using PnP.Framework.Utilities;

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
            if (string.IsNullOrEmpty(Folder))
            {
                if (CurrentWeb.PropertyBagContainsKey(Key))
                {
                    if (Force || ShouldContinue(string.Format(Properties.Resources.Delete0, Key), Properties.Resources.Confirm))
                    {
                        CurrentWeb.RemovePropertyBagValue(Key);
                    }
                }
            }
            else
            {
                CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

                var folderUrl = UrlUtility.Combine(CurrentWeb.ServerRelativeUrl, Folder);
                var folder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(folderUrl));

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
