using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;
using File = Microsoft.SharePoint.Client.File;
using Folder = Microsoft.SharePoint.Client.Folder;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFolderFile", DefaultParameterSetName = ParameterSet_FOLDERSBYPIPE)]
    [OutputType(typeof(IEnumerable<File>))]
    public class GetFolderFile : PnPWebRetrievalsCmdlet<File>
    {
        private const string ParameterSet_FOLDERSBYPIPE = "Folder via pipebind";
        private const string ParameterSet_FOLDERBYURL = "Folder via url";

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_FOLDERBYURL)]
        public string FolderSiteRelativeUrl;

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_FOLDERSBYPIPE)]
        public FolderPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string ItemName = string.Empty;

        [Parameter(Mandatory = false)]
        public SwitchParameter Recursive;

        [Parameter(Mandatory = false)]
        public SwitchParameter ExcludeSystemFolders;          

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            var contents = GetContents(FolderSiteRelativeUrl);

            if (!string.IsNullOrEmpty(ItemName))
            {
                contents = contents.Where(f => f.Name.Equals(ItemName, StringComparison.InvariantCultureIgnoreCase));
            }

            WriteObject(contents, true);
        }

        private IEnumerable<File> GetContents(string FolderSiteRelativeUrl)
        {
            Folder targetFolder = null;
            if (string.IsNullOrEmpty(FolderSiteRelativeUrl) && ParameterSetName == ParameterSet_FOLDERSBYPIPE && Identity != null)
            {
                targetFolder = Identity.GetFolder(CurrentWeb);
            }
            else
            {
                string serverRelativeUrl = null;
                if (!string.IsNullOrEmpty(FolderSiteRelativeUrl))
                {
                    serverRelativeUrl = UrlUtility.Combine(CurrentWeb.ServerRelativeUrl, FolderSiteRelativeUrl);
                }

                if(string.IsNullOrEmpty(FolderSiteRelativeUrl))
                {
                    if(ParameterSpecified(nameof(ExcludeSystemFolders)))
                    {
                        WriteWarning($"The {nameof(ExcludeSystemFolders)} parameter is only supported when retrieving a specific folder. It will be ignored.");
                        ExcludeSystemFolders = false;
                    }
                    targetFolder = CurrentWeb.EnsureProperty(w => w.RootFolder);
                }
                else
                {
                    targetFolder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));
                }
            }

            IEnumerable<File> files = null;
            IEnumerable<Folder> folders = null;

            files = ClientContext.LoadQuery(targetFolder.Files.IncludeWithDefaultProperties(RetrievalExpressions)).OrderBy(f => f.Name);

            if (Recursive)
            {
                if(ExcludeSystemFolders.ToBool())
                {
                    folders = ClientContext.LoadQuery(targetFolder.Folders.IncludeWithDefaultProperties(f => f.ListItemAllFields)).Where(f => !ExcludeSystemFolders.ToBool() || !f.ListItemAllFields.ServerObjectIsNull.GetValueOrDefault(false)).OrderBy(f => f.Name);
                }
                else
                {
                    folders = ClientContext.LoadQuery(targetFolder.Folders).OrderBy(f => f.Name);
                }                
            }
            ClientContext.ExecuteQueryRetry();

            IEnumerable<File> folderContent = files;

            if (Recursive && folders.Count() > 0)
            {
                foreach (var folder in folders)
                {
                    var relativeUrl = folder.ServerRelativeUrl.Replace(CurrentWeb.ServerRelativeUrl, "");

                    WriteVerbose($"Processing folder {relativeUrl}");

                    var subFolderContents = GetContents(relativeUrl);
                    folderContent = folderContent.Concat<File>(subFolderContents);
                }
            }

            return folderContent;
        }
    }
}
