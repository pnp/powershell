using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPListItem", DefaultParameterSetName = ParameterSet_ALLITEMS)]
    [OutputType(typeof(ListItem))]
    [RequiredApiApplicationPermissions("sharepoint/Sites.Selected")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.Read.All")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.ReadWrite.All")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.Manage.All")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.Read")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.Write")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.Manage")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]

    public class GetListItem : PnPWebRetrievalsCmdlet<ListItem>
    {
        private const string ParameterSet_BYID = "By Id";
        private const string ParameterSet_BYUNIQUEID = "By Unique Id";
        private const string ParameterSet_BYQUERY = "By Query";
        private const string ParameterSet_ALLITEMS = "All Items";
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterAttribute.AllParameterSets)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYID)]
        public int Id = -1;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYUNIQUEID)]
        public Guid UniqueId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYQUERY)]
        public string Query;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYQUERY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLITEMS)]
        public string FolderServerRelativeUrl;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLITEMS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYUNIQUEID)]
        public string[] Fields;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLITEMS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYQUERY)]
        public int PageSize = -1;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLITEMS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYQUERY)]
        public ScriptBlock ScriptBlock;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALLITEMS)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYUNIQUEID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYQUERY)]
        public SwitchParameter IncludeContentType;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb);
            if (list == null)
                throw new PSArgumentException($"No list found with id, title or url '{List}'", "List");

            if (HasId())
            {
                var listItem = list.GetItemById(Id);
                if (Fields != null)
                {
                    foreach (var field in Fields)
                    {
                        ClientContext.Load(listItem, l => l[field]);
                    }
                }
                else
                {
                    ClientContext.Load(listItem);
                }
                if (IncludeContentType)
                {
                    ClientContext.Load(listItem, l => l.ContentType, l => l.ContentType.Name, l => l.ContentType.Id, l => l.ContentType.StringId, l => l.ContentType.Description);
                }
                if (RetrievalExpressions.Length > 0)
                    ClientContext.Load(listItem, RetrievalExpressions);
                // Fields are loaded individually here rather than projected through a view, so the threshold does not apply
                ExecuteQueryRetryWithLookupThresholdHint(list, null);
                WriteObject(listItem);
            }
            else if (UniqueId != Guid.Empty)
            {
                CamlQuery query = new CamlQuery();
                var viewFieldsStringBuilder = new StringBuilder();
                if (HasFields())
                {
                    viewFieldsStringBuilder.Append("<ViewFields>");
                    foreach (var field in Fields)
                    {
                        viewFieldsStringBuilder.AppendFormat("<FieldRef Name='{0}'/>", field);
                    }
                    viewFieldsStringBuilder.Append("</ViewFields>");
                }
                query.ViewXml = $"<View Scope='RecursiveAll'><Query><Where><Or><Eq><FieldRef Name='GUID'/><Value Type='Guid'>{UniqueId}</Value></Eq><Eq><FieldRef Name='UniqueId' /><Value Type='Guid'>{UniqueId}</Value></Eq></Or></Where></Query>{viewFieldsStringBuilder}</View>";

                var listItem = list.GetItems(query);
                // Call ClientContext.Load() with and without retrievalExpressions to load FieldValues, otherwise no fields will be loaded (CSOM behavior)
                ClientContext.Load(listItem);
                ClientContext.Load(listItem, l => l.Include(RetrievalExpressions));
                if (IncludeContentType)
                {
                    ClientContext.Load(listItem, l => l.Include(a => a.ContentType, a => a.ContentType.Id, a => a.ContentType.Name, a => a.ContentType.Description, a => a.ContentType.StringId));
                }
                ExecuteQueryRetryWithLookupThresholdHint(list, GetProjectedFields(query.ViewXml));
                WriteObject(listItem);
            }
            else
            {
                CamlQuery query = HasCamlQuery() ? new CamlQuery { ViewXml = Query } : CamlQuery.CreateAllItemsQuery();
                query.FolderServerRelativeUrl = FolderServerRelativeUrl;

                if (Fields != null)
                {
                    var queryElement = XElement.Parse(query.ViewXml);

                    var viewFields = queryElement.Descendants("ViewFields").FirstOrDefault();
                    if (viewFields != null)
                    {
                        viewFields.RemoveAll();
                    }
                    else
                    {
                        viewFields = new XElement("ViewFields");
                        queryElement.Add(viewFields);
                    }

                    foreach (var field in Fields)
                    {
                        XElement viewField = new XElement("FieldRef");
                        viewField.SetAttributeValue("Name", field);
                        viewFields.Add(viewField);
                    }
                    query.ViewXml = queryElement.ToString();
                }

                if (HasPageSize())
                {
                    var queryElement = XElement.Parse(query.ViewXml);

                    var rowLimit = queryElement.Descendants("RowLimit").FirstOrDefault();
                    if (rowLimit != null)
                    {
                        rowLimit.RemoveAll();
                    }
                    else
                    {
                        rowLimit = new XElement("RowLimit");
                        queryElement.Add(rowLimit);
                    }

                    rowLimit.SetAttributeValue("Paged", "TRUE");
                    rowLimit.SetValue(PageSize);

                    query.ViewXml = queryElement.ToString();
                }

                do
                {
                    var listItems = list.GetItems(query);
                    // Call ClientContext.Load() with and without retrievalExpressions to load FieldValues, otherwise no fields will be loaded (CSOM behavior)
                    ClientContext.Load(listItems);
                    ClientContext.Load(listItems, l => l.Include(RetrievalExpressions));
                    if (IncludeContentType)
                    {
                        ClientContext.Load(listItems, l => l.Include(a => a.ContentType, a => a.ContentType.Id, a => a.ContentType.Name, a => a.ContentType.Description, a => a.ContentType.StringId));
                    }
                    ExecuteQueryRetryWithLookupThresholdHint(list, GetProjectedFields(query.ViewXml));

                    WriteObject(listItems, true);

                    if (ScriptBlock != null)
                    {
                        ScriptBlock.Invoke(listItems);
                    }

                    if (HasPageSize())
                    {
                        query.ListItemCollectionPosition = listItems.ListItemCollectionPosition;
                    }
                } while (query.ListItemCollectionPosition != null);
            }
        }

        /// <summary>
        /// The number of lookup columns SharePoint Online allows a single query to project. A Lookup, Person or Group,
        /// Managed Metadata, Created By or Modified By column each count as one.
        /// </summary>
        private const int LookupColumnThreshold = 12;

        private static readonly string[] LookupColumnTypes = { "Lookup", "LookupMulti", "User", "UserMulti", "TaxonomyFieldType", "TaxonomyFieldTypeMulti" };

        /// <summary>
        /// SharePoint refuses a query that projects more than <see cref="LookupColumnThreshold"/> lookup columns, which is easy
        /// to run into through -Fields or the ViewFields of -Query, and reports it only as a throttled query. Point at what can
        /// be done about it, but leave the original error in place. See https://github.com/pnp/powershell/issues/4311
        ///
        /// The same exception covers the list view threshold, for which this advice would be wrong, so rather than reading the
        /// server message, which is localized, establish from the columns that were asked for whether the limit was exceeded.
        /// </summary>
        private void ExecuteQueryRetryWithLookupThresholdHint(List list, IEnumerable<string> projectedFields)
        {
            try
            {
                ClientContext.ExecuteQueryRetry();
            }
            catch (ServerException e) when (e.ServerErrorTypeName == "Microsoft.SharePoint.SPQueryThrottledException"
                                            && CountLookupColumns(list, projectedFields) > LookupColumnThreshold)
            {
                LogWarning($"The query asks for more than the {LookupColumnThreshold} lookup columns SharePoint allows a single query to project, counting every Lookup, Person or Group, Managed Metadata, Created By and Modified By column. Ask for fewer of those columns through -Fields, or through the ViewFields of -Query, or retrieve the items one at a time with -Id, which is not subject to this limit.");
                throw;
            }
        }

        /// <summary>
        /// Counts how many of the provided columns are of a type that counts towards the lookup column threshold. Runs only
        /// when a query was refused, and reads the field schema of the list, which is not itself subject to the threshold.
        /// </summary>
        private int CountLookupColumns(List list, IEnumerable<string> projectedFields)
        {
            var names = new HashSet<string>(projectedFields ?? Enumerable.Empty<string>(), StringComparer.OrdinalIgnoreCase);
            if (names.Count <= LookupColumnThreshold) return 0;

            try
            {
                var fields = ClientContext.LoadQuery(list.Fields.Include(f => f.InternalName, f => f.Title, f => f.TypeAsString));
                ClientContext.ExecuteQueryRetry();

                return fields.Count(f => (names.Contains(f.InternalName) || names.Contains(f.Title))
                                         && LookupColumnTypes.Contains(f.TypeAsString));
            }
            catch (Exception)
            {
                // Never let establishing whether the advice applies get in the way of reporting the original error.
                return 0;
            }
        }

        /// <summary>
        /// Returns the columns a CAML view projects, so that a refused query can be checked against the lookup column threshold.
        /// An empty result means the query projects whatever SharePoint decides to return, which the threshold does not apply to.
        /// </summary>
        private static IEnumerable<string> GetProjectedFields(string viewXml)
        {
            if (string.IsNullOrWhiteSpace(viewXml)) return Enumerable.Empty<string>();

            try
            {
                return XElement.Parse(viewXml)
                    .Descendants("ViewFields")
                    .Descendants("FieldRef")
                    .Select(f => f.Attribute("Name")?.Value)
                    .Where(n => !string.IsNullOrEmpty(n))
                    .ToArray();
            }
            catch (System.Xml.XmlException)
            {
                return Enumerable.Empty<string>();
            }
        }

        private bool HasId()
        {
            return Id != -1;
        }

        private bool HasCamlQuery()
        {
            return Query != null;
        }

        private bool HasFields()
        {
            return Fields != null;
        }

        private bool HasPageSize()
        {
            return PageSize > 0;
        }
    }
}
