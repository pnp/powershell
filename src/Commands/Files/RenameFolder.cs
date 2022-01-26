using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Rename, "PnPFolder")]
    public class RenameFolder : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public FolderPipeBind Folder;

        [Parameter(Mandatory = true)]
        public string TargetFolderName = string.Empty;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            var sourceFolder = Folder.GetFolder(CurrentWeb);
            sourceFolder.EnsureProperties(f => f.ServerRelativePath, f => f.Name);            

            var targetPath = string.Concat(sourceFolder.ServerRelativePath.DecodedUrl.Remove(sourceFolder.ServerRelativePath.DecodedUrl.Length - sourceFolder.Name.Length), TargetFolderName);
            sourceFolder.MoveToUsingPath(ResourcePath.FromDecodedUrl(targetPath));
            ClientContext.ExecuteQueryRetry();
        }
    }
}