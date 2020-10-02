using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Move, "Folder")]
    
    public class MoveFolder : PnPWebCmdlet
    {

        [Parameter(Mandatory = true)]
        public string Folder = string.Empty;

        [Parameter(Mandatory = true)]
        public string TargetFolder = string.Empty;

        protected override void ExecuteCmdlet()
        {
            SelectedWeb.EnsureProperty(w => w.ServerRelativeUrl);

            var sourceFolderUrl = UrlUtility.Combine(SelectedWeb.ServerRelativeUrl, Folder);
            Folder sourceFolder = SelectedWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(sourceFolderUrl));
            ClientContext.Load(sourceFolder, f => f.Name, f => f.ServerRelativeUrl);
            ClientContext.ExecuteQueryRetry();

            var targetPath = string.Concat(TargetFolder, "/", sourceFolder.Name);
            sourceFolder.MoveToUsingPath(ResourcePath.FromDecodedUrl(targetPath));
            ClientContext.ExecuteQueryRetry();

            var folder = SelectedWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(targetPath));
            ClientContext.Load(folder, f => f.Name, f => f.ItemCount, f => f.TimeLastModified, f => f.ListItemAllFields);
            ClientContext.ExecuteQueryRetry();
            WriteObject(folder);
        }
    }
}
