using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFolder", DefaultParameterSetName = ParameterSet_FOLDERSINCURRENTWEB)]
    public class GetFolder : PnPWebRetrievalsCmdlet<Folder>
    {
        private const string ParameterSet_FOLDERSINCURRENTWEB = "Folders in current Web";
        private const string ParameterSet_CURRENTWEBROOTFOLDER = "Root folder of the current Web";
        private const string ParameterSet_LISTROOTFOLDER = "Root folder of a list";
        private const string ParameterSet_FOLDERSINLIST = "Folders In List";
        private const string ParameterSet_FOLDERBYURL = "Folder by url";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_FOLDERBYURL)]
        [Alias("RelativeUrl")]
        public string Url;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSet_FOLDERSINLIST)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        [Obsolete("Please transition to using Get-PnPFolder -ListRootFolder <folder> | Get-PnPFolderInFolder instead as this argument will be removed in a future version of the PnP PowerShell Cmdlets")]
        public ListPipeBind List;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSet_LISTROOTFOLDER)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind ListRootFolder;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSet_CURRENTWEBROOTFOLDER)]
        public SwitchParameter CurrentWebRootFolder;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FOLDERBYURL)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LISTROOTFOLDER)]
        public SwitchParameter AsListItem;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<Folder, object>>[] { f => f.ServerRelativeUrl, f => f.Name, f => f.TimeLastModified, f => f.ItemCount };

            Folder folder = null;
            switch (ParameterSetName)
            {
                case ParameterSet_FOLDERSINCURRENTWEB:
                    {
                        WriteVerbose("Getting all folders in the root of the current web");
                        ClientContext.Load(CurrentWeb, w => w.Folders.IncludeWithDefaultProperties(RetrievalExpressions));
                        ClientContext.ExecuteQueryRetry();
                        WriteObject(CurrentWeb.Folders, true);
                        break;
                    }

                case ParameterSet_CURRENTWEBROOTFOLDER:
                    {
                        WriteVerbose("Getting root folder of the current web");
                        folder = CurrentWeb.RootFolder;

                        ReturnFolderProperties(folder);
                        break;
                    }

                case ParameterSet_LISTROOTFOLDER:
                    {
                        WriteVerbose("Getting root folder of the provided list");
                        var list = ListRootFolder.GetList(CurrentWeb);
                        folder = list.RootFolder;

                        ReturnFolderProperties(folder);
                        break;
                    }

                case ParameterSet_FOLDERSINLIST:
                    {
                        // Gets the provided list
#pragma warning disable CS0618 // Type or member is obsolete                    
                        var list = List.GetList(CurrentWeb);
#pragma warning restore CS0618 // Type or member is obsolete                    

                        // Query for all folders in the list
                        CamlQuery query = CamlQuery.CreateAllFoldersQuery();
                        do
                        {
                            // Execute the query. It will retrieve all properties of the folders. Refraining to using the RetrievalExpressions would cause a tremendous increased load on SharePoint as it would have to execute a query per list item which would be less efficient, especially on lists with many folders, than just getting all properties directly
                            ListItemCollection listItems = list.GetItems(query);
                            ClientContext.Load(listItems, item => item.Include(t => t.Folder), item => item.ListItemCollectionPosition);
                            ClientContext.ExecuteQueryRetry();

                            // Take all the folders from the resulting list items and put them in a list to return
                            var folders = new List<Folder>(listItems.Count);
                            foreach (ListItem listItem in listItems)
                            {
                                var listFolder = listItem.Folder;
                                listFolder.EnsureProperties(RetrievalExpressions);
                                folders.Add(listFolder);
                            }

                            WriteObject(folders, true);

                            query.ListItemCollectionPosition = listItems.ListItemCollectionPosition;
                        } while (query.ListItemCollectionPosition != null);
                        break;
                    }

                case ParameterSet_FOLDERBYURL:
                    {
                        WriteVerbose("Getting folder at the provided url");
                        var webServerRelativeUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
                        if (!Url.StartsWith(webServerRelativeUrl, StringComparison.OrdinalIgnoreCase))
                        {
                            Url = UrlUtility.Combine(webServerRelativeUrl, Url);
                        }
                        folder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(Url));

                        ReturnFolderProperties(folder);
                        break;
                    }
            }
        }

        private void ReturnFolderProperties(Folder folder)
        {
            WriteVerbose("Retrieving folder properties");

            if (AsListItem.IsPresent)
            {
                folder?.EnsureProperties(RetrievalExpressions);
                folder?.EnsureProperties(f => f.Exists, f => f.ListItemAllFields);
                if (folder.Exists)
                {
                    WriteObject(folder.ListItemAllFields);
                }
            }
            else
            {
                folder?.EnsureProperties(RetrievalExpressions);
                WriteObject(folder, false);
            }

        }
    }
}
