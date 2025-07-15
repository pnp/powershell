using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers;
using PnP.Framework.Provisioning.Providers.Xml;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using SPSite = Microsoft.SharePoint.Client.Site;
using PnP.PowerShell.Commands.Base.Completers;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    [Cmdlet(VerbsCommon.Add, "PnPDataRowsToSiteTemplate")]
    public class AddDataRowsToSiteTemplate : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Path;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string Query;

        [Parameter(Mandatory = false)]
        public string[] Fields;

        [Parameter(Mandatory = false, Position = 5)]
        public SwitchParameter IncludeSecurity;

        [Parameter(Mandatory = false, Position = 4)]
        public ITemplateProviderExtension[] TemplateProviderExtensions;

        [Parameter(Mandatory = false)]
        public SwitchParameter TokenizeUrls;

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string KeyColumn;

        private readonly static FieldType[] _unsupportedFieldTypes =
        {
            FieldType.Attachments,
            FieldType.Computed
        };

        protected override void ExecuteCmdlet()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }

            var template = ProvisioningHelper.LoadSiteTemplateFromFile(Path, TemplateProviderExtensions, (e) =>
                    {
                        LogError(e);
                    });

            if (template == null)
            {
                throw new ApplicationException("Invalid template file!");
            }
            // We will remove a list if it's found so we can get the list

            List spList = List.GetListOrThrow(nameof(List), CurrentWeb,
                           l => l.RootFolder, l => l.HasUniqueRoleAssignments);

            var tokenParser = new Framework.Provisioning.ObjectHandlers.TokenParser(ClientContext.Web, template);

            ListInstance listInstance = template.Lists.Find(l => tokenParser.ParseString(l.Title) == spList.Title);
            if (listInstance == null)
            {
                throw new ApplicationException("List does not exist in the template file!");
            }

            if (!string.IsNullOrEmpty(KeyColumn))
            {
                listInstance.DataRows.KeyColumn = KeyColumn;
            }

            ClientContext.Load(ClientContext.Web, w => w.Url, w => w.ServerRelativeUrl, w => w.Id);
            ClientContext.Load(ClientContext.Site, s => s.Url, s => s.ServerRelativeUrl, s => s.Id);

            CamlQuery query = new CamlQuery();

            var viewFieldsStringBuilder = new StringBuilder();
            if (Fields != null)
            {
                viewFieldsStringBuilder.Append("<ViewFields>");
                foreach (var field in Fields)
                {
                    viewFieldsStringBuilder.AppendFormat("<FieldRef Name='{0}'/>", field);
                }
                viewFieldsStringBuilder.Append("</ViewFields>");
            }

            query.ViewXml = string.Format("<View Scope='RecursiveAll'>{0}{1}</View>", Query, viewFieldsStringBuilder);
            List<ListItem> listItems = new List<ListItem>();
            do
            {
                var listItemsCollection = spList.GetItems(query);

                ClientContext.Load(listItemsCollection, lI => lI.Include(l => l.HasUniqueRoleAssignments, l => l.ContentType.StringId));
                ClientContext.ExecuteQueryRetry();

                listItemsCollection.EnsureProperty(l => l.ListItemCollectionPosition);

                query.ListItemCollectionPosition = listItemsCollection.ListItemCollectionPosition;
                listItems.AddRange(listItemsCollection);

            } while (query.ListItemCollectionPosition != null);

            Microsoft.SharePoint.Client.FieldCollection fieldCollection = spList.Fields;
            ClientContext.Load(fieldCollection, fs => fs.Include(f => f.InternalName, f => f.FieldTypeKind, f => f.ReadOnlyField));
            ClientContext.ExecuteQueryRetry();

            var rows = new DataRowCollection(template);
            foreach (var listItem in listItems)
            {
                // Make sure we don't pull Folders.. Of course this won't work
                if (listItem.ServerObjectIsNull == false)
                {
                    if (!(listItem.FileSystemObjectType == FileSystemObjectType.Folder))
                    {
                        DataRow row = new DataRow();
                        if (IncludeSecurity)
                        {
                            listItem.EnsureProperty(l => l.HasUniqueRoleAssignments);
                            if (listItem.HasUniqueRoleAssignments)
                            {
                                row.Security.ClearSubscopes = true;
                                row.Security.CopyRoleAssignments = false;

                                var roleAssignments = listItem.RoleAssignments;
                                ClientContext.Load(roleAssignments);
                                ClientContext.ExecuteQueryRetry();

                                ClientContext.Load(roleAssignments, r => r.Include(a => a.Member.LoginName, a => a.Member, a => a.RoleDefinitionBindings));
                                ClientContext.ExecuteQueryRetry();

                                foreach (var roleAssignment in roleAssignments)
                                {
                                    var principalName = roleAssignment.Member.LoginName;
                                    var roleBindings = roleAssignment.RoleDefinitionBindings;
                                    foreach (var roleBinding in roleBindings)
                                    {
                                        row.Security.RoleAssignments.Add(new PnP.Framework.Provisioning.Model.RoleAssignment() { Principal = principalName, RoleDefinition = roleBinding.Name });
                                    }
                                }
                            }
                        }

                        List<Microsoft.SharePoint.Client.Field> fieldsToExport;
                        // Get the fieds to export
                        if (Fields != null)
                        {
                            fieldsToExport = [];
                            foreach (var fieldName in Fields)
                            {
                                // Discard all fields readonly and unsupported field type
                                Microsoft.SharePoint.Client.Field dataField = fieldCollection.FirstOrDefault(f => f.InternalName == fieldName && !f.ReadOnlyField && !_unsupportedFieldTypes.Contains(f.FieldTypeKind));
                                if (dataField != null)
                                {
                                    fieldsToExport.Add(dataField);
                                }
                            }
                        }
                        else
                        {
                            // Discard all fields readonly and unsupported field type
                            fieldsToExport = [.. fieldCollection.AsEnumerable().Where(f => !f.ReadOnlyField && !_unsupportedFieldTypes.Contains(f.FieldTypeKind))];
                        }

                        // Get validate field and get data
                        foreach (Microsoft.SharePoint.Client.Field field in fieldsToExport)
                        {
                            if (listItem.FieldExistsAndUsed(field.InternalName))
                            {
                                var fieldValue = GetFieldValueAsText(ClientContext.Web, listItem, field);
                                if (TokenizeUrls.IsPresent)
                                {
                                    fieldValue = Tokenize(fieldValue, ClientContext.Web, ClientContext.Site);
                                }
                                row.Values.Add(field.InternalName, fieldValue);
                            }
                        }

                        rows.Add(row);
                    }
                }
            }
            template.Lists.Remove(listInstance);
            listInstance.DataRows.AddRange(rows);
            template.Lists.Add(listInstance);

            var outFileName = System.IO.Path.GetFileName(Path);
            var outPath = new FileInfo(Path).DirectoryName;

            var fileSystemConnector = new FileSystemConnector(outPath, "");
            var formatter = XMLPnPSchemaFormatter.LatestFormatter;
            var extension = new FileInfo(Path).Extension.ToLowerInvariant();
            if (extension == ".pnp")
            {
                XMLTemplateProvider provider = new XMLOpenXMLTemplateProvider(new OpenXMLConnector(Path, fileSystemConnector));
                var templateFileName = outFileName.Substring(0, outFileName.LastIndexOf(".", StringComparison.Ordinal)) + ".xml";

                provider.SaveAs(template, templateFileName, formatter, TemplateProviderExtensions);
            }
            else
            {
                XMLTemplateProvider provider = new XMLFileSystemTemplateProvider(Path, "");
                provider.SaveAs(template, Path, formatter, TemplateProviderExtensions);
            }
        }

        private string GetFieldValueAsText(Web web, ListItem listItem, Microsoft.SharePoint.Client.Field field)
        {
            var rawValue = listItem[field.InternalName];
            if (rawValue == null) return null;

            // Since the TaxonomyField is not in the FieldTypeKind enumeration below, a specific check is done here for this type
            if (field is TaxonomyField)
            {
                if (rawValue is TaxonomyFieldValueCollection)
                {
                    List<string> termIds = new List<string>();
                    foreach (var taxonomyValue in (TaxonomyFieldValueCollection)rawValue)
                    {
                        termIds.Add($"{taxonomyValue.TermGuid}");
                    }
                    return String.Join(";", termIds);
                }
                else if (rawValue is TaxonomyFieldValue)
                {
                    return $"{((TaxonomyFieldValue)rawValue).TermGuid}";
                }
            }

            // Specific operations based on the type of field at hand
            switch (field.FieldTypeKind)
            {
                case FieldType.Geolocation:
                    var geoValue = (FieldGeolocationValue)rawValue;
                    return $"{geoValue.Altitude},{geoValue.Latitude},{geoValue.Longitude},{geoValue.Measure}";
                case FieldType.URL:
                    var urlValue = (FieldUrlValue)rawValue;
                    return $"{urlValue.Url},{urlValue.Description}";
                case FieldType.Lookup:
                    var strVal = rawValue as string;
                    if (strVal != null)
                    {
                        return strVal;
                    }
                    var singleLookupValue = rawValue as FieldLookupValue;
                    if (singleLookupValue != null)
                    {
                        return singleLookupValue.LookupId.ToString();
                    }
                    var multipleLookupValue = rawValue as FieldLookupValue[];
                    if (multipleLookupValue != null)
                    {
                        return string.Join(",", multipleLookupValue.Select(lv => lv.LookupId));
                    }
                    throw new Exception("Invalid data in field");
                case FieldType.User:
                    var singleUserValue = rawValue as FieldUserValue;
                    if (singleUserValue != null)
                    {
                        return GetLoginName(web, singleUserValue.LookupId);
                    }
                    var multipleUserValue = rawValue as FieldUserValue[];
                    if (multipleUserValue != null)
                    {
                        return string.Join(",", multipleUserValue.Select(lv => GetLoginName(web, lv.LookupId)));
                    }
                    throw new Exception("Invalid data in field");
                case FieldType.MultiChoice:
                    var multipleChoiceValue = rawValue as string[];
                    if (multipleChoiceValue != null)
                    {
                        return string.Join(";#", multipleChoiceValue);
                    }
                    return Convert.ToString(rawValue);
                case FieldType.DateTime:
                    var dateValue = rawValue as DateTime?;
                    if (dateValue != null)
                    {
                        return string.Format("{0:O}", dateValue.Value.ToUniversalTime());
                    }
                    throw new Exception("Invalid data in field");
                default:
                    return Convert.ToString(rawValue);
            }
        }

        private Dictionary<Guid, Dictionary<int, string>> _webUserCache = new Dictionary<Guid, Dictionary<int, string>>();
        private string GetLoginName(Web web, int userId)
        {
            try
            {
                if (!_webUserCache.ContainsKey(web.Id)) _webUserCache.Add(web.Id, new Dictionary<int, string>());
                if (!_webUserCache[web.Id].ContainsKey(userId))
                {
                    var user = web.GetUserById(userId);
                    web.Context.Load(user, u => u.LoginName);
                    web.Context.ExecuteQueryRetry();
                    _webUserCache[web.Id].Add(userId, user.LoginName);
                }
                return _webUserCache[web.Id][userId];
            }
            catch
            {
                // If user is removed/disabled from AAD, return null
                LogWarning("User cannot be found, skipped adding field value");
                return null;
            }
        }

        private static string Tokenize(string input, Web web, SPSite site)
        {
            if (string.IsNullOrEmpty(input)) return input;

            input = input.ReplaceCaseInsensitive(web.Url, "{site}");
            input = input.ReplaceCaseInsensitive(web.ServerRelativeUrl, "{site}");
            input = input.ReplaceCaseInsensitive(web.Id.ToString(), "{siteid}");
            input = input.ReplaceCaseInsensitive(site.ServerRelativeUrl, "{sitecollection}");
            input = input.ReplaceCaseInsensitive(site.Id.ToString(), "{sitecollectionid}");
            input = input.ReplaceCaseInsensitive(site.Url, "{sitecollection}");

            return input;
        }
    }
}