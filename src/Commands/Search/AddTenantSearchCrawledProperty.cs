using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search.Administration;
using Microsoft.SharePoint.Client.Search.Portability;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Enums;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.Add, "PnPTenantSearchCrawledProperty", DefaultParameterSetName = ParameterSetKnownPropertySet, SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
    public class AddTenantSearchCrawledProperty : PnPWebCmdlet
    {
        private const string ParameterSetKnownPropertySet = "KnownPropertySet";
        private const string ParameterSetPropertySetGuid = "PropertySetGuid";

        private static readonly IReadOnlyDictionary<SearchCrawledPropertySet, CrawledPropertySetInfo> RecommendedPropertySets =
            new Dictionary<SearchCrawledPropertySet, CrawledPropertySetInfo>
            {
                { SearchCrawledPropertySet.SharePointDefault, new CrawledPropertySetInfo("00130329-0000-0130-C000-000000131346", "SharePoint", true, true, SearchCrawledPropertySet.SharePointDefault) },
                { SearchCrawledPropertySet.SharePointTaxonomy, new CrawledPropertySetInfo("158D7563-AEFF-4DBF-BF16-4A1445F0366C", "SharePoint", false, true, SearchCrawledPropertySet.SharePointTaxonomy) },
                { SearchCrawledPropertySet.SharePointStructured, new CrawledPropertySetInfo("ED280121-B677-4E2A-8FBC-0D9E2325B0A2", "SharePoint", false, true, SearchCrawledPropertySet.SharePointStructured) },
                { SearchCrawledPropertySet.SharePointRich, new CrawledPropertySetInfo("FEA84DF6-A0FC-492C-9AA7-D28B8DCB08B3", "SharePoint", false, true, SearchCrawledPropertySet.SharePointRich) }
            };

        private static readonly IReadOnlyDictionary<Guid, CrawledPropertySetInfo> AllowedPropertySetIds =
            RecommendedPropertySets.Values
                .Concat(new[]
                {
                    new CrawledPropertySetInfo("F29F85E0-4FF9-1068-AB91-08002B27B3D9", "Office", false, false),
                    new CrawledPropertySetInfo("D5CDD502-2E9C-101B-9397-08002B2CF9AE", "Office", false, false),
                    new CrawledPropertySetInfo("D1B5D3F0-C0B3-11CF-9A92-00A0C908DBF1", "SharePoint", false, false),
                    new CrawledPropertySetInfo("012357BD-1113-171D-1F25-292BB0B0B0B0", "Internal", false, false),
                    new CrawledPropertySetInfo("B725F130-47EF-101A-A5F1-02608C9EEBAC", "Basic", false, false),
                    new CrawledPropertySetInfo("49691C90-7E17-101A-A91C-08002B2ECDA9", "Basic", false, false),
                    new CrawledPropertySetInfo("C82BF597-B831-11D0-B733-00AA00A1EBD2", "Basic", false, false),
                    new CrawledPropertySetInfo("70EB7A10-55D9-11CF-B75B-00AA0051FE20", "Basic", false, false),
                    new CrawledPropertySetInfo("00140329-0000-0140-C000-000000141446", "SharePoint", false, false),
                    new CrawledPropertySetInfo("00110329-0000-0110-C000-000000111146", "SharePoint", false, false),
                    new CrawledPropertySetInfo("0B63E343-9CCC-11D0-BCDB-00805FCCCE04", "Basic", false, false),
                    new CrawledPropertySetInfo("00020329-0000-0000-C000-000000000046", "SharePoint", false, false),
                    new CrawledPropertySetInfo("0C4B2ABA-0518-4EC2-807E-25DD264B660F", "SharePoint", false, false)
                })
                .ToDictionary(propertySet => propertySet.Id);

        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Name;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetKnownPropertySet)]
        public SearchCrawledPropertySet PropertySet;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetPropertySetGuid)]
        public Guid PropertySetGuid;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (!ClientContext.Url.Contains("-admin", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(Resources.CurrentSiteIsNoTenantAdminSite);
            }

            var propertySet = ResolvePropertySet();
            var propertySetDescription = propertySet.Info.PropertySet?.ToString() ?? propertySet.Info.Id.ToString("D", CultureInfo.InvariantCulture);

            if (!ShouldProcess(Name, $"Create tenant search crawled property in property set '{propertySetDescription}'"))
            {
                return;
            }

            ConfirmIfNeeded(propertySet);
            var schemaId = ResolveTenantSchemaId();
            var configuration = BuildSearchConfigurationXml(Name, propertySet.Info.Id, propertySet.Info.CategoryName, propertySet.Info.MapToContents, schemaId);
            LogDebug($"Creating tenant crawled property '{Name}' using property set '{propertySetDescription}' ({propertySet.Info.Id}), category '{propertySet.Info.CategoryName}', and schema ID '{schemaId}'.");
            LogDebug(configuration);

            try
            {
                ClientContext.ImportSearchSettingsConfiguration(configuration, SearchObjectLevel.SPSiteSubscription);
            }
            catch (ServerException ex)
            {
                var details = $"Search configuration import failed: {ex.Message}";
                if (!string.IsNullOrEmpty(ex.ServerErrorTypeName))
                {
                    details += $" ServerErrorTypeName: {ex.ServerErrorTypeName}.";
                }
                if (!string.IsNullOrEmpty(ex.ServerErrorTraceCorrelationId))
                {
                    details += $" CorrelationId: {ex.ServerErrorTraceCorrelationId}.";
                }
                throw new InvalidOperationException(details, ex);
            }

            WriteObject(new
            {
                Name,
                PropertySet = propertySet.Info.PropertySet?.ToString(),
                PropertySetGuid = propertySet.Info.Id,
                CategoryName = propertySet.Info.CategoryName,
                MapToContents = propertySet.Info.MapToContents,
                SchemaId = schemaId,
                Imported = true
            });
        }

        internal static string BuildSearchConfigurationXml(string name, Guid propertySetId, string categoryName, bool mapToContents, int schemaId)
        {
            XNamespace portability = "http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Portability";
            XNamespace admin = "http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration";
            XNamespace query = "http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration.Query";
            XNamespace arrays = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
            XNamespace knownTypes = "http://www.microsoft.com/sharepoint/search/KnownTypes/2008/08";
            XNamespace i = "http://www.w3.org/2001/XMLSchema-instance";

            return new XDocument(
                new XElement(portability + "SearchConfigurationSettings",
                    new XAttribute(XNamespace.Xmlns + "i", i),
                    CreateSearchQueryConfigurationSettings(portability, admin, query, knownTypes),
                    new XElement(portability + "SearchRankingModelConfigurationSettings",
                        new XElement(portability + "RankingModels",
                            new XAttribute(XNamespace.Xmlns + "d3p1", arrays))),
                    new XElement(portability + "SearchSchemaConfigurationSettings",
                        CreateEmptyInfoCollection(portability + "Aliases", admin, arrays, i),
                        new XElement(portability + "CategoriesAndCrawledProperties",
                            new XAttribute(XNamespace.Xmlns + "d3p1", arrays),
                            new XElement(arrays + "KeyValueOfguidCrawledPropertyInfoCollectionaSYUqUE_P",
                                new XElement(arrays + "Key", propertySetId.ToString("D", CultureInfo.InvariantCulture)),
                                new XElement(arrays + "Value",
                                    new XAttribute(XNamespace.Xmlns + "d5p1", admin),
                                    new XElement(admin + "LastItemName", name),
                                    new XElement(admin + "dictionary",
                                        new XElement(arrays + "KeyValueOfstringCrawledPropertyInfoy6h3NzC8",
                                            new XElement(arrays + "Key", name),
                                            new XElement(arrays + "Value",
                                                new XElement(admin + "Name", name),
                                                new XElement(admin + "CategoryName", categoryName),
                                                new XElement(admin + "IsImplicit", false),
                                                new XElement(admin + "IsMappedToContents", mapToContents),
                                                new XElement(admin + "IsNameEnum", false),
                                                new XElement(admin + "MappedManagedProperties"),
                                                new XElement(admin + "Propset", propertySetId.ToString("D", CultureInfo.InvariantCulture)),
                                                new XElement(admin + "Samples"),
                                                new XElement(admin + "SchemaId", schemaId))))))),
                        CreateEmptyInfoCollection(portability + "CrawledProperties", admin, arrays, i),
                        CreateEmptyInfoCollection(portability + "ManagedProperties", admin, arrays, i, includeTotalCount: true),
                        CreateEmptyDictionaryCollection(portability + "Mappings", admin, arrays),
                        CreateEmptyDictionaryCollection(portability + "Overrides", admin, arrays),
                        new XElement(portability + "SchemaId", schemaId)),
                    new XElement(portability + "SearchSubscriptionSettingsConfigurationSettings", new XAttribute(i + "nil", "true")),
                    new XElement(portability + "SearchTaxonomyConfigurationSettings", new XAttribute(i + "nil", "true"))))
                .ToString(SaveOptions.DisableFormatting);
        }

        private static XElement CreateSearchQueryConfigurationSettings(XNamespace portability, XNamespace admin, XNamespace query, XNamespace knownTypes)
        {
            return new XElement(portability + "SearchQueryConfigurationSettings",
                new XElement(portability + "SearchQueryConfigurationSettings",
                    new XElement(portability + "BestBets", new XAttribute(XNamespace.Xmlns + "d4p1", knownTypes)),
                    new XElement(portability + "DefaultSourceId", Guid.Empty.ToString("D", CultureInfo.InvariantCulture)),
                    new XElement(portability + "DefaultSourceIdSet", true),
                    new XElement(portability + "DeployToParent", false),
                    new XElement(portability + "DisableInheritanceOnImport", false),
                    new XElement(portability + "QueryRuleGroups", new XAttribute(XNamespace.Xmlns + "d4p1", knownTypes)),
                    new XElement(portability + "QueryRules", new XAttribute(XNamespace.Xmlns + "d4p1", knownTypes)),
                    new XElement(portability + "ResultTypes", new XAttribute(XNamespace.Xmlns + "d4p1", admin)),
                    new XElement(portability + "Sources", new XAttribute(XNamespace.Xmlns + "d4p1", query)),
                    new XElement(portability + "UserSegments", new XAttribute(XNamespace.Xmlns + "d4p1", knownTypes))));
        }

        private static XElement CreateEmptyInfoCollection(XName name, XNamespace itemNamespace, XNamespace dictionaryNamespace, XNamespace nilNamespace, bool includeTotalCount = false)
        {
            var element = new XElement(name,
                new XAttribute(XNamespace.Xmlns + "d3p1", itemNamespace),
                new XElement(itemNamespace + "LastItemName", new XAttribute(nilNamespace + "nil", "true")),
                new XElement(itemNamespace + "dictionary", new XAttribute(XNamespace.Xmlns + "d4p1", dictionaryNamespace)));

            if (includeTotalCount)
            {
                element.Add(new XElement(itemNamespace + "TotalCount", 0));
            }

            return element;
        }

        private static XElement CreateEmptyDictionaryCollection(XName name, XNamespace itemNamespace, XNamespace dictionaryNamespace)
        {
            return new XElement(name,
                new XAttribute(XNamespace.Xmlns + "d3p1", itemNamespace),
                new XElement(itemNamespace + "dictionary", new XAttribute(XNamespace.Xmlns + "d4p1", dictionaryNamespace)));
        }

        private int ResolveTenantSchemaId()
        {
            SearchObjectOwner owningScope = new SearchObjectOwner(ClientContext, SearchObjectLevel.SPSiteSubscription);
            var config = new SearchConfigurationPortability(ClientContext);
            ClientResult<string> configuration = config.ExportSearchConfiguration(owningScope);
            ClientContext.ExecuteQueryRetry();

            var schemaIdValue = XDocument.Parse(configuration.Value)
                .Descendants()
                .FirstOrDefault(element => element.Name.LocalName == "SchemaId")?.Value;

            if (int.TryParse(schemaIdValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out var schemaId))
            {
                return schemaId;
            }

            throw new InvalidOperationException("Could not resolve the tenant search schema ID from the exported search configuration.");
        }

        private ResolvedPropertySet ResolvePropertySet()
        {
            if (ParameterSetName == ParameterSetPropertySetGuid)
            {
                if (!AllowedPropertySetIds.TryGetValue(PropertySetGuid, out var propertySetInfo))
                {
                    throw new ArgumentException($"Property set '{PropertySetGuid}' is not supported by this cmdlet.");
                }

                return new ResolvedPropertySet(propertySetInfo, true);
            }

            return new ResolvedPropertySet(RecommendedPropertySets[PropertySet], false);
        }

        private void ConfirmIfNeeded(ResolvedPropertySet propertySet)
        {
            var warnings = GetWarnings(Name, propertySet).ToList();
            if (warnings.Count == 0)
            {
                return;
            }

            var message = string.Join(Environment.NewLine, warnings) + Environment.NewLine + "Do you want to continue creating the tenant crawled property?";
            if (!Force && !ShouldContinue(message, Resources.Confirm))
            {
                throw new InvalidOperationException("Crawled property creation cancelled.");
            }

            foreach (var warning in warnings)
            {
                LogWarning(warning);
            }
        }

        private static IEnumerable<string> GetWarnings(string name, ResolvedPropertySet propertySet)
        {
            if (propertySet.UsedGuidParameter)
            {
                yield return "You are using a property set GUID directly. Prefer -PropertySet unless you are reproducing an existing crawled property pattern.";
            }

            if (!propertySet.Info.IsRecommended)
            {
                yield return "The selected property set GUID is for a less common property set. Most SharePoint crawled properties should use SharePointDefault, SharePointTaxonomy, SharePointStructured, or SharePointRich.";
            }

            var expectedPropertySet = GetExpectedPropertySet(name);
            if (expectedPropertySet.HasValue && propertySet.Info.PropertySet != expectedPropertySet.Value)
            {
                var selectedPropertySet = propertySet.Info.PropertySet?.ToString() ?? propertySet.Info.Id.ToString("D", CultureInfo.InvariantCulture);
                yield return $"The crawled property name '{name}' usually belongs in '{expectedPropertySet.Value}', but '{selectedPropertySet}' was selected.";
            }
        }

        private static SearchCrawledPropertySet? GetExpectedPropertySet(string name)
        {
            if (name.StartsWith("ows_taxId", StringComparison.OrdinalIgnoreCase))
            {
                return SearchCrawledPropertySet.SharePointTaxonomy;
            }

            if (name.StartsWith("ows_q_", StringComparison.OrdinalIgnoreCase))
            {
                return SearchCrawledPropertySet.SharePointStructured;
            }

            if (name.StartsWith("ows_r_", StringComparison.OrdinalIgnoreCase))
            {
                return SearchCrawledPropertySet.SharePointRich;
            }

            if (name.StartsWith("ows_", StringComparison.OrdinalIgnoreCase))
            {
                return SearchCrawledPropertySet.SharePointDefault;
            }

            return null;
        }

        private sealed class ResolvedPropertySet
        {
            internal ResolvedPropertySet(CrawledPropertySetInfo info, bool usedGuidParameter)
            {
                Info = info;
                UsedGuidParameter = usedGuidParameter;
            }

            internal CrawledPropertySetInfo Info { get; }

            internal bool UsedGuidParameter { get; }
        }

        private sealed class CrawledPropertySetInfo
        {
            internal CrawledPropertySetInfo(string id, string categoryName, bool mapToContents, bool isRecommended, SearchCrawledPropertySet? propertySet = null)
            {
                Id = Guid.Parse(id);
                CategoryName = categoryName;
                MapToContents = mapToContents;
                IsRecommended = isRecommended;
                PropertySet = propertySet;
            }

            internal Guid Id { get; }

            internal string CategoryName { get; }

            internal bool MapToContents { get; }

            internal bool IsRecommended { get; }

            internal SearchCrawledPropertySet? PropertySet { get; }
        }
    }
}
