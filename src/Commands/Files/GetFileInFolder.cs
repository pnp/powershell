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
    [Cmdlet(VerbsCommon.Get, "PnPFileInFolder", DefaultParameterSetName = ParameterSet_FOLDERSBYPIPE)]
    [OutputType(typeof(IEnumerable<File>))]
    public class GetFileInFolder : PnPWebRetrievalsCmdlet<File>
    {
        private const string ParameterSet_FOLDERSBYPIPE = "Folder via pipebind";
        private const string ParameterSet_LISTSBYPIPE = "Folder via list pipebind";
        private const string ParameterSet_FOLDERBYURL = "Folder via url";

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_FOLDERBYURL)]
        public string FolderSiteRelativeUrl;

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_FOLDERSBYPIPE)]
        public FolderPipeBind Identity;

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_LISTSBYPIPE)]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public string ItemName = string.Empty;

        [Alias("Recurse")]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FOLDERBYURL)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FOLDERSBYPIPE)]
        public SwitchParameter Recursive;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FOLDERBYURL)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FOLDERSBYPIPE)]
        public SwitchParameter ExcludeSystemFolders;          

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            IEnumerable<File> contents = null;
            if (ParameterSetName == ParameterSet_LISTSBYPIPE)
            {
                // Get the files from the list, supporting large lists
                contents = GetContentsFromDocumentLibrary(List.GetList(CurrentWeb));
            }
            else
            {
                // Get the files from the file system, not supporting large lists
                contents = GetContentsByUrl(FolderSiteRelativeUrl);
            }

            if (!string.IsNullOrEmpty(ItemName))
            {
                contents = contents.Where(f => f.Name.Equals(ItemName, StringComparison.InvariantCultureIgnoreCase));
            }

            WriteObject(contents, true);
        }

        private IEnumerable<File> GetContentsFromDocumentLibrary(List documentLibrary)
        {
            var query = CamlQuery.CreateAllItemsQuery();
            var queryElement = XElement.Parse(query.ViewXml);

            var rowLimit = new XElement("RowLimit");
            rowLimit.SetAttributeValue("Paged", "TRUE");
            rowLimit.SetValue(1000);
            queryElement.Add(rowLimit);

            query.ViewXml = queryElement.ToString();

            List<File> results = [];

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
                                                                    item => item.File));
                
                if(ParameterSpecified(nameof(Includes)))
                {
                    var expressions = Includes.Select(i => (Expression<Func<ListItem, object>>)Utilities.DynamicExpression.ParseLambda(typeof(ListItem), typeof(object), $"File.{i}", null)).ToArray();
                    ClientContext.Load(listItems, items => items.Include(expressions));
                }
                ClientContext.ExecuteQueryRetry();

                results.AddRange(listItems.Where(item => item.FileSystemObjectType == FileSystemObjectType.File).Select(item => item.File));

                query.ListItemCollectionPosition = listItems.ListItemCollectionPosition;
            } while (query.ListItemCollectionPosition != null);

            return results;
        }

        private IEnumerable<File> GetContentsByUrl(string FolderSiteRelativeUrl)
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
                    var relativeUrl = folder.ServerRelativeUrl.Remove(0, CurrentWeb.ServerRelativeUrl.Length);

                    WriteVerbose($"Processing folder {relativeUrl}");

                    var subFolderContents = GetContentsByUrl(relativeUrl);
                    folderContent = folderContent.Concat(subFolderContents);
                }
            }

            return folderContent;
        }
    }
}
