using System;
using System.IO;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers.Xml;
using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.ObjectHandlers;
using System.Collections;
using System.Linq;
using PnP.Framework.Provisioning.Providers;
using System.Collections.Generic;
using PnP.PowerShell.Commands.Utilities;
using System.Net;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPSiteTemplate")]
    public class InvokeSiteTemplate : PnPSharePointCmdlet
    {
        private ProgressRecord progressRecord = new ProgressRecord(0, "Activity", "Status");
        private ProgressRecord subProgressRecord = new ProgressRecord(1, "Activity", "Status");

        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ParameterSetName = "Path")]
        [Parameter(Mandatory = false, ParameterSetName = "Instance")]
        public string Path;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string TemplateId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string ResourceFolder;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter OverwriteSystemPropertyBagValues;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter IgnoreDuplicateDataRowErrors;

        [Parameter(Mandatory = false)]
        public SwitchParameter ProvisionContentTypesToSubWebs;

        [Parameter(Mandatory = false)]
        public SwitchParameter ProvisionFieldsToSubWebs;

        [Parameter(Mandatory = false)]
        public SwitchParameter ClearNavigation;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public Hashtable Parameters;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public Handlers Handlers;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public Handlers ExcludeHandlers;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ExtensibilityHandler[] ExtensibilityHandlers;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ITemplateProviderExtension[] TemplateProviderExtensions;

        [Parameter(Mandatory = false, ParameterSetName = "Instance")]
        public ProvisioningTemplate InputInstance;
        
        [Parameter(Mandatory = false, ParameterSetName = "Stream")]
        public MemoryStream Stream { get; set; }
        
        [Parameter(Mandatory = false)]
        [Alias("Url")]
        public string Identity { get; set; }

        protected override void ExecuteCmdlet()
        {
            ClientContext applyTemplateContext = null;

            // If the Identity or Url parameter has been specified, we will build a context to apply the template to that specific site collection
            if (ParameterSpecified(nameof(Identity)))
            {
                // Validate if the Identity/Url parameter is a valid full URL
                if (Uri.TryCreate(Identity, UriKind.Absolute, out Uri uri))
                {
                    LogDebug($"Connecting to the SharePoint Online site at '{uri}' to apply the template to");
                    try
                    {
                        applyTemplateContext = Connection.CloneContext(Identity);
                    }
                    catch (WebException e) when (e.Status == WebExceptionStatus.NameResolutionFailure)
                    {
                        throw new PSInvalidOperationException($"The hostname '{uri}' which you have provided to apply the template to is invalid and does not exist.", e);
                    }
                    catch (Exception e)
                    {
                        throw new PSInvalidOperationException($"Unable to connect to the SharePoint Online Admin site at '{uri}' to run apply the template to. Error message: {e.Message}", e);
                    }
                    LogDebug($"Connected to the SharePoint Online site at '{uri}' to apply the template");
                }
                else
                {
                    throw new ArgumentException("The Identity parameter, when provided, must be a valid full URL to the site collection to apply the template to.", nameof(Identity));
                }
            }
            else
            {
                // If the Identity/Url parameter has not been specified, we will use the current context to apply the template to
                applyTemplateContext = ClientContext;
            }

           
            // Avoid the template being applied to a tenant admin site
            if (PnPConnection.IsTenantAdminSite(applyTemplateContext))
            {
                // If the current context is a tenant admin site, we cannot apply a site template to it
                throw new PSInvalidOperationException($"You cannot apply a site template to a tenant admin site. Please connect to a site collection or use the {nameof(Identity)} parameter to specify which sitecollection it should be applied to.");
            }

            applyTemplateContext.Web.EnsureProperty(w => w.Url);
            ProvisioningTemplate provisioningTemplate;

            FileConnectorBase fileConnector;
            if (ParameterSpecified(nameof(Path)))
            {
                bool templateFromFileSystem = !Path.ToLower().StartsWith("http");
                string templateFileName = System.IO.Path.GetFileName(Path);
                if (templateFromFileSystem)
                {
                    if (!System.IO.Path.IsPathRooted(Path))
                    {
                        Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                    }
                    if (!System.IO.File.Exists(Path))
                    {
                        throw new FileNotFoundException($"File not found");
                    }
                    if (!string.IsNullOrEmpty(ResourceFolder))
                    {
                        if (!System.IO.Path.IsPathRooted(ResourceFolder))
                        {
                            ResourceFolder = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path,
                                ResourceFolder);
                        }
                    }
                    var fileInfo = new FileInfo(Path);
                    fileConnector = new FileSystemConnector(fileInfo.DirectoryName, "");
                }
                else
                {
                    Uri fileUri = new(Path);
                    var webUrl = Web.WebUrlFromFolderUrlDirect(ClientContext, fileUri);
                    var templateContext = ClientContext.Clone(webUrl.ToString());

                    var library = Path.ToLower().Replace(templateContext.Url.ToLower(), "").TrimStart('/');
                    var idx = library.IndexOf("/", StringComparison.Ordinal);
                    library = library.Substring(0, idx);

                    // This syntax creates a SharePoint connector regardless we have the -InputInstance argument or not
                    fileConnector = new SharePointConnector(templateContext, templateContext.Url, library);
                }

                // If we don't have the -InputInstance parameter, we load the template from the source connector

                Stream stream = fileConnector.GetFileStream(templateFileName);
                var isOpenOfficeFile = FileUtilities.IsOpenOfficeFile(stream);
                XMLTemplateProvider provider;
                if (isOpenOfficeFile)
                {
                    var openXmlConnector = new OpenXMLConnector(templateFileName, fileConnector);
                    provider = new XMLOpenXMLTemplateProvider(openXmlConnector);
                    if (!string.IsNullOrEmpty(openXmlConnector.Info?.Properties?.TemplateFileName))
                    {
                        templateFileName = openXmlConnector.Info.Properties.TemplateFileName;
                    }
                    else
                    {
                        templateFileName = templateFileName.Substring(0, templateFileName.LastIndexOf(".", StringComparison.Ordinal)) + ".xml";
                    }
                }
                else
                {
                    if (templateFromFileSystem)
                    {
                        provider = new XMLFileSystemTemplateProvider(fileConnector.Parameters[FileConnectorBase.CONNECTIONSTRING] + "", "");
                    }
                    else
                    {
                        throw new NotSupportedException("Only .pnp package files are supported from a SharePoint library");
                    }
                }

                if (ParameterSpecified(nameof(TemplateId)))
                {
                    provisioningTemplate = provider.GetTemplate(templateFileName, TemplateId, null, TemplateProviderExtensions);
                }
                else
                {
                    provisioningTemplate = provider.GetTemplate(templateFileName, TemplateProviderExtensions);
                }

                if (provisioningTemplate == null)
                {
                    // If we don't have the template, raise an error and exit
                    LogError("The -Path parameter targets an invalid repository or template object.");
                    return;
                }

                if (isOpenOfficeFile)
                {
                    provisioningTemplate.Connector = provider.Connector;
                }
                else
                {
                    if (ResourceFolder != null)
                    {
                        var fileSystemConnector = new FileSystemConnector(ResourceFolder, "");
                        provisioningTemplate.Connector = fileSystemConnector;
                    }
                    else
                    {
                        provisioningTemplate.Connector = provider.Connector;
                    }
                }
            }
            else
            {
                if (InputInstance == null && Stream != null)
                {
                    LogDebug("Determining if template from provided stream is a .pnp package file");
                    Stream.Position = 0;

                    var isOpenOfficeFile = FileUtilities.IsOpenOfficeFile(Stream);
                    if (isOpenOfficeFile)
                    {
                        LogDebug("Package is a .pnp package file, loading template from provided stream");
                        var openXmlConnector = new OpenXMLConnector(Stream);
                        var provider = new XMLOpenXMLTemplateProvider(openXmlConnector);

                        var templates = ProvisioningHelper.LoadSiteTemplatesFromStream(Stream, TemplateProviderExtensions, LogError);

                        LogDebug($"Package stream contains {templates.Count} template{(templates.Count != 1 ? "s" : "")}");

                        if (ParameterSpecified(nameof(TemplateId)))
                        {
                            // If we have the -TemplateId parameter, we look for the template with that ID in the package stream
                            LogDebug($"Looking for template with ID {TemplateId} in package stream");
                            provisioningTemplate = templates.FirstOrDefault(t => t.Id == TemplateId);
                        }
                        else
                        {
                            // If we don't have the -TemplateId parameter, we use the last template in the package stream
                            LogDebug($"No template ID specified, using the last template in package stream");
                            provisioningTemplate = templates.LastOrDefault();
                        }
                        if (provisioningTemplate == null)
                        {
                            // If we don't have a template, raise an error and exit
                            LogError("Unable to find template in provided stream. Please check the template ID or the content of the stream.");
                            return;
                        }
                        provisioningTemplate.Connector = provider.Connector;
                    }
                    else
                    {
                        // XML template files have not been implemented to be used from a stream, only .pnp files
                        throw new NotSupportedException("Only .pnp package files are supported in a stream");
                    }
                }
                else
                {
                    provisioningTemplate = InputInstance;

                    if (ResourceFolder != null)
                    {
                        var fileSystemConnector = new FileSystemConnector(ResourceFolder, "");
                        provisioningTemplate.Connector = fileSystemConnector;
                    }
                    else
                    {
                        if (Path != null)
                        {
                            if (!System.IO.Path.IsPathRooted(Path))
                            {
                                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                            }

                            var fileInfo = new FileInfo(Path);
                            fileConnector = new FileSystemConnector(System.IO.Path.IsPathRooted(fileInfo.FullName) ? fileInfo.FullName : fileInfo.DirectoryName, "");
                            provisioningTemplate.Connector = fileConnector;
                        }
                        else
                        {
                            Path = SessionState.Path.CurrentFileSystemLocation.Path;
                        }
                    }
                }
            }

            if (Parameters != null)
            {
                foreach (var parameter in Parameters.Keys)
                {
                    if (provisioningTemplate.Parameters.ContainsKey(parameter.ToString()))
                    {
                        provisioningTemplate.Parameters[parameter.ToString()] = Parameters[parameter].ToString();
                    }
                    else
                    {
                        provisioningTemplate.Parameters.Add(parameter.ToString(), Parameters[parameter].ToString());
                    }
                }
            }

            var applyingInformation = new ProvisioningTemplateApplyingInformation();

            if (ParameterSpecified(nameof(Handlers)))
            {
                applyingInformation.HandlersToProcess = Handlers;
            }
            if (ParameterSpecified(nameof(ExcludeHandlers)))
            {
                foreach (var handler in (Handlers[])Enum.GetValues(typeof(Handlers)))
                {
                    if (!ExcludeHandlers.Has(handler) && handler != Handlers.All)
                    {
                        Handlers |= handler;
                    }
                }
                applyingInformation.HandlersToProcess = Handlers;
            }

            if (ExtensibilityHandlers != null)
            {
                applyingInformation.ExtensibilityHandlers = ExtensibilityHandlers.ToList();
            }

            applyingInformation.ProgressDelegate = (message, step, total) =>
            {
                if (message != null)
                {
                    var percentage = Convert.ToInt32(100 / Convert.ToDouble(total) * Convert.ToDouble(step));
                    progressRecord.Activity = $"Applying template to {applyTemplateContext.Url}";
                    progressRecord.StatusDescription = message;
                    progressRecord.PercentComplete = percentage;
                    progressRecord.RecordType = ProgressRecordType.Processing;
                    WriteProgress(progressRecord);
                }
            };

            var warningsShown = new List<string>();

            applyingInformation.MessagesDelegate = (message, type) =>
            {
                switch (type)
                {
                    case ProvisioningMessageType.Warning:
                        {
                            if (!warningsShown.Contains(message))
                            {
                                LogWarning(message);
                                warningsShown.Add(message);
                            }
                            break;
                        }
                    case ProvisioningMessageType.Progress:
                        {
                            if (message != null)
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
                                        subProgressRecord.Activity = string.IsNullOrEmpty(messageSplitted[0]) ? "-" : messageSplitted[0];
                                        subProgressRecord.StatusDescription = string.IsNullOrEmpty(messageSplitted[1]) ? "-" : messageSplitted[1];
                                        subProgressRecord.PercentComplete = Convert.ToInt32(100 / total * current);
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

            applyingInformation.OverwriteSystemPropertyBagValues = OverwriteSystemPropertyBagValues;
            applyingInformation.IgnoreDuplicateDataRowErrors = IgnoreDuplicateDataRowErrors;
            applyingInformation.ClearNavigation = ClearNavigation;
            applyingInformation.ProvisionContentTypesToSubWebs = ProvisionContentTypesToSubWebs;
            applyingInformation.ProvisionFieldsToSubWebs = ProvisionFieldsToSubWebs;

            using (var provisioningContext = new PnPProvisioningContext(async (resource, scope) =>
            {
                return await TokenRetrieval.GetAccessTokenAsync(resource, scope, Connection);
            }, azureEnvironment: Connection.AzureEnvironment))
            {
                applyTemplateContext.Web.ApplyProvisioningTemplate(provisioningTemplate, applyingInformation);
            }

            WriteProgress(new ProgressRecord(0, $"Applying template to {applyTemplateContext.Url}", " ") { RecordType = ProgressRecordType.Completed });

            if (Stream != null)
            {
                // Reset the stream position to 0 so it can be used again if needed
                Stream.Position = 0;
            }
        }
    }
}
