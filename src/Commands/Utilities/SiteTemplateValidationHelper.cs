using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers.Xml;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;
using FrameworkFileUtilities = PnP.Framework.Provisioning.ObjectHandlers.Utilities.FileUtilities;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class SiteTemplateValidationHelper
    {
        internal static List<SiteTemplateValidationIssue> Validate(ProvisioningTemplate template, XElement sourceElement = null)
        {
            var issues = new List<SiteTemplateValidationIssue>();

            var siteFieldIds = ValidateFields(template.SiteFields, "SiteFields", issues);
            ValidateContentTypes(template, siteFieldIds, issues);
            ValidateLists(template, siteFieldIds, issues);
            ValidateDependencies(template, issues);
            ValidateDeprecatedElements(sourceElement, issues);
            ValidateRequiredResourcePaths(template, issues);

            ValidateResources(template, issues);

            return issues;
        }

        internal static string GetSchemaNamespace(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return null;
            }

            return XDocument.Parse(xml).Root?.Name.NamespaceName;
        }

        internal static SiteTemplateValidationIssue CreateSchemaIssue(Exception exception, string templateFile = null)
        {
            var location = templateFile ?? "Schema";
            if (exception is System.Xml.Schema.XmlSchemaException schemaException && schemaException.LineNumber > 0)
            {
                location = templateFile == null
                    ? $"Line {schemaException.LineNumber}, position {schemaException.LinePosition}"
                    : $"{templateFile} line {schemaException.LineNumber}, position {schemaException.LinePosition}";
            }

            return CreateIssue("SchemaValidationFailed", exception.Message, location);
        }

#pragma warning disable CS0618 // The 2019/03 schema is deprecated but the framework still deserializes it.
        private static readonly HashSet<string> SupportedSchemaNamespaces = new(StringComparer.OrdinalIgnoreCase)
        {
            XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2019_03,
            XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2019_09,
            XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2020_02,
            XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2021_03,
            XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2022_09
        };
#pragma warning restore CS0618

        internal static SiteTemplateValidationIssue CreateSchemaVersionIssue(string schemaNamespace)
        {
            if (string.IsNullOrWhiteSpace(schemaNamespace) ||
                schemaNamespace.Equals(XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2022_09, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            // An unknown namespace falls back to the latest deserializer, which silently matches nothing and yields an empty template.
            if (!SupportedSchemaNamespaces.Contains(schemaNamespace))
            {
                return CreateIssue(
                    "UnsupportedSchema",
                    $"The template uses the unsupported provisioning schema '{schemaNamespace}'. Its contents cannot be read and are ignored, so nothing in it has been validated.",
                    "ProvisioningTemplate");
            }

            return CreateIssue(
                "LegacySchema",
                $"The template uses the older provisioning schema '{schemaNamespace}'. Consider converting it to the latest schema before applying it.",
                "ProvisioningTemplate",
                SiteTemplateValidationSeverity.Warning);
        }

        private static void ValidateContentTypes(ProvisioningTemplate template, HashSet<Guid> siteFieldIds, List<SiteTemplateValidationIssue> issues)
        {
            foreach (var contentType in template.ContentTypes)
            {
                var location = $"ContentTypes[{contentType.Name ?? contentType.Id ?? "unknown"}]";
                if (string.IsNullOrWhiteSpace(contentType.Id))
                {
                    issues.Add(CreateIssue("MissingContentTypeId", "A content type does not define an ID.", location));
                }

                foreach (var fieldRef in contentType.FieldRefs.Where(fieldRef => fieldRef.Id == Guid.Empty))
                {
                    issues.Add(CreateIssue("MissingContentTypeFieldRefId", "A content type field reference does not define an ID.", location));
                }

                AddExternalDependencyIssue(
                    issues,
                    "UnresolvedContentTypeFieldRef",
                    location,
                    "field references",
                    "must already exist on the target site",
                    contentType.FieldRefs
                        .Where(fieldRef => fieldRef.Id != Guid.Empty && !siteFieldIds.Contains(fieldRef.Id))
                        .Select(fieldRef => fieldRef.Id.ToString()));

                AddDuplicateIssues(
                    contentType.FieldRefs.Where(fieldRef => fieldRef.Id != Guid.Empty),
                    fieldRef => fieldRef.Id,
                    duplicateId => CreateIssue("DuplicateContentTypeFieldRef", $"Field reference '{duplicateId}' occurs more than once in content type '{contentType.Name ?? contentType.Id}'.", location),
                    issues);
            }

            AddDuplicateIssues(
                template.ContentTypes.Where(contentType => !string.IsNullOrWhiteSpace(contentType.Id)),
                contentType => contentType.Id,
                duplicateId => CreateIssue("DuplicateContentTypeId", $"Content type ID '{duplicateId}' occurs more than once.", "ContentTypes"),
                issues,
                StringComparer.OrdinalIgnoreCase);
        }

        private static void ValidateLists(ProvisioningTemplate template, HashSet<Guid> siteFieldIds, List<SiteTemplateValidationIssue> issues)
        {
            var contentTypeIds = template.ContentTypes
                .Where(contentType => !string.IsNullOrWhiteSpace(contentType.Id))
                .Select(contentType => contentType.Id)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var list in template.Lists)
            {
                var location = $"Lists[{list.Title ?? list.Url ?? "unknown"}]";
                if (string.IsNullOrWhiteSpace(list.Url))
                {
                    issues.Add(CreateIssue("MissingListUrl", "A list does not define a URL.", location));
                }

                foreach (var binding in list.ContentTypeBindings.Where(binding => string.IsNullOrWhiteSpace(binding.ContentTypeId)))
                {
                    issues.Add(CreateIssue("MissingContentTypeBindingId", "A list content type binding does not define a content type ID.", location));
                }

                AddExternalDependencyIssue(
                    issues,
                    "UnresolvedContentTypeBinding",
                    location,
                    "content types",
                    "must already exist on the target site",
                    list.ContentTypeBindings
                        .Where(binding => !string.IsNullOrWhiteSpace(binding.ContentTypeId) && !contentTypeIds.Contains(binding.ContentTypeId))
                        .Select(binding => binding.ContentTypeId));

                var listFieldIds = ValidateFields(list.Fields, $"{location}.Fields", issues);

                foreach (var fieldRef in list.FieldRefs.Where(fieldRef => fieldRef.Id == Guid.Empty))
                {
                    issues.Add(CreateIssue("MissingListFieldRefId", "A list field reference does not define an ID.", location));
                }

                AddExternalDependencyIssue(
                    issues,
                    "UnresolvedListFieldRef",
                    location,
                    "field references",
                    "must already exist on the target site",
                    list.FieldRefs
                        .Where(fieldRef => fieldRef.Id != Guid.Empty && !siteFieldIds.Contains(fieldRef.Id) && !listFieldIds.Contains(fieldRef.Id))
                        .Select(fieldRef => fieldRef.Id.ToString()));

                AddDuplicateIssues(
                    list.ContentTypeBindings.Where(binding => !string.IsNullOrWhiteSpace(binding.ContentTypeId)),
                    binding => binding.ContentTypeId,
                    duplicateId => CreateIssue("DuplicateContentTypeBinding", $"Content type binding '{duplicateId}' occurs more than once in list '{list.Title ?? list.Url}'.", location),
                    issues,
                    StringComparer.OrdinalIgnoreCase);

                AddDuplicateIssues(
                    list.FieldRefs.Where(fieldRef => fieldRef.Id != Guid.Empty),
                    fieldRef => fieldRef.Id,
                    duplicateId => CreateIssue("DuplicateListFieldRef", $"Field reference '{duplicateId}' occurs more than once in list '{list.Title ?? list.Url}'.", location),
                    issues);
            }

            AddDuplicateIssues(
                template.Lists.Where(list => !string.IsNullOrWhiteSpace(list.Url)),
                list => list.Url,
                duplicateUrl => CreateIssue("DuplicateListUrl", $"List URL '{duplicateUrl}' occurs more than once.", "Lists"),
                issues,
                StringComparer.OrdinalIgnoreCase);
        }

        private static HashSet<Guid> ValidateFields(IEnumerable<Field> fields, string location, List<SiteTemplateValidationIssue> issues)
        {
            var fieldIds = new List<Guid>();
            foreach (var field in fields)
            {
                if (string.IsNullOrWhiteSpace(field.SchemaXml))
                {
                    issues.Add(CreateIssue("MissingFieldSchema", "A field does not define schema XML.", location));
                    continue;
                }

                try
                {
                    var fieldElement = XElement.Parse(field.SchemaXml);
                    var idAttribute = fieldElement.Attribute("ID")?.Value;
                    if (!Guid.TryParse(idAttribute, out var fieldId))
                    {
                        issues.Add(CreateIssue(
                            "InvalidFieldId",
                            idAttribute == null ? "A field does not define an ID." : $"Field ID '{idAttribute}' is not a valid GUID.",
                            location));
                        continue;
                    }
                    fieldIds.Add(fieldId);
                }
                catch (System.Xml.XmlException exception)
                {
                    issues.Add(CreateIssue("InvalidFieldSchema", exception.Message, location));
                }
            }

            AddDuplicateIssues(
                fieldIds,
                fieldId => fieldId,
                duplicateId => CreateIssue("DuplicateFieldId", $"Field ID '{duplicateId}' occurs more than once.", location),
                issues);

            return fieldIds.ToHashSet();
        }

        private static void ValidateDependencies(ProvisioningTemplate template, List<SiteTemplateValidationIssue> issues)
        {
            var termSetIds = template.TermGroups
                .SelectMany(termGroup => termGroup.TermSets)
                .Where(termSet => termSet.Id != Guid.Empty)
                .Select(termSet => termSet.Id)
                .ToHashSet();

            var externalTermSets = new Dictionary<string, List<string>>(StringComparer.Ordinal);

            CollectTermSetDependencies(template.SiteFields, "SiteFields", template, termSetIds, externalTermSets, issues);
            foreach (var list in template.Lists)
            {
                CollectTermSetDependencies(list.Fields, $"Lists[{list.Title ?? list.Url ?? "unknown"}].Fields", template, termSetIds, externalTermSets, issues);
            }
            CollectTermSetDependency(template.Navigation?.GlobalNavigation?.ManagedNavigation?.TermSetId, "Navigation.GlobalNavigation", template, termSetIds, externalTermSets);
            CollectTermSetDependency(template.Navigation?.CurrentNavigation?.ManagedNavigation?.TermSetId, "Navigation.CurrentNavigation", template, termSetIds, externalTermSets);

            foreach (var externalTermSet in externalTermSets)
            {
                AddExternalDependencyIssue(
                    issues,
                    "ExternalTermSetDependency",
                    externalTermSet.Key,
                    "term sets",
                    "must already exist in the target term store",
                    externalTermSet.Value);
            }

            if (!string.IsNullOrWhiteSpace(template.WebSettings?.HubSiteUrl))
            {
                issues.Add(CreateIssue(
                    "ExternalHubSiteDependency",
                    $"Hub site '{template.WebSettings.HubSiteUrl}' must exist and be accessible when the template is applied.",
                    "WebSettings.HubSiteUrl",
                    SiteTemplateValidationSeverity.Information));
            }
        }

        private static void CollectTermSetDependencies(
            IEnumerable<Field> fields,
            string location,
            ProvisioningTemplate template,
            HashSet<Guid> termSetIds,
            Dictionary<string, List<string>> externalTermSets,
            List<SiteTemplateValidationIssue> issues)
        {
            foreach (var field in fields.Where(field => !string.IsNullOrWhiteSpace(field.SchemaXml)))
            {
                XElement fieldElement;
                try
                {
                    fieldElement = XElement.Parse(field.SchemaXml);
                }
                catch (System.Xml.XmlException)
                {
                    continue;
                }

                var fieldType = (string)fieldElement.Attribute("Type");
                if (!string.Equals(fieldType, "TaxonomyFieldType", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(fieldType, "TaxonomyFieldTypeMulti", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var termSetReference = fieldElement.Attribute("TermSetId")?.Value ??
                    fieldElement.Descendants()
                        .FirstOrDefault(element => element.Name.LocalName == "Property" &&
                            element.Elements().Any(child => child.Name.LocalName == "Name" && child.Value == "TermSetId"))?
                        .Elements()
                        .FirstOrDefault(element => element.Name.LocalName == "Value")?
                        .Value;
                if (string.IsNullOrWhiteSpace(termSetReference))
                {
                    issues.Add(CreateIssue(
                        "MissingTermSetReference",
                        "A taxonomy field does not identify its term set.",
                        location,
                        SiteTemplateValidationSeverity.Warning));
                    continue;
                }
                CollectTermSetDependency(termSetReference, location, template, termSetIds, externalTermSets);
            }
        }

        private static void CollectTermSetDependency(
            string termSetReference,
            string location,
            ProvisioningTemplate template,
            HashSet<Guid> termSetIds,
            Dictionary<string, List<string>> externalTermSets)
        {
            if (string.IsNullOrWhiteSpace(termSetReference))
            {
                return;
            }

            if (Guid.TryParse(termSetReference, out var termSetId))
            {
                if (!termSetIds.Contains(termSetId))
                {
                    AddExternalTermSet(termSetReference, location, externalTermSets);
                }
                return;
            }

            if (TryParseTermSetToken(termSetReference, out var groupName, out var termSetName))
            {
                var isDefined = template.TermGroups.Any(group =>
                    string.Equals(group.Name, groupName, StringComparison.OrdinalIgnoreCase) &&
                    group.TermSets.Any(termSet => string.Equals(termSet.Name, termSetName, StringComparison.OrdinalIgnoreCase)));
                if (!isDefined)
                {
                    AddExternalTermSet(termSetReference, location, externalTermSets);
                }
            }
            else if (TryParseSiteCollectionTermSetToken(termSetReference, out termSetName))
            {
                var isDefined = template.TermGroups.Any(group =>
                    group.SiteCollectionTermGroup &&
                    group.TermSets.Any(termSet => string.Equals(termSet.Name, termSetName, StringComparison.OrdinalIgnoreCase)));
                if (!isDefined)
                {
                    AddExternalTermSet(termSetReference, location, externalTermSets);
                }
            }
            else
            {
                AddExternalTermSet(termSetReference, location, externalTermSets);
            }
        }

        private static bool TryParseTermSetToken(string value, out string groupName, out string termSetName)
        {
            groupName = null;
            termSetName = null;
            const string prefix = "{termsetid:";
            if (!value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) || !value.EndsWith('}'))
            {
                return false;
            }

            var parts = value[prefix.Length..^1].Split(':');
            if (parts.Length != 2)
            {
                return false;
            }

            groupName = parts[0];
            termSetName = parts[1];
            return true;
        }

        private static bool TryParseSiteCollectionTermSetToken(string value, out string termSetName)
        {
            termSetName = null;
            const string prefix = "{sitecollectiontermsetid:";
            if (!value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) || !value.EndsWith('}'))
            {
                return false;
            }

            termSetName = value[prefix.Length..^1];
            return !string.IsNullOrWhiteSpace(termSetName);
        }

        private static void AddExternalTermSet(string termSetReference, string location, Dictionary<string, List<string>> externalTermSets)
        {
            if (!externalTermSets.TryGetValue(location, out var references))
            {
                references = [];
                externalTermSets[location] = references;
            }
            references.Add(termSetReference);
        }

        /// <summary>Reports the references a template does not define as one informational issue per location.</summary>
        private static void AddExternalDependencyIssue(
            List<SiteTemplateValidationIssue> issues,
            string code,
            string location,
            string subject,
            string requirement,
            IEnumerable<string> references)
        {
            const int maximumListedReferences = 10;

            var distinctReferences = references
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(reference => reference, StringComparer.OrdinalIgnoreCase)
                .ToList();
            if (distinctReferences.Count == 0)
            {
                return;
            }

            var listedReferences = string.Join(", ", distinctReferences.Take(maximumListedReferences));
            if (distinctReferences.Count > maximumListedReferences)
            {
                listedReferences += $" and {distinctReferences.Count - maximumListedReferences} more";
            }

            issues.Add(CreateIssue(
                code,
                $"{distinctReferences.Count} {subject} not defined in the template {requirement}: {listedReferences}.",
                location,
                SiteTemplateValidationSeverity.Information));
        }

        private static void ValidateDeprecatedElements(XElement sourceElement, List<SiteTemplateValidationIssue> issues)
        {
            if (sourceElement == null)
            {
                return;
            }

            // Teams channels sit under the Provisioning root, outside any site template, so they are out of scope here.
            foreach (var attribute in sourceElement.DescendantsAndSelf().Attributes().Where(attribute =>
                attribute.Name.LocalName == "ClientSideHostProperties" && attribute.Parent?.Name.LocalName == "CustomAction"))
            {
                issues.Add(CreateIssue(
                    "DeprecatedElement",
                    "The 'ClientSideHostProperties' custom action attribute is no longer part of the latest provisioning schema.",
                    GetElementLocation(attribute.Parent),
                    SiteTemplateValidationSeverity.Warning));
            }
        }

        private static string GetElementLocation(XElement element)
        {
            return element == null ? "ProvisioningTemplate" : element.Name.LocalName;
        }

        private static void ValidateRequiredResourcePaths(ProvisioningTemplate template, List<SiteTemplateValidationIssue> issues)
        {
            foreach (var file in template.Files.Where(file => string.IsNullOrWhiteSpace(file.Src)))
            {
                issues.Add(CreateIssue("MissingFileSource", "A file does not define a source path.", "Files"));
            }

            foreach (var localization in template.Localizations.Where(localization => string.IsNullOrWhiteSpace(localization.ResourceFile)))
            {
                issues.Add(CreateIssue("MissingLocalizationResourceFile", "A localization does not define a resource file.", "Localizations"));
            }

            foreach (var directory in template.Directories.Where(directory => string.IsNullOrWhiteSpace(directory.Src)))
            {
                issues.Add(CreateIssue("MissingDirectorySource", "A directory does not define a source path.", "Directories"));
            }

            foreach (var list in template.Lists)
            {
                foreach (var attachment in list.DataRows.SelectMany(dataRow => dataRow.Attachments).Where(attachment => string.IsNullOrWhiteSpace(attachment.Src)))
                {
                    issues.Add(CreateIssue(
                        "MissingDataRowAttachmentSource",
                        "A list data row attachment does not define a source path.",
                        $"Lists[{list.Title ?? list.Url ?? "unknown"}].DataRows.Attachments"));
                }
            }

            if (template.Workflows?.WorkflowDefinitions != null)
            {
                foreach (var definition in template.Workflows.WorkflowDefinitions.Where(definition => string.IsNullOrWhiteSpace(definition.XamlPath)))
                {
                    issues.Add(CreateIssue("MissingWorkflowXamlPath", "A workflow definition does not define a XAML path.", "Workflows.WorkflowDefinitions"));
                }
            }

            if (template.Publishing?.DesignPackage != null && string.IsNullOrWhiteSpace(template.Publishing.DesignPackage.DesignPackagePath))
            {
                issues.Add(CreateIssue("MissingDesignPackagePath", "The design package does not define a source path.", "Publishing.DesignPackage"));
            }

            // The engine reads every default document when applying a content type, so an empty path fails there.
            foreach (var contentType in template.ContentTypes.Where(contentType => contentType.DocumentSetTemplate != null))
            {
                foreach (var defaultDocument in contentType.DocumentSetTemplate.DefaultDocuments.Where(defaultDocument => string.IsNullOrWhiteSpace(defaultDocument.FileSourcePath)))
                {
                    issues.Add(CreateIssue(
                        "MissingDefaultDocumentSource",
                        "A document set default document does not define a source path.",
                        $"ContentTypes[{contentType.Name ?? contentType.Id}].DocumentSetTemplate.DefaultDocuments[{defaultDocument.Name ?? "unknown"}]"));
                }
            }

            if (template.Tenant?.AppCatalog?.Packages != null)
            {
                foreach (var package in template.Tenant.AppCatalog.Packages.Where(package => string.IsNullOrWhiteSpace(package.Src)))
                {
                    issues.Add(CreateIssue("MissingAppPackageSource", "An app catalog package does not define a source path.", "Tenant.AppCatalog.Packages"));
                }
            }

            if (template.Tenant?.SiteScripts != null)
            {
                foreach (var siteScript in template.Tenant.SiteScripts.Where(siteScript => string.IsNullOrWhiteSpace(siteScript.JsonFilePath)))
                {
                    issues.Add(CreateIssue("MissingSiteScriptFile", "A site script does not define a JSON file path.", "Tenant.SiteScripts"));
                }
            }
        }

        private static void ValidateResources(ProvisioningTemplate template, List<SiteTemplateValidationIssue> issues)
        {
            if (template.Connector == null)
            {
                return;
            }

            foreach (var file in template.Files.Where(file => IsCheckableResourcePath(file.Src)))
            {
                ValidateResource(() => FrameworkFileUtilities.GetFileStream(template, file), file.Src, $"Files[{file.Src}]", issues);
            }

            foreach (var localization in template.Localizations.Where(localization => IsCheckableResourcePath(localization.ResourceFile)))
            {
                ValidateResource(
                    () => FrameworkFileUtilities.GetFileStream(template, localization.ResourceFile),
                    localization.ResourceFile,
                    $"Localizations[{localization.ResourceFile}]",
                    issues,
                    SiteTemplateValidationSeverity.Warning);
            }

            foreach (var directory in template.Directories.Where(directory => IsCheckableResourcePath(directory.Src)))
            {
                ValidateDirectory(template, directory, issues);
                if (IsCheckableResourcePath(directory.MetadataMappingFile))
                {
                    ValidateResource(
                        () => FrameworkFileUtilities.GetFileStream(template, directory.MetadataMappingFile),
                        directory.MetadataMappingFile,
                        $"Directories[{directory.Src}].MetadataMappingFile",
                        issues,
                        SiteTemplateValidationSeverity.Warning);
                }
            }

            foreach (var list in template.Lists)
            {
                foreach (var attachment in list.DataRows.SelectMany(dataRow => dataRow.Attachments).Where(attachment => IsCheckableResourcePath(attachment.Src)))
                {
                    ValidateResource(
                        () => FrameworkFileUtilities.GetFileStream(template, attachment.Src),
                        attachment.Src,
                        $"Lists[{list.Title ?? list.Url ?? "unknown"}].DataRows.Attachments[{attachment.Name ?? attachment.Src}]",
                        issues);
                }
            }

            // The engine only reads the logo from the connector for group sites, and never for the group image endpoint.
            if (template.WebSettings != null &&
                IsConnectorRelativePath(template.WebSettings.SiteLogo) &&
                !template.WebSettings.SiteLogo.Contains("_api/groupservice/getgroupimage", StringComparison.OrdinalIgnoreCase))
            {
                ValidateResource(
                    () => FrameworkFileUtilities.GetFileStream(template, template.WebSettings.SiteLogo),
                    template.WebSettings.SiteLogo,
                    "WebSettings.SiteLogo",
                    issues,
                    SiteTemplateValidationSeverity.Warning);
            }

            if (template.Workflows?.WorkflowDefinitions != null)
            {
                foreach (var definition in template.Workflows.WorkflowDefinitions.Where(definition => IsCheckableResourcePath(definition.XamlPath)))
                {
                    ValidateResource(
                        () => FrameworkFileUtilities.GetFileStream(template, definition.XamlPath),
                        definition.XamlPath,
                        $"Workflows.WorkflowDefinitions[{definition.DisplayName ?? definition.XamlPath}]",
                        issues);
                }
            }

            if (template.Publishing?.DesignPackage != null && IsCheckableResourcePath(template.Publishing.DesignPackage.DesignPackagePath))
            {
                ValidateResource(
                    () => FrameworkFileUtilities.GetFileStream(template, template.Publishing.DesignPackage.DesignPackagePath),
                    template.Publishing.DesignPackage.DesignPackagePath,
                    "Publishing.DesignPackage",
                    issues);
            }

            foreach (var contentType in template.ContentTypes.Where(contentType => contentType.DocumentSetTemplate != null))
            {
                foreach (var defaultDocument in contentType.DocumentSetTemplate.DefaultDocuments.Where(defaultDocument => IsCheckableResourcePath(defaultDocument.FileSourcePath)))
                {
                    ValidateResource(
                        () => FrameworkFileUtilities.GetFileStream(template, defaultDocument.FileSourcePath),
                        defaultDocument.FileSourcePath,
                        $"ContentTypes[{contentType.Name ?? contentType.Id}].DocumentSetTemplate.DefaultDocuments[{defaultDocument.Name ?? defaultDocument.FileSourcePath}]",
                        issues);
                }
            }

            if (template.Tenant?.AppCatalog?.Packages != null)
            {
                foreach (var package in template.Tenant.AppCatalog.Packages.Where(package => IsCheckableResourcePath(package.Src)))
                {
                    ValidateResource(() => FrameworkFileUtilities.GetFileStream(template, package.Src), package.Src, $"Tenant.AppCatalog.Packages[{package.Src}]", issues);
                }
            }

            if (template.Tenant?.SiteScripts != null)
            {
                foreach (var siteScript in template.Tenant.SiteScripts.Where(siteScript => IsCheckableResourcePath(siteScript.JsonFilePath)))
                {
                    ValidateResource(() => FrameworkFileUtilities.GetFileStream(template, siteScript.JsonFilePath), siteScript.JsonFilePath, $"Tenant.SiteScripts[{siteScript.JsonFilePath}]", issues);
                }
            }
        }

        private static void ValidateDirectory(ProvisioningTemplate template, PnP.Framework.Provisioning.Model.Directory directory, List<SiteTemplateValidationIssue> issues)
        {
            var hasResource = GetResourcePathCandidates(directory.Src).Any(source =>
            {
                try
                {
                    return template.Connector.GetFiles(source)?.Any() == true ||
                        directory.Recursive && template.Connector.GetFolders(source)?.Any() == true;
                }
                catch (DirectoryNotFoundException)
                {
                    return false;
                }
            });

            if (!hasResource)
            {
                issues.Add(CreateIssue(
                    "EmptyOrMissingDirectoryResource",
                    $"Directory resource '{directory.Src}' is empty or could not be found.",
                    $"Directories[{directory.Src}]",
                    SiteTemplateValidationSeverity.Warning));
            }
        }

        // URLs are resolved when the template is applied; anything else could name a file the template carries.
        private static bool IsCheckableResourcePath(string source)
        {
            return !string.IsNullOrWhiteSpace(source) &&
                !source.StartsWith('/') &&
                !Uri.TryCreate(source, UriKind.Absolute, out _);
        }

        // The site logo is frequently a URL or a token rather than a packaged file, so it is only checked when it is neither.
        private static bool IsConnectorRelativePath(string source)
        {
            return IsCheckableResourcePath(source) && !IsTokenizedPath(source);
        }

        // Path tokens lead the value or name a parameter; a brace elsewhere is part of a literal folder or file name.
        private static bool IsTokenizedPath(string source)
        {
            return source.StartsWith('{') || source.Contains("{parameter:", StringComparison.OrdinalIgnoreCase);
        }

        private static IEnumerable<string> GetResourcePathCandidates(string source)
        {
            var decodedSource = Uri.UnescapeDataString(source);
            return new[]
            {
                source,
                decodedSource,
                decodedSource.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar),
                decodedSource.Replace('\\', '/'),
                decodedSource.Replace('/', '\\')
            }.Distinct(StringComparer.OrdinalIgnoreCase);
        }

        private static void ValidateResource(
            Func<Stream> openResource,
            string source,
            string location,
            List<SiteTemplateValidationIssue> issues,
            SiteTemplateValidationSeverity severity = SiteTemplateValidationSeverity.Error)
        {
            // A token can only be expanded against a live site, so an unresolved one is reported as unverified rather than missing.
            void AddUnresolvedIssue(string code, string message)
            {
                issues.Add(IsTokenizedPath(source)
                    ? CreateIssue(
                        "UnverifiedResourcePath",
                        $"Referenced resource '{source}' contains a token, so it could only be checked once the template is applied.",
                        location,
                        SiteTemplateValidationSeverity.Information)
                    : CreateIssue(code, message, location, severity));
            }

            try
            {
                using var stream = openResource();
                if (stream == null)
                {
                    AddUnresolvedIssue("MissingResource", $"Referenced resource '{source}' could not be found.");
                }
            }
            catch (FileNotFoundException)
            {
                AddUnresolvedIssue("MissingResource", $"Referenced resource '{source}' could not be found.");
            }
            catch (DirectoryNotFoundException)
            {
                AddUnresolvedIssue("MissingResource", $"Referenced resource '{source}' could not be found.");
            }
            catch (ArgumentException)
            {
                AddUnresolvedIssue("InvalidResourcePath", $"Referenced resource '{source}' does not resolve to a file name.");
            }
            catch (UnauthorizedAccessException)
            {
                AddUnresolvedIssue("InvalidResourcePath", $"Referenced resource '{source}' could not be read as a file.");
            }
            catch (IOException exception)
            {
                AddUnresolvedIssue("InvalidResourcePath", $"Referenced resource '{source}' could not be read. {exception.Message}");
            }
        }

        private static void AddDuplicateIssues<TItem, TKey>(
            IEnumerable<TItem> items,
            Func<TItem, TKey> keySelector,
            Func<TKey, SiteTemplateValidationIssue> issueFactory,
            List<SiteTemplateValidationIssue> issues,
            IEqualityComparer<TKey> comparer = null)
        {
            foreach (var group in items.GroupBy(keySelector, comparer ?? EqualityComparer<TKey>.Default).Where(group => group.Count() > 1))
            {
                issues.Add(issueFactory(group.Key));
            }
        }

        internal static SiteTemplateValidationIssue CreateIssue(
            string code,
            string message,
            string location,
            SiteTemplateValidationSeverity severity = SiteTemplateValidationSeverity.Error)
        {
            return new SiteTemplateValidationIssue
            {
                Code = code,
                Severity = severity,
                Message = message,
                Location = location
            };
        }
    }
}