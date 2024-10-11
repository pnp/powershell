using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.ObjectHandlers;
using PnP.Framework.Provisioning.Providers;
using PnP.Framework.Provisioning.Providers.Xml;
using File = System.IO.File;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using System.Collections;
using PnP.PowerShell.Commands.Utilities;
using System.Collections.Generic;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.Framework.Provisioning.Model.Configuration;
using PnP.PowerShell.Commands.Base;
using System.Threading.Tasks;
using PnP.Framework.Provisioning.Providers.Markdown;


namespace PnP.PowerShell.Commands.Provisioning.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteTemplate")]
    public class GetSiteTemplate : PnPWebCmdlet
    {
        //private readonly ProgressRecord mainProgressRecord = new ProgressRecord(0, "Processing", "Status");
        private readonly ProgressRecord subProgressRecord = new ProgressRecord(1, "Activity", "Status");

        [Parameter(Mandatory = false, Position = 0)]
        public string Out;

        [Parameter(Mandatory = false, Position = 1)]
        public XMLPnPSchemaVersion Schema = XMLPnPSchemaVersion.LATEST;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeAllTermGroups;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeSiteCollectionTermGroup;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeSiteGroups;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeTermGroupsSecurity;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeSearchConfiguration;

        [Parameter(Mandatory = false)]
        public SwitchParameter PersistBrandingFiles;

        [Parameter(Mandatory = false)]
        public SwitchParameter PersistPublishingFiles;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeNativePublishingFiles;

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeHiddenLists;

        [Parameter(Mandatory = false)]
        [Alias("IncludeAllClientSidePages")]
        public SwitchParameter IncludeAllPages;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipVersionCheck;

        [Parameter(Mandatory = false)]
        public SwitchParameter PersistMultiLanguageResources;

        [Parameter(Mandatory = false)]
        public string ResourceFilePrefix;

        [Parameter(Mandatory = false)]
        public Handlers Handlers;

        [Parameter(Mandatory = false)]
        public Handlers ExcludeHandlers;

        [Parameter(Mandatory = false)]
        public ExtensibilityHandler[] ExtensibilityHandlers;

        [Parameter(Mandatory = false)]
        public ITemplateProviderExtension[] TemplateProviderExtensions;

        [Parameter(Mandatory = false)]
        public string[] ContentTypeGroups;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public System.Text.Encoding Encoding = System.Text.Encoding.Unicode;

        [Parameter(Mandatory = false)]
        public string TemplateDisplayName;

        [Parameter(Mandatory = false)]
        public string TemplateImagePreviewUrl;

        [Parameter(Mandatory = false)]
        public Hashtable TemplateProperties;

        [Parameter(Mandatory = false)]
        public SwitchParameter OutputInstance;

        [Parameter(Mandatory = false)]
        public SwitchParameter ExcludeContentTypesFromSyndication;

        [Parameter(Mandatory = false)]
        public List<string> ListsToExtract;

        [Parameter(Mandatory = false)]
        public ExtractConfigurationPipeBind Configuration;

        protected override void ExecuteCmdlet()
        {
            ExtractConfiguration extractConfiguration = null;
            if (ParameterSpecified(nameof(Configuration)))
            {
                extractConfiguration = Configuration.GetConfiguration(SessionState.Path.CurrentFileSystemLocation.Path);
            }
            if (PersistMultiLanguageResources == false && ResourceFilePrefix != null)
            {
                WriteWarning("In order to export resource files, also specify the PersistMultiLanguageResources switch");
            }
            if (!string.IsNullOrEmpty(Out))
            {
                if (!Path.IsPathRooted(Out))
                {
                    Out = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Out);
                }
                if (File.Exists(Out))
                {
                    if (Force || ShouldContinue(string.Format(Resources.File0ExistsOverwrite, Out), Resources.Confirm))
                    {
                        ExtractTemplate(Schema, new FileInfo(Out).DirectoryName, new FileInfo(Out).Name, extractConfiguration);
                    }
                }
                else
                {
                    ExtractTemplate(Schema, new FileInfo(Out).DirectoryName, new FileInfo(Out).Name, extractConfiguration);
                }
            }
            else
            {
                ExtractTemplate(Schema, SessionState.Path.CurrentFileSystemLocation.Path, null, extractConfiguration);
            }
        }

        private void ExtractTemplate(XMLPnPSchemaVersion schema, string path, string packageName, ExtractConfiguration configuration)
        {
            CurrentWeb.EnsureProperty(w => w.Url);
            ProvisioningTemplateCreationInformation creationInformation = null;
            if (configuration != null)
            {
                creationInformation = configuration.ToCreationInformation(CurrentWeb);
            }
            else
            {
                creationInformation = new ProvisioningTemplateCreationInformation(CurrentWeb);
            }

            if (ParameterSpecified(nameof(Handlers)))
            {
                creationInformation.HandlersToProcess = Handlers;
            }
            if (ParameterSpecified(nameof(ExcludeHandlers)))
            {
                foreach (var handler in (Handlers[])Enum.GetValues(typeof(Handlers)))
                {
                    if (!ExcludeHandlers.Has(handler) && handler != Handlers.All)
                    {
                        Handlers = Handlers | handler;
                    }
                }
                creationInformation.HandlersToProcess = Handlers;
            }

            var extension = "";
            if (packageName != null)
            {
                if (packageName.IndexOf(".", StringComparison.Ordinal) > -1)
                {
                    extension = packageName.Substring(packageName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
                }
                else
                {
                    packageName += ".pnp";
                    extension = ".pnp";
                }
            }

            var fileSystemConnector = new FileSystemConnector(path, "");
            if (extension == ".pnp")
            {
                creationInformation.FileConnector = new OpenXMLConnector(packageName, fileSystemConnector);
            }
            else if (extension == ".md")
            {
                creationInformation.FileConnector = fileSystemConnector;
            }
            else
            {
                creationInformation.FileConnector = fileSystemConnector;
            }
#pragma warning disable 618
            if (ParameterSpecified(nameof(PersistBrandingFiles)))
            {
                creationInformation.PersistBrandingFiles = PersistBrandingFiles;
            }
#pragma warning restore 618
            if (ParameterSpecified(nameof(PersistPublishingFiles)))
            {
                creationInformation.PersistPublishingFiles = PersistPublishingFiles;
            }
            if (ParameterSpecified(nameof(IncludeNativePublishingFiles)))
            {
                creationInformation.IncludeNativePublishingFiles = IncludeNativePublishingFiles;
            }
            if (ParameterSpecified(nameof(IncludeSiteGroups)))
            {
                creationInformation.IncludeSiteGroups = IncludeSiteGroups;
            }
            if (ParameterSpecified(nameof(IncludeTermGroupsSecurity)))
            {
                creationInformation.IncludeTermGroupsSecurity = IncludeTermGroupsSecurity;
            }
            if (ParameterSpecified(nameof(IncludeSearchConfiguration)))
            {
                creationInformation.IncludeSearchConfiguration = IncludeSearchConfiguration;
            }
            if (ParameterSpecified(nameof(IncludeHiddenLists)))
            {
                creationInformation.IncludeHiddenLists = IncludeHiddenLists;
            }
            if (ParameterSpecified(nameof(IncludeAllPages)))
            {
                creationInformation.IncludeAllClientSidePages = IncludeAllPages;
            }
            if (ParameterSpecified(nameof(SkipVersionCheck)))
            {
                creationInformation.SkipVersionCheck = SkipVersionCheck;
            }
            if (ParameterSpecified(nameof(ContentTypeGroups)) && ContentTypeGroups != null)
            {
                creationInformation.ContentTypeGroupsToInclude = ContentTypeGroups.ToList();
            }
            if (ParameterSpecified(nameof(PersistMultiLanguageResources)))
            {
                creationInformation.PersistMultiLanguageResources = PersistMultiLanguageResources;
            }
            if (extension == ".pnp")
            {
                // if file is of pnp format, persist all files
                creationInformation.PersistBrandingFiles = true;
                creationInformation.PersistPublishingFiles = true;
                creationInformation.PersistMultiLanguageResources = true;
            }
            if (!string.IsNullOrEmpty(ResourceFilePrefix))
            {
                creationInformation.ResourceFilePrefix = ResourceFilePrefix;
            }
            else
            {
                if (Out != null)
                {
                    FileInfo fileInfo = new FileInfo(Out);
                    var prefix = fileInfo.Name;
                    // strip extension, if there is any
                    var indexOfLastDot = prefix.LastIndexOf(".", StringComparison.Ordinal);
                    if (indexOfLastDot > -1)
                    {
                        prefix = prefix.Substring(0, indexOfLastDot);
                    }
                    creationInformation.ResourceFilePrefix = prefix;
                }

            }
            if (ExtensibilityHandlers != null)
            {
                creationInformation.ExtensibilityHandlers = ExtensibilityHandlers.ToList();
            }

            creationInformation.BaseTemplate = CurrentWeb.GetBaseTemplate();

            creationInformation.ProgressDelegate = (message, step, total) =>
            {
                var percentage = Convert.ToInt32((100 / Convert.ToDouble(total)) * Convert.ToDouble(step));

                WriteProgress(new ProgressRecord(0, $"Extracting Template from {CurrentWeb.Url}", message) { PercentComplete = percentage });
                WriteProgress(new ProgressRecord(1, " ", " ") { RecordType = ProgressRecordType.Completed });
            };
            creationInformation.MessagesDelegate = (message, type) =>
            {
                switch (type)
                {
                    case ProvisioningMessageType.Warning:
                        {
                            WriteWarning(message);
                            break;
                        }
                    case ProvisioningMessageType.Progress:
                        {
                            var activity = message;
                            if (message.IndexOf("|") > -1)
                            {
                                var messageSplitted = message.Split('|');
                                if (messageSplitted.Length == 4)
                                {
                                    var current = double.Parse(messageSplitted[2]);
                                    var total = double.Parse(messageSplitted[3]);
                                    subProgressRecord.RecordType = ProgressRecordType.Processing;
                                    subProgressRecord.Activity = messageSplitted[0];
                                    subProgressRecord.StatusDescription = messageSplitted[1];
                                    subProgressRecord.PercentComplete = Convert.ToInt32((100 / total) * current);
                                    WriteProgress(subProgressRecord);
                                }
                                else
                                {
                                    subProgressRecord.Activity = "Processing";
                                    subProgressRecord.RecordType = ProgressRecordType.Processing;
                                    subProgressRecord.StatusDescription = activity;
                                    subProgressRecord.PercentComplete = 0;
                                    WriteProgress(subProgressRecord);
                                }
                            }
                            else
                            {
                                subProgressRecord.Activity = "Processing";
                                subProgressRecord.RecordType = ProgressRecordType.Processing;
                                subProgressRecord.StatusDescription = activity;
                                subProgressRecord.PercentComplete = 0;
                                WriteProgress(subProgressRecord);
                            }
                            break;
                        }
                    case ProvisioningMessageType.Completed:
                        {
                            WriteProgress(new ProgressRecord(1, message, " ") { RecordType = ProgressRecordType.Completed });
                            break;
                        }
                }
            };

            if (IncludeAllTermGroups)
            {
                creationInformation.IncludeAllTermGroups = true;
            }
            else
            {
                if (IncludeSiteCollectionTermGroup)
                {
                    creationInformation.IncludeSiteCollectionTermGroup = true;
                }
            }

            if (ParameterSpecified(nameof(ExcludeContentTypesFromSyndication)))
            {
                creationInformation.IncludeContentTypesFromSyndication = !ExcludeContentTypesFromSyndication.ToBool();
            }

            if (ListsToExtract != null && ListsToExtract.Count > 0)
            {
                creationInformation.ListsToExtract.AddRange(ListsToExtract);
            }
            ProvisioningTemplate template = null;
            using (var provisioningContext = new PnPProvisioningContext(async (resource, scope) =>
            {
                return await TokenRetrieval.GetAccessTokenAsync(resource, scope, Connection);
            }, azureEnvironment: Connection.AzureEnvironment))
            {
                template = CurrentWeb.GetProvisioningTemplate(creationInformation);
            }
            // Set metadata for template, if any
            SetTemplateMetadata(template, TemplateDisplayName, TemplateImagePreviewUrl, TemplateProperties);

            if (!OutputInstance)
            {
                var formatter = ProvisioningHelper.GetFormatter(schema);

                if (extension == ".pnp")
                {
                    XMLTemplateProvider provider = new XMLOpenXMLTemplateProvider(
                          creationInformation.FileConnector as OpenXMLConnector);
                    var templateFileName = packageName.Substring(0, packageName.LastIndexOf(".", StringComparison.Ordinal)) + ".xml";

                    provider.SaveAs(template, templateFileName, formatter, TemplateProviderExtensions);
                }
                else if (extension == ".md")
                {
                    WriteWarning("The generation of a markdown report is work in progress, it will improve/grow with later releases.");
                    ITemplateFormatter mdFormatter = new MarkdownPnPFormatter();
                    using (var outputStream = mdFormatter.ToFormattedTemplate(template))
                    {
                        using (var fileStream = File.Create(Path.Combine(path, packageName)))
                        {
                            outputStream.Seek(0, SeekOrigin.Begin);
                            outputStream.CopyTo(fileStream);
                            fileStream.Close();
                        }
                    }
                }
                else
                {
                    if (Out != null)
                    {
                        XMLTemplateProvider provider = new XMLFileSystemTemplateProvider(path, "");
                        provider.SaveAs(template, Path.Combine(path, packageName), formatter, TemplateProviderExtensions);
                    }
                    else
                    {
                        var outputStream = formatter.ToFormattedTemplate(template);
                        var reader = new StreamReader(outputStream);

                        WriteObject(reader.ReadToEnd());
                    }
                }
            }
            else
            {
                WriteObject(template);
            }
        }

        public static void SetTemplateMetadata(ProvisioningTemplate template, string templateDisplayName, string templateImagePreviewUrl, Hashtable templateProperties)
        {
            if (!String.IsNullOrEmpty(templateDisplayName))
            {
                template.DisplayName = templateDisplayName;
            }

            if (!String.IsNullOrEmpty(templateImagePreviewUrl))
            {
                template.ImagePreviewUrl = templateImagePreviewUrl;
            }

            if (templateProperties != null && templateProperties.Count > 0)
            {
                var properties = templateProperties
                    .Cast<DictionaryEntry>()
                    .ToDictionary(i => (String)i.Key, i => (String)i.Value);

                foreach (var key in properties.Keys)
                {
                    if (!String.IsNullOrEmpty(key))
                    {
                        template.Properties[key] = properties[key];
                    }
                }
            }
        }
    }
}