using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Rename, "PnPFolder")]
    public class RenameFolder : PnPWebCmdlet
    {

        [Parameter(Mandatory = true)]
        public string Folder = string.Empty;

        [Parameter(Mandatory = true)]
        public string TargetFolderName = string.Empty;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            var sourceFolderUrl = UrlUtility.Combine(CurrentWeb.ServerRelativeUrl, Folder);
            Folder sourceFolder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(sourceFolderUrl));
            ClientContext.Load(sourceFolder, f => f.Name, f => f.ServerRelativePath);
            ClientContext.ExecuteQueryRetry();

            var targetPath = string.Concat(sourceFolder.ServerRelativePath.DecodedUrl.Remove(sourceFolder.ServerRelativePath.DecodedUrl.Length - sourceFolder.Name.Length), TargetFolderName);
            sourceFolder.MoveToUsingPath(ResourcePath.FromDecodedUrl(targetPath));
            ClientContext.ExecuteQueryRetry();
        }
    }
}