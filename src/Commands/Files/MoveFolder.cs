using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Move, "PnPFolder")]
    
    public class MoveFolder : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public FolderPipeBind Folder;

        [Parameter(Mandatory = true)]
        public string TargetFolder = string.Empty;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            var sourceFolder = Folder.GetFolder(CurrentWeb);            
            ClientContext.Load(sourceFolder, f => f.Name, f => f.ServerRelativeUrl, f => f.ServerRelativePath);
            ClientContext.ExecuteQueryRetry();

            var targetPath = string.Concat(TargetFolder, "/", sourceFolder.Name);
            sourceFolder.MoveToUsingPath(ResourcePath.FromDecodedUrl(targetPath));
            ClientContext.ExecuteQueryRetry();

            var folder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(targetPath));
            ClientContext.Load(folder, f => f.Name, f => f.ItemCount, f => f.TimeLastModified, f => f.ListItemAllFields);
            ClientContext.ExecuteQueryRetry();
            WriteObject(folder);
        }
    }
}