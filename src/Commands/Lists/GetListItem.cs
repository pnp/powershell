using System;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPListItem", DefaultParameterSetName = ParameterSet_ALLITEMS)]
    [OutputType(typeof(ListItem))]
    public class GetListItem : PnPWebRetrievalsCmdlet<ListItem>
    {
        private const string ParameterSet_BYID = "By Id";
        private const string ParameterSet_BYUNIQUEID = "By Unique Id";
        private const string ParameterSet_BYQUERY = "By Query";
        private const string ParameterSet_ALLITEMS = "All Items";
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = ParameterAttribute.AllParameterSets)]
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
                ClientContext.ExecuteQueryRetry();
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
                ClientContext.ExecuteQueryRetry();
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
                    ClientContext.ExecuteQueryRetry();

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
