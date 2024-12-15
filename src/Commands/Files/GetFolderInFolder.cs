using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;
using Folder = Microsoft.SharePoint.Client.Folder;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFolderInFolder", DefaultParameterSetName = ParameterSet_FOLDERSBYPIPE)]
    [OutputType(typeof(IEnumerable<Folder>))]
    public class GetFolderInFolder : PnPWebRetrievalsCmdlet<Folder>
    {
        private const string ParameterSet_FOLDERSBYPIPE = "Folder via pipebind";
        private const string ParameterSet_LISTSBYPIPE = "Folder via list pipebind";
        private const string ParameterSet_FOLDERBYURL = "Folder via url";

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_FOLDERBYURL)]
        public string FolderSiteRelativeUrl;

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterSet_FOLDERSBYPIPE)]
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

            if(ExcludeSystemFolders.ToBool())
            {
                DefaultRetrievalExpressions = [f => f.ListItemAllFields];
            }

            IEnumerable<Folder> contents = null;
            if (ParameterSetName == ParameterSet_LISTSBYPIPE)
            {
                // Get the folders from the list, supporting large lists
                contents = GetContentsFromDocumentLibrary(List.GetList(CurrentWeb));
            }
            else
            {
                // Get the folders from the file system, not supporting large lists
                contents = GetContentsByUrl(FolderSiteRelativeUrl);
            }

            if (!string.IsNullOrEmpty(ItemName))
            {
                contents = contents.Where(f => f.Name.Equals(ItemName, StringComparison.InvariantCultureIgnoreCase));
            }

            WriteObject(contents, true);
        }

        private IEnumerable<Folder> GetContentsFromDocumentLibrary(List documentLibrary)
        {
            var query = CamlQuery.CreateAllItemsQuery();
            var queryElement = XElement.Parse(query.ViewXml);

            var rowLimit = new XElement("RowLimit");
            rowLimit.SetAttributeValue("Paged", "TRUE");
            rowLimit.SetValue(1000);
            queryElement.Add(rowLimit);

            query.ViewXml = queryElement.ToString();

            List<Folder> results = [];

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
                                                                    item => item.Folder));
                
                if(ParameterSpecified(nameof(Includes)))
                {
                    var expressions = Includes.Select(i => (Expression<Func<ListItem, object>>)Utilities.DynamicExpression.ParseLambda(typeof(ListItem), typeof(object), $"Folder.{i}", null)).ToArray();
                    ClientContext.Load(listItems, items => items.Include(expressions));
                }
                ClientContext.ExecuteQueryRetry();

                results.AddRange(listItems.Where(item => item.FileSystemObjectType == FileSystemObjectType.Folder).Select(item => item.Folder));

                query.ListItemCollectionPosition = listItems.ListItemCollectionPosition;
            } while (query.ListItemCollectionPosition != null);

            return results;
        }

        private IEnumerable<Folder> GetContentsByUrl(string FolderSiteRelativeUrl)
        {
            Folder targetFolder = null;
            if (string.IsNullOrEmpty(FolderSiteRelativeUrl) && ParameterSetName == ParameterSet_FOLDERSBYPIPE && Identity != null)
            {
                targetFolder = Identity.GetFolder(CurrentWeb);
                CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
            }
            else
            {
                string serverRelativeUrl = null;
                if (!string.IsNullOrEmpty(FolderSiteRelativeUrl))
                {
                    serverRelativeUrl = UrlUtility.Combine(CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl), FolderSiteRelativeUrl);
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

            IEnumerable<Folder> folders = null;
            if(ExcludeSystemFolders.ToBool())
            {
                folders = ClientContext.LoadQuery(targetFolder.Folders.IncludeWithDefaultProperties(f => f.ListItemAllFields)).Where(f => !ExcludeSystemFolders.ToBool() || !f.ListItemAllFields.ServerObjectIsNull.GetValueOrDefault(false)).OrderBy(f => f.Name);
            }
            else
            {
                folders = ClientContext.LoadQuery(targetFolder.Folders).OrderBy(f => f.Name);
            }
            ClientContext.ExecuteQueryRetry();        

            IEnumerable<Folder> folderContent = folders;

            if (Recursive && folders.Count() > 0)
            {
                foreach (var folder in folders)
                {
                    var relativeUrl = folder.ServerRelativeUrl.Replace(CurrentWeb.ServerRelativeUrl, "");

                    WriteVerbose($"Processing folder {relativeUrl}");

                    var subFolderContents = GetContentsByUrl(relativeUrl);
                    folderContent = folderContent.Concat<Folder>(subFolderContents);
                }
            }

            return folderContent;
        }
    }
}
