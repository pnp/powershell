using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFolder", DefaultParameterSetName = ParameterSet_FOLDERSINCURRENTWEB)]
    public class GetFolder : PnPWebRetrievalsCmdlet<Folder>
    {
        private const string ParameterSet_FOLDERSINCURRENTWEB = "Folders in current Web";
        private const string ParameterSet_FOLDERSINLIST = "Folders In List";
        private const string ParameterSet_FOLDERBYURL = "Folder By Url";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_FOLDERBYURL)]
        [Alias("RelativeUrl")]
        public string Url;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSet_FOLDERSINLIST)]
        public ListPipeBind List;

        protected override void ExecuteCmdlet()
        {
            DefaultRetrievalExpressions = new Expression<Func<Folder, object>>[] { f => f.ServerRelativeUrl, f => f.Name, f => f.TimeLastModified, f => f.ItemCount };
            
            switch(ParameterSetName)
            {
                case ParameterSet_FOLDERSINCURRENTWEB:
                {
                    // Query for all folders in the root of the current web
                    ClientContext.Load(CurrentWeb, w => w.Folders.IncludeWithDefaultProperties(RetrievalExpressions));
                    ClientContext.ExecuteQueryRetry();

                    WriteObject(CurrentWeb.Folders, true);
                    break;
                }

                case ParameterSet_FOLDERSINLIST:
                {
                    // Gets the provided list
                    var list = List.GetList(CurrentWeb);

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
                            var folder = listItem.Folder;
                            folder.EnsureProperties(RetrievalExpressions);
                            folders.Add(folder);
                        }

                        WriteObject(folders, true);

                        query.ListItemCollectionPosition = listItems.ListItemCollectionPosition;
                    } while (query.ListItemCollectionPosition != null);
                    break;
                }

                case ParameterSet_FOLDERBYURL:
                {
                    // Query for folders at the provided URL
                    var webServerRelativeUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
                    if (!Url.StartsWith(webServerRelativeUrl, StringComparison.OrdinalIgnoreCase))
                    {
                        Url = UrlUtility.Combine(webServerRelativeUrl, Url);
                    }
                    var folder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(Url));
                    folder.EnsureProperties(RetrievalExpressions);

                    WriteObject(folder);
                    break;
                }
            }
        }
    }
}
