using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;
using File = Microsoft.SharePoint.Client.File;
using Folder = Microsoft.SharePoint.Client.Folder;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFolderItem", DefaultParameterSetName = ParameterSet_FOLDERSBYPIPE)]
    [OutputType(typeof(IEnumerable<ClientObject>))]
    public class GetFolderItem : PnPWebCmdlet
    {
        private const string ParameterSet_FOLDERSBYPIPE = "Folder via folder pipebind";
        private const string ParameterSet_LISTSBYPIPE = "Folder via list pipebind";
        private const string ParameterSet_FOLDERBYURL = "Folder via url";

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_FOLDERBYURL)]
        public string FolderSiteRelativeUrl;

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_FOLDERSBYPIPE)]
        public FolderPipeBind Identity;

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_LISTSBYPIPE)]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        [ValidateSet("Folder", "File", "All")]
        public string ItemType = "All";

        [Parameter(Mandatory = false)]
        public string ItemName = string.Empty;

        [Alias("Recurse")]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FOLDERBYURL)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FOLDERSBYPIPE)]
        public SwitchParameter Recursive;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LISTSBYPIPE)]
        public string[] Includes;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            IEnumerable<object> contents = null;
            if (ParameterSetName == ParameterSet_LISTSBYPIPE)
            {
                // Get the files and folders from the list, supporting large lists
                contents = GetContentsFromDocumentLibrary(List.GetList(CurrentWeb));
            }
            else
            {
                // Get the files and folders from the file system, not supporting large lists
                contents = GetContentsByUrl(FolderSiteRelativeUrl);
            }

            if (!string.IsNullOrEmpty(ItemName))
            {
                var filteredContents = new List<object>();
                foreach(var item in contents)
                {
                    if(item is Folder folder)
                    {
                        if(folder.Name.Equals(ItemName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            filteredContents.Add(folder);
                        }
                    }
                    else if(item is File file)
                    {
                        if(file.Name.Equals(ItemName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            filteredContents.Add(file);
                        }
                    }
                }

                contents = filteredContents;
            }

            WriteObject(contents, true);
        }

        private IEnumerable<ClientObject> GetContentsFromDocumentLibrary(List documentLibrary)
        {
            var query = CamlQuery.CreateAllItemsQuery();
            var queryElement = XElement.Parse(query.ViewXml);

            var rowLimit = new XElement("RowLimit");
            rowLimit.SetAttributeValue("Paged", "TRUE");
            rowLimit.SetValue(1000);
            queryElement.Add(rowLimit);

            query.ViewXml = queryElement.ToString();

            List<ClientObject> results = [];

            do
            {
                var listItems = documentLibrary.GetItems(query);
                // Call ClientContext.Load() with and without retrievalExpressions to load FieldValues, otherwise no fields will be loaded (CSOM behavior)
                ClientContext.Load(listItems);
                ClientContext.Load(listItems, items => items.Include(item => item.FileSystemObjectType, 
                                                                    item => item.Id, 
                                                                    item => item.DisplayName, 
                                                                    item => item["FileLeafRef"],
                                                                    item => item["FileRef"], 
                                                                    item => item.File,
                                                                    item => item.Folder));
                
                if(ParameterSpecified(nameof(Includes)))
                {
                    var expressions = Includes.Select(i => (Expression<Func<ListItem, object>>)Utilities.DynamicExpression.ParseLambda(typeof(ListItem), typeof(object), i, null)).ToArray();
                    ClientContext.Load(listItems, items => items.Include(expressions));
                }
                ClientContext.ExecuteQueryRetry();

                foreach (var listItem in listItems)
                {
                    if(listItem.FileSystemObjectType == FileSystemObjectType.File && (ItemType == "File" || ItemType == "All"))
                    {
                        results.Add(listItem.File);
                    }
                    if(listItem.FileSystemObjectType == FileSystemObjectType.Folder && (ItemType == "Folder" || ItemType == "All"))
                    {
                        results.Add(listItem.Folder);
                    }
                }

                results.AddRange(listItems);

                query.ListItemCollectionPosition = listItems.ListItemCollectionPosition;
            } while (query.ListItemCollectionPosition != null);

            return results;
        }

        private IEnumerable<object> GetContentsByUrl(string FolderSiteRelativeUrl)
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
                    targetFolder = CurrentWeb.EnsureProperty(w => w.RootFolder);
                }
                else
                {
                    targetFolder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));
                }
            }

            IEnumerable<File> files = null;
            IEnumerable<Folder> folders = null;

            if (ItemType == "File" || ItemType == "All")
            {
                files = ClientContext.LoadQuery(targetFolder.Files).OrderBy(f => f.Name);
            }
            if (ItemType == "Folder" || ItemType == "All" || Recursive)
            {
                folders = ClientContext.LoadQuery(targetFolder.Folders).OrderBy(f => f.Name);
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

            if (Recursive && folders.Count() > 0)
            {
                foreach (var folder in folders)
                {
                    var relativeUrl = folder.ServerRelativeUrl.Replace(CurrentWeb.ServerRelativeUrl, "");

                    WriteVerbose($"Processing folder {relativeUrl}");

                    var subFolderContents = GetContentsByUrl(relativeUrl);
                    folderContent = folderContent.Concat<object>(subFolderContents);
                }
            }

            return folderContent;
        }
    }
}
