using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Remove, "PnPFolder")]
    public class RemoveFolder : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name = string.Empty;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public FolderPipeBind Folder;

        [Parameter(Mandatory = false)]
        public SwitchParameter Recycle;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            var parentFolder = Folder.GetFolder(CurrentWeb);
            ClientContext.Load(parentFolder, f => f.Name, f => f.ServerRelativeUrl, f => f.ServerRelativePath);
            ClientContext.ExecuteQueryRetry();

            var folderUrl = UrlUtility.Combine(parentFolder.ServerRelativePath.DecodedUrl, Name);
            Folder folder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(folderUrl));
            folder.EnsureProperty(f => f.Name);

            if (Force || ShouldContinue(string.Format(Resources.Delete0, folder.Name), Resources.Confirm))
            {
                if (Recycle)
                {
                    var recycleResult = folder.Recycle();
                    ClientContext.ExecuteQueryRetry();
                    WriteObject(new RecycleResult { RecycleBinItemId = recycleResult.Value });
                }
                else
                {
                    folder.DeleteObject();
                    ClientContext.ExecuteQueryRetry();
                }
            }
        }
    }
}