using System.Management.Automation;
using Microsoft.SharePoint.Client;

using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Remove, "PnPFolder")]
    public class RemoveFolder : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name = string.Empty;

        [Parameter(Mandatory = true)]
        public string Folder = string.Empty;

        [Parameter(Mandatory = false)]
        public SwitchParameter Recycle;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            var folderUrl = UrlUtility.Combine(CurrentWeb.ServerRelativeUrl, Folder, Name);
            Folder folder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(folderUrl));
            folder.EnsureProperty(f => f.Name);

            if (Force || ShouldContinue(string.Format(Resources.Delete0, folder.Name), Resources.Confirm))
            {
                if (Recycle)
                {
                    folder.Recycle();
                }
                else
                {
                    folder.DeleteObject();
                }

                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}

