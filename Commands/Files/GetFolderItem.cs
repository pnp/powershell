using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;
using File = Microsoft.SharePoint.Client.File;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFolderItem")]
    
    
    
    
    
    
    public class GetFolderItem : PnPWebCmdlet
    {
        private const string ParameterSet_FOLDERSBYPIPE = "Folder via pipebind";
        private const string ParameterSet_FOLDERBYURL = "Folder via url";

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_FOLDERBYURL)]
        public string FolderSiteRelativeUrl;

        [Parameter(Mandatory = false, Position = 0, ParameterSetName = ParameterSet_FOLDERSBYPIPE)]
        public FolderPipeBind Identity;

        [Parameter(Mandatory = false)]
        [ValidateSet("Folder", "File", "All")]
        public string ItemType = "All";

        [Parameter(Mandatory = false)]
        public string ItemName = string.Empty;

        [Parameter(Mandatory = false, Position = 4)]
        public SwitchParameter Recursive;

        private IEnumerable<object> GetContents(string FolderSiteRelativeUrl)
        {
            Folder targetFolder = null;
            if (ParameterSetName == ParameterSet_FOLDERSBYPIPE && Identity != null)
            {
                targetFolder = Identity.GetFolder(SelectedWeb);
            }
            else
            {
                string serverRelativeUrl = null;
                if (!string.IsNullOrEmpty(FolderSiteRelativeUrl))
                {
                    var webUrl = SelectedWeb.EnsureProperty(w => w.ServerRelativeUrl);
                    serverRelativeUrl = UrlUtility.Combine(webUrl, FolderSiteRelativeUrl);
                }

                targetFolder = (string.IsNullOrEmpty(FolderSiteRelativeUrl)) ? SelectedWeb.RootFolder : SelectedWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));
            }

            IEnumerable<File> files = null;
            IEnumerable<Folder> folders = null;

            if (ItemType == "File" || ItemType == "All")
            {
                files = ClientContext.LoadQuery(targetFolder.Files).OrderBy(f => f.Name);
                if (!string.IsNullOrEmpty(ItemName))
                {
                    files = files.Where(f => f.Name.Equals(ItemName, StringComparison.InvariantCultureIgnoreCase));
                }
            }
            if (ItemType == "Folder" || ItemType == "All" || Recursive)
            {
                folders = ClientContext.LoadQuery(targetFolder.Folders).OrderBy(f => f.Name);
                if (!string.IsNullOrEmpty(ItemName))
                {
                    folders = folders.Where(f => f.Name.Equals(ItemName, StringComparison.InvariantCultureIgnoreCase));
                }
            }
            ClientContext.ExecuteQueryRetry();
            
            IEnumerable<object> folderContent = null;
            switch (ItemType)
            {
                case "All":
                    folderContent = folders.Concat<object>(files);                    
                    break;
                case "Folder":
                    folderContent = folders;
                    break;
                default:
                    folderContent = files;
                    break;
            }
            
            if(Recursive && folders.Count() > 0)
            {
                foreach(var folder in folders)
                {
                    var relativeUrl = folder.ServerRelativeUrl.Replace(SelectedWeb.ServerRelativeUrl, "");
                    var subFolderContents = GetContents(relativeUrl);
                    folderContent = folderContent.Concat<object>(subFolderContents);
                }                
            }

            return folderContent;
        }

        protected override void ExecuteCmdlet()
        {
            var contents = GetContents(FolderSiteRelativeUrl);
            WriteObject(contents, true);
        }
    }
}
