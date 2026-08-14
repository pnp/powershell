using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.ObjectHandlers.Utilities;
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

        internal static SiteTemplateValidationIssue CreateSchemaIssue(Exception exception)
        {
            var location = "Schema";
            if (exception is System.Xml.Schema.XmlSchemaException schemaException && schemaException.LineNumber > 0)
            {
                location = $"Line {schemaException.LineNumber}, position {schemaException.LinePosition}";
            }

            return CreateIssue("SchemaValidationFailed", exception.Message, location);
        }

        internal static SiteTemplateValidationIssue CreateLegacySchemaIssue(string schemaNamespace)
        {
            if (string.IsNullOrWhiteSpace(schemaNamespace) ||
                schemaNamespace.Equals(XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2022_09, StringComparison.OrdinalIgnoreCase))
            {
                return null;
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

                foreach (var fieldRef in contentType.FieldRefs.Where(fieldRef => fieldRef.Id != Guid.Empty && !siteFieldIds.Contains(fieldRef.Id)))
                {
                    issues.Add(CreateIssue(
                        "UnresolvedContentTypeFieldRef",
                        $"Field reference '{fieldRef.Id}' is not defined in the template and must exist on the target site.",
                        location,
                        SiteTemplateValidationSeverity.Warning));
                }

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

                foreach (var binding in list.ContentTypeBindings.Where(binding =>
                    !string.IsNullOrWhiteSpace(binding.ContentTypeId) &&
                    !contentTypeIds.Contains(binding.ContentTypeId)))
                {
                    issues.Add(CreateIssue(
                        "UnresolvedContentTypeBinding",
                        $"Content type '{binding.ContentTypeId}' is not defined in the template and must exist on the target site.",
                        location,
                        SiteTemplateValidationSeverity.Warning));
                }

                var listFieldIds = ValidateFields(list.Fields, $"{location}.Fields", issues);

                foreach (var fieldRef in list.FieldRefs.Where(fieldRef => fieldRef.Id == Guid.Empty))
                {
                    issues.Add(CreateIssue("MissingListFieldRefId", "A list field reference does not define an ID.", location));
                }

                foreach (var fieldRef in list.FieldRefs.Where(fieldRef =>
                    fieldRef.Id != Guid.Empty &&
                    !siteFieldIds.Contains(fieldRef.Id) &&
                    !listFieldIds.Contains(fieldRef.Id)))
                {
                    issues.Add(CreateIssue(
                        "UnresolvedListFieldRef",
                        $"Field reference '{fieldRef.Id}' is not defined in the template and must exist on the target site.",
                        location,
                        SiteTemplateValidationSeverity.Warning));
                }

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
                        issues.Add(CreateIssue("InvalidFieldId", $"Field ID '{idAttribute}' is not a valid GUID.", location));
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

            ValidateTermSetDependencies(template.SiteFields, "SiteFields", template, termSetIds, issues);
            foreach (var list in template.Lists)
            {
                ValidateTermSetDependencies(list.Fields, $"Lists[{list.Title ?? list.Url ?? "unknown"}].Fields", template, termSetIds, issues);
            }
            ValidateTermSetDependency(template.Navigation?.GlobalNavigation?.ManagedNavigation?.TermSetId, "Navigation.GlobalNavigation", template, termSetIds, issues);
            ValidateTermSetDependency(template.Navigation?.CurrentNavigation?.ManagedNavigation?.TermSetId, "Navigation.CurrentNavigation", template, termSetIds, issues);

            if (!string.IsNullOrWhiteSpace(template.WebSettings?.HubSiteUrl))
            {
                issues.Add(CreateIssue(
                    "ExternalHubSiteDependency",
                    $"Hub site '{template.WebSettings.HubSiteUrl}' must exist and be accessible when the template is applied.",
                    "WebSettings.HubSiteUrl",
                    SiteTemplateValidationSeverity.Warning));
            }
        }

        private static void ValidateTermSetDependencies(
            IEnumerable<Field> fields,
            string location,
            ProvisioningTemplate template,
            HashSet<Guid> termSetIds,
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
                ValidateTermSetDependency(termSetReference, location, template, termSetIds, issues);
            }
        }

        private static void ValidateTermSetDependency(
            string termSetReference,
            string location,
            ProvisioningTemplate template,
            HashSet<Guid> termSetIds,
            List<SiteTemplateValidationIssue> issues)
        {
            if (string.IsNullOrWhiteSpace(termSetReference))
            {
                return;
            }

            if (Guid.TryParse(termSetReference, out var termSetId))
            {
                if (!termSetIds.Contains(termSetId))
                {
                    AddExternalTermSetIssue(termSetReference, location, issues);
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
                    AddExternalTermSetIssue(termSetReference, location, issues);
                }
            }
            else if (TryParseSiteCollectionTermSetToken(termSetReference, out termSetName))
            {
                var isDefined = template.TermGroups.Any(group =>
                    group.SiteCollectionTermGroup &&
                    group.TermSets.Any(termSet => string.Equals(termSet.Name, termSetName, StringComparison.OrdinalIgnoreCase)));
                if (!isDefined)
                {
                    AddExternalTermSetIssue(termSetReference, location, issues);
                }
            }
            else
            {
                AddExternalTermSetIssue(termSetReference, location, issues);
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

        private static void AddExternalTermSetIssue(string termSetReference, string location, List<SiteTemplateValidationIssue> issues)
        {
            issues.Add(CreateIssue(
                "ExternalTermSetDependency",
                $"Term set '{termSetReference}' is not defined in the template and must exist in the target term store.",
                location,
                SiteTemplateValidationSeverity.Warning));
        }

        private static void ValidateDeprecatedElements(XElement sourceElement, List<SiteTemplateValidationIssue> issues)
        {
            if (sourceElement == null)
            {
                return;
            }

            foreach (var attribute in sourceElement.DescendantsAndSelf().Attributes().Where(attribute =>
                attribute.Name.LocalName == "Private" && attribute.Parent?.Name.LocalName == "Channel"))
            {
                issues.Add(CreateIssue(
                    "DeprecatedElement",
                    "The 'Private' Teams channel attribute is deprecated. Use 'MembershipType' instead.",
                    GetElementLocation(attribute.Parent),
                    SiteTemplateValidationSeverity.Warning));
            }

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

            foreach (var file in template.Files.Where(file => !string.IsNullOrWhiteSpace(file.Src)))
            {
                ValidateResource(() => FrameworkFileUtilities.GetFileStream(template, file), file.Src, $"Files[{file.Src}]", issues);
            }

            foreach (var localization in template.Localizations.Where(localization => !string.IsNullOrWhiteSpace(localization.ResourceFile)))
            {
                ValidateResource(
                    () => FrameworkFileUtilities.GetFileStream(template, localization.ResourceFile),
                    localization.ResourceFile,
                    $"Localizations[{localization.ResourceFile}]",
                    issues,
                    SiteTemplateValidationSeverity.Warning);
            }

            foreach (var directory in template.Directories.Where(directory => !string.IsNullOrWhiteSpace(directory.Src)))
            {
                ValidateDirectory(template, directory, issues);
                if (!string.IsNullOrWhiteSpace(directory.MetadataMappingFile))
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
                foreach (var attachment in list.DataRows.SelectMany(dataRow => dataRow.Attachments).Where(attachment => !string.IsNullOrWhiteSpace(attachment.Src)))
                {
                    ValidateResource(
                        () => FrameworkFileUtilities.GetFileStream(template, attachment.Src),
                        attachment.Src,
                        $"Lists[{list.Title ?? list.Url ?? "unknown"}].DataRows.Attachments[{attachment.Name ?? attachment.Src}]",
                        issues);
                }
            }

            if (template.WebSettings != null &&
                !string.IsNullOrWhiteSpace(template.WebSettings.SiteLogo) &&
                !Uri.TryCreate(template.WebSettings.SiteLogo, UriKind.Absolute, out _))
            {
                ValidateResource(
                    () => OpenEngineResource(template, template.WebSettings.SiteLogo),
                    template.WebSettings.SiteLogo,
                    "WebSettings.SiteLogo",
                    issues,
                    SiteTemplateValidationSeverity.Warning);
            }

            if (template.Tenant?.AppCatalog?.Packages != null)
            {
                foreach (var package in template.Tenant.AppCatalog.Packages.Where(package => !string.IsNullOrWhiteSpace(package.Src)))
                {
                    ValidateResource(() => OpenEngineResource(template, package.Src), package.Src, $"Tenant.AppCatalog.Packages[{package.Src}]", issues);
                }
            }

            if (template.Tenant?.SiteScripts != null)
            {
                foreach (var siteScript in template.Tenant.SiteScripts.Where(siteScript => !string.IsNullOrWhiteSpace(siteScript.JsonFilePath)))
                {
                    ValidateResource(() => OpenEngineResource(template, siteScript.JsonFilePath), siteScript.JsonFilePath, $"Tenant.SiteScripts[{siteScript.JsonFilePath}]", issues);
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

        private static MemoryStream OpenEngineResource(ProvisioningTemplate template, string source)
        {
            return new MemoryStream(ConnectorFileHelper.GetFileBytes(template.Connector, source), writable: false);
        }

        private static void ValidateResource(
            Func<Stream> openResource,
            string source,
            string location,
            List<SiteTemplateValidationIssue> issues,
            SiteTemplateValidationSeverity severity = SiteTemplateValidationSeverity.Error)
        {
            try
            {
                using var stream = openResource();
                if (stream == null)
                {
                    issues.Add(CreateIssue("MissingResource", $"Referenced resource '{source}' could not be found.", location, severity));
                }
            }
            catch (FileNotFoundException)
            {
                issues.Add(CreateIssue("MissingResource", $"Referenced resource '{source}' could not be found.", location, severity));
            }
            catch (DirectoryNotFoundException)
            {
                issues.Add(CreateIssue("MissingResource", $"Referenced resource '{source}' could not be found.", location, severity));
            }
            catch (ArgumentException)
            {
                issues.Add(CreateIssue("MissingResource", $"Referenced resource '{source}' could not be found.", location, severity));
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