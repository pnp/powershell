using PnP.Core.Provisioning.Connectors;
using PnP.Core.Provisioning.Model;
using PnP.Core.Provisioning.Model.Configuration;
using PnP.Core.Provisioning.Providers.Xml;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Management.Automation;
using File = System.IO.File;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    public partial class GetSiteTemplate
    {
        private void ExecuteCmdletExperimental()
        {
            if (PersistMultiLanguageResources == false && ResourceFilePrefix != null)
            {
                LogWarning("In order to export resource files, also specify the PersistMultiLanguageResources switch");
            }

            var configuration = ParameterSpecified(nameof(Configuration))
                ? Configuration.GetCoreConfiguration(SessionState.Path.CurrentFileSystemLocation.Path, LogWarning)
                : new ExtractConfiguration();

            if (string.IsNullOrEmpty(Out))
            {
                ExtractTemplateExperimental(SessionState.Path.CurrentFileSystemLocation.Path, null, configuration);
                return;
            }

            if (!Path.IsPathRooted(Out))
            {
                Out = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Out);
            }
            if (File.Exists(Out) && !Force && !ShouldContinue(string.Format(Resources.File0ExistsOverwrite, Out), Resources.Confirm))
            {
                return;
            }

            ExtractTemplateExperimental(new FileInfo(Out).DirectoryName, new FileInfo(Out).Name, configuration);
        }

        private void ExtractTemplateExperimental(string path, string packageName, ExtractConfiguration configuration)
        {
            var extension = string.Empty;
            if (packageName != null)
            {
                if (packageName.IndexOf(".", StringComparison.Ordinal) > -1)
                {
                    extension = packageName.Substring(packageName.LastIndexOf(".", StringComparison.Ordinal)).ToLowerInvariant();
                }
                else
                {
                    packageName += ".pnp";
                    extension = ".pnp";
                }
            }

            if (extension == ".md")
            {
                ThrowTerminatingError(new ErrorRecord(
                    new PSNotSupportedException("The experimental PnP.Core.Provisioning engine does not write markdown reports. Extract to an .xml or .pnp file, or run without -Experimental."),
                    "MarkdownNotSupportedByExperimentalEngine",
                    ErrorCategory.NotImplemented,
                    Out));
                return;
            }

            ApplyParametersToConfiguration(configuration, path, packageName, extension);

            var reporter = new CoreProvisioningReporter($"Extracting template from {PnPContext.Uri}", WriteProgress, LogWarning);
            configuration.ProgressDelegate = reporter.ProgressDelegate;
            configuration.MessagesDelegate = reporter.MessagesDelegate;

            var template = reporter.Run(() => PnPContext.GetProvisioningManager().GetTemplateAsync(configuration));

            SetTemplateMetadataExperimental(template, TemplateDisplayName, TemplateImagePreviewUrl, TemplateProperties);

            if (OutputInstance)
            {
                WriteObject(template);
                return;
            }

            var formatter = CoreProvisioningHelper.GetFormatter(Schema);

            if (extension == ".pnp")
            {
                var provider = new XMLOpenXMLTemplateProvider(configuration.FileConnector as OpenXMLConnector);
                var templateFileName = packageName.Substring(0, packageName.LastIndexOf(".", StringComparison.Ordinal)) + ".xml";
                provider.SaveAs(template, templateFileName, formatter);
            }
            else if (Out != null)
            {
                new XMLFileSystemTemplateProvider(path, string.Empty).SaveAs(template, Path.Combine(path, packageName), formatter);
            }
            else
            {
                using var outputStream = formatter.ToFormattedTemplate(template);
                using var reader = new StreamReader(outputStream);
                WriteObject(reader.ReadToEnd());
            }
        }

        private void ApplyParametersToConfiguration(ExtractConfiguration configuration, string path, string packageName, string extension)
        {
            var fileSystemConnector = new FileSystemConnector(path, string.Empty);
            configuration.FileConnector = extension == ".pnp"
                ? new OpenXMLConnector(packageName, fileSystemConnector)
                : fileSystemConnector;

            var handlers = this.Handlers;
            if (ParameterSpecified(nameof(ExcludeHandlers)))
            {
                handlers |= CoreProvisioningHelper.InvertExcludedHandlers(this.ExcludeHandlers);
            }
            if (ParameterSpecified(nameof(Handlers)) || ParameterSpecified(nameof(ExcludeHandlers)))
            {
                configuration.Handlers = CoreProvisioningHelper.ToConfigurationHandlers(handlers);
            }

            if (ParameterSpecified(nameof(PersistBrandingFiles)))
            {
                configuration.PersistAssetFiles = PersistBrandingFiles;
            }
            if (ParameterSpecified(nameof(PersistPublishingFiles)))
            {
                configuration.Publishing.Persist = PersistPublishingFiles;
                configuration.PersistAssetFiles = configuration.PersistAssetFiles || PersistPublishingFiles;
            }
            if (ParameterSpecified(nameof(IncludeNativePublishingFiles)))
            {
                configuration.Publishing.IncludeNativePublishingFiles = IncludeNativePublishingFiles;
            }
            if (ParameterSpecified(nameof(IncludeSiteGroups)))
            {
                configuration.SiteSecurity.IncludeSiteGroups = IncludeSiteGroups;
            }
            if (ParameterSpecified(nameof(IncludeTermGroupsSecurity)))
            {
                configuration.Taxonomy.IncludeSecurity = IncludeTermGroupsSecurity;
            }
            if (ParameterSpecified(nameof(IncludeSearchConfiguration)))
            {
                configuration.SearchSettings.Include = IncludeSearchConfiguration;
            }
            if (ParameterSpecified(nameof(IncludeHiddenLists)))
            {
                configuration.Lists.IncludeHiddenLists = IncludeHiddenLists;
            }
            if (ParameterSpecified(nameof(IncludeAllPages)))
            {
                configuration.Pages.IncludeAllClientSidePages = IncludeAllPages;
            }
            if (ParameterSpecified(nameof(ContentTypeGroups)) && ContentTypeGroups != null)
            {
                configuration.ContentTypes.Groups = ContentTypeGroups.ToList();
            }
            if (ParameterSpecified(nameof(ExcludeContentTypesFromSyndication)))
            {
                configuration.ContentTypes.ExcludeFromSyndication = ExcludeContentTypesFromSyndication;
            }
            if (ParameterSpecified(nameof(PersistMultiLanguageResources)))
            {
                configuration.MultiLanguage.PersistResources = PersistMultiLanguageResources;
            }
            if (IncludeAllTermGroups)
            {
                configuration.Taxonomy.IncludeAllTermGroups = true;
            }
            else if (IncludeSiteCollectionTermGroup)
            {
                configuration.Taxonomy.IncludeSiteCollectionTermGroup = true;
            }
            if (ExtensibilityHandlers != null)
            {
                configuration.Extensibility.Handlers = CoreProvisioningHelper.ToCoreExtensibilityHandlers(ExtensibilityHandlers);
            }
            if (ListsToExtract != null && ListsToExtract.Count > 0)
            {
                foreach (var list in ListsToExtract)
                {
                    configuration.Lists.Lists.Add(new PnP.Core.Provisioning.Model.Configuration.Lists.Lists.ExtractListsListsConfiguration { Title = list });
                }
            }

            if (extension == ".pnp")
            {
                configuration.PersistAssetFiles = true;
                configuration.Publishing.Persist = true;
                configuration.MultiLanguage.PersistResources = true;
            }

            if (!string.IsNullOrEmpty(ResourceFilePrefix))
            {
                configuration.MultiLanguage.ResourceFilePrefix = ResourceFilePrefix;
            }
            else if (Out != null)
            {
                var prefix = new FileInfo(Out).Name;
                var indexOfLastDot = prefix.LastIndexOf(".", StringComparison.Ordinal);
                configuration.MultiLanguage.ResourceFilePrefix = indexOfLastDot > -1 ? prefix.Substring(0, indexOfLastDot) : prefix;
            }

            if (ParameterSpecified(nameof(SkipVersionCheck)))
            {
                LogWarning("-SkipVersionCheck has no effect with -Experimental: the PnP.Core.Provisioning engine does not stamp or check a template version.");
            }
            if (ParameterSpecified(nameof(TemplateProviderExtensions)))
            {
                LogWarning("-TemplateProviderExtensions has no effect with -Experimental: template provider extensions are not run by the PnP.Core.Provisioning engine.");
            }
        }

        private static void SetTemplateMetadataExperimental(ProvisioningTemplate template, string templateDisplayName, string templateImagePreviewUrl, Hashtable templateProperties)
        {
            if (template == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(templateDisplayName))
            {
                template.DisplayName = templateDisplayName;
            }
            if (!string.IsNullOrEmpty(templateImagePreviewUrl))
            {
                template.ImagePreviewUrl = templateImagePreviewUrl;
            }
            if (templateProperties == null)
            {
                return;
            }

            foreach (DictionaryEntry entry in templateProperties)
            {
                var key = (string)entry.Key;
                if (!string.IsNullOrEmpty(key))
                {
                    template.Properties[key] = (string)entry.Value;
                }
            }
        }
    }
}
