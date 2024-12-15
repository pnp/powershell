using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Add, "PnPFolder")]
    public class AddFolder : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name = string.Empty;

        [Parameter(Mandatory = true)]
        public FolderPipeBind Folder;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
                        
            Folder folder = Folder.GetFolder(CurrentWeb);
            var result = folder.CreateFolder(Name);

            WriteObject(result);
        }
    }
}
