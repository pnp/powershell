using PnP.Core.Provisioning.Connectors;
using PnP.Core.Provisioning.Model;
using PnP.Core.Provisioning.Model.Configuration;
using PnP.Core.Provisioning.Providers.Xml;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    public partial class InvokeSiteTemplate
    {
        private void ExecuteCmdletExperimental()
        {
            var targetUri = PnPContext.Uri;
            if (ParameterSpecified(nameof(Identity)))
            {
                if (!Uri.TryCreate(Identity, UriKind.Absolute, out targetUri))
                {
                    throw new ArgumentException("The Identity parameter, when provided, must be a valid full URL to the site collection to apply the template to.", nameof(Identity));
                }
            }

            if (IsTenantAdminSite(targetUri))
            {
                throw new PSInvalidOperationException($"You cannot apply a site template to a tenant admin site. Please connect to a site collection or use the {nameof(Identity)} parameter to specify which sitecollection it should be applied to.");
            }

            var template = LoadTemplateExperimental();
            if (template == null)
            {
                LogError("The -Path parameter targets an invalid repository or template object.");
                return;
            }

            if (Parameters != null)
            {
                foreach (var parameter in Parameters.Keys)
                {
                    template.Parameters[parameter.ToString()] = Parameters[parameter].ToString();
                }
            }

            var configuration = BuildApplyConfigurationExperimental();
            var reporter = new CoreProvisioningReporter($"Applying template to {targetUri}", WriteProgress, LogWarning);
            configuration.ProgressDelegate = reporter.ProgressDelegate;
            configuration.MessagesDelegate = reporter.MessagesDelegate;

            PnPContext applyContext = null;
            try
            {
                applyContext = ParameterSpecified(nameof(Identity)) ? PnPContext.Clone(targetUri) : PnPContext;
                LogDebug($"Applying the template to the SharePoint Online site at '{targetUri}'");
                reporter.Run(() => applyContext.GetProvisioningManager().ApplyTemplateAsync(template, configuration));
            }
            finally
            {
                if (applyContext != null && !ReferenceEquals(applyContext, PnPContext))
                {
                    applyContext.Dispose();
                }
            }

            if (Stream != null)
            {
                Stream.Position = 0;
            }
        }

        private ProvisioningTemplate LoadTemplateExperimental()
        {
            if (ParameterSpecified(nameof(TemplateProviderExtensions)))
            {
                LogWarning("-TemplateProviderExtensions has no effect with -Experimental: template provider extensions are not run by the PnP.Core.Provisioning engine.");
            }

            if (ParameterSpecified(nameof(Path)))
            {
                return Path.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                    ? LoadTemplateFromSharePointExperimental()
                    : LoadTemplateFromFileExperimental();
            }

            if ((InputInstance == null || InputInstance.IsEmpty) && Stream != null)
            {
                LogDebug("Loading template from provided stream");
                var template = SelectTemplateExperimental(CoreProvisioningHelper.LoadSiteTemplatesFromStream(Stream, LogError));
                Stream.Position = 0;
                return template;
            }

            var inputTemplate = InputInstance?.GetCoreTemplate();
            if (inputTemplate != null)
            {
                inputTemplate.Connector = ResolveResourceConnectorExperimental();
            }
            return inputTemplate;
        }

        private ProvisioningTemplate LoadTemplateFromFileExperimental()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }
            if (!System.IO.File.Exists(Path))
            {
                throw new FileNotFoundException($"File not found");
            }

            var templateFileName = System.IO.Path.GetFileName(Path);
            var fileInfo = new FileInfo(Path);
            FileConnectorBase fileConnector = new FileSystemConnector(fileInfo.DirectoryName, string.Empty);

            bool isPackage;
            using (var stream = fileConnector.GetFileStream(templateFileName))
            {
                isPackage = FileUtilities.IsOpenOfficeFile(stream);
            }

            XMLTemplateProvider provider;
            if (isPackage)
            {
                var openXmlConnector = new OpenXMLConnector(templateFileName, fileConnector);
                templateFileName = !string.IsNullOrEmpty(openXmlConnector.Info?.Properties?.TemplateFileName)
                    ? openXmlConnector.Info.Properties.TemplateFileName
                    : templateFileName.Substring(0, templateFileName.LastIndexOf(".", StringComparison.Ordinal)) + ".xml";
                provider = new XMLOpenXMLTemplateProvider(openXmlConnector);
            }
            else
            {
                provider = new XMLFileSystemTemplateProvider(fileInfo.DirectoryName, string.Empty);
            }

            var template = ParameterSpecified(nameof(TemplateId))
                ? provider.GetTemplate(templateFileName, TemplateId)
                : provider.GetTemplate(templateFileName);

            if (template == null)
            {
                return null;
            }

            template.Connector = isPackage ? provider.Connector : ResolveResourceConnectorExperimental() ?? provider.Connector;
            return template;
        }

        private ProvisioningTemplate LoadTemplateFromSharePointExperimental()
        {
            var fileUri = new Uri(Path);
            LogDebug($"Reading the template from '{fileUri}'");

            byte[] contents;
            try
            {
                contents = PnPContext.Web.GetFileByServerRelativeUrl(fileUri.AbsolutePath).GetContentBytes();
            }
            catch (Exception exception)
            {
                throw new PSInvalidOperationException($"Unable to read the template at '{fileUri}' with the current connection. With -Experimental the template is read through the site you are connected to, so connect to the site holding it and try again. Error message: {exception.Message}", exception);
            }

            using var stream = new MemoryStream(contents);
            if (!FileUtilities.IsOpenOfficeFile(stream))
            {
                throw new NotSupportedException("Only .pnp package files are supported from a SharePoint library");
            }

            stream.Position = 0;
            return SelectTemplateExperimental(CoreProvisioningHelper.LoadSiteTemplatesFromStream(stream, LogError));
        }

        private ProvisioningTemplate SelectTemplateExperimental(System.Collections.Generic.List<ProvisioningTemplate> templates)
        {
            if (templates == null || templates.Count == 0)
            {
                LogError("Unable to find a template in the provided source. Please check the template ID or the content of the source.");
                return null;
            }

            if (!ParameterSpecified(nameof(TemplateId)))
            {
                LogDebug("No template ID specified, using the last template in the source");
                return templates.LastOrDefault();
            }

            LogDebug($"Looking for template with ID {TemplateId}");
            var template = templates.FirstOrDefault(t => t.Id == TemplateId);
            if (template == null)
            {
                LogError($"Unable to find a template with ID '{TemplateId}' in the provided source.");
            }
            return template;
        }

        private FileConnectorBase ResolveResourceConnectorExperimental()
        {
            if (ResourceFolder != null)
            {
                if (!System.IO.Path.IsPathRooted(ResourceFolder))
                {
                    ResourceFolder = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, ResourceFolder);
                }
                return new FileSystemConnector(ResourceFolder, string.Empty);
            }

            if (Path != null)
            {
                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }
                var fileInfo = new FileInfo(Path);
                return new FileSystemConnector(System.IO.Path.IsPathRooted(fileInfo.FullName) ? fileInfo.DirectoryName : fileInfo.FullName, string.Empty);
            }

            return new FileSystemConnector(SessionState.Path.CurrentFileSystemLocation.Path, string.Empty);
        }

        private ApplyConfiguration BuildApplyConfigurationExperimental()
        {
            var configuration = new ApplyConfiguration();

            var handlers = this.Handlers;
            if (ParameterSpecified(nameof(ExcludeHandlers)))
            {
                handlers |= CoreProvisioningHelper.InvertExcludedHandlers(this.ExcludeHandlers);
            }
            if (ParameterSpecified(nameof(Handlers)) || ParameterSpecified(nameof(ExcludeHandlers)))
            {
                configuration.Handlers = CoreProvisioningHelper.ToConfigurationHandlers(handlers);
            }

            if (ExtensibilityHandlers != null)
            {
                configuration.Extensibility.Handlers = CoreProvisioningHelper.ToCoreExtensibilityHandlers(ExtensibilityHandlers);
            }

            configuration.PropertyBag.OverwriteSystemValues = OverwriteSystemPropertyBagValues;
            configuration.Lists.IgnoreDuplicateDataRowErrors = IgnoreDuplicateDataRowErrors;
            configuration.Navigation.ClearNavigation = ClearNavigation;
            configuration.ContentTypes.ProvisionContentTypesToSubWebs = ProvisionContentTypesToSubWebs;
            configuration.Fields.ProvisionFieldsToSubWebs = ProvisionFieldsToSubWebs;

            return configuration;
        }

        private static bool IsTenantAdminSite(Uri uri)
        {
            var host = uri.Host.ToLowerInvariant();
            return host.Contains("-admin.sharepoint.", StringComparison.Ordinal);
        }
    }
}
