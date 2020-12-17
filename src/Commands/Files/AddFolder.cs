using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Add, "PnPFolder")]
    public class AddFolder : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name = string.Empty;

        [Parameter(Mandatory = true)]
        public string Folder = string.Empty;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
                        
            Folder folder = CurrentWeb.GetFolderByServerRelativeUrl(UrlUtility.Combine(CurrentWeb.ServerRelativeUrl, Folder));
            ClientContext.Load(folder, f => f.ServerRelativeUrl);
            ClientContext.ExecuteQueryRetry();

            var result = folder.CreateFolder(Name);

            WriteObject(result);
        }
    }
}
