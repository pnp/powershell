using PnP.Core.Provisioning.Connectors;
using PnP.Core.Provisioning.Model;
using PnP.Core.Provisioning.Model.Configuration;
using PnP.Core.Provisioning.Providers;
using PnP.Core.Provisioning.Providers.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FrameworkHandlers = PnP.Framework.Provisioning.Model.Handlers;
using FrameworkSchemaVersion = PnP.Framework.Provisioning.Providers.Xml.XMLPnPSchemaVersion;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Loads, saves and converts provisioning templates with the experimental PnP.Core.Provisioning engine
    /// </summary>
    internal static class CoreProvisioningHelper
    {
        /// <summary>
        /// Returns the PnP.Core.Provisioning formatter for a schema version.
        /// </summary>
        /// <param name="schema">The schema version a cmdlet was given</param>
        /// <returns>The matching formatter</returns>
        internal static ITemplateFormatter GetFormatter(FrameworkSchemaVersion schema)
        {
            return schema switch
            {
                FrameworkSchemaVersion.V201909 => XMLPnPSchemaFormatter.GetSpecificFormatter(XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2019_09),
                FrameworkSchemaVersion.V202002 => XMLPnPSchemaFormatter.GetSpecificFormatter(XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2020_02),
                FrameworkSchemaVersion.V202103 => XMLPnPSchemaFormatter.GetSpecificFormatter(XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2021_03),
                FrameworkSchemaVersion.V202209 => XMLPnPSchemaFormatter.GetSpecificFormatter(XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2022_09),
                _ => XMLPnPSchemaFormatter.LatestFormatter
            };
        }

        /// <summary>
        /// Maps the flags of a -Handlers parameter onto the handler list of a PnP.Core.Provisioning configuration.
        /// </summary>
        /// <param name="handlers">The handlers a cmdlet was given</param>
        /// <returns>The matching configuration handlers. An empty list means every handler.</returns>
        internal static List<ConfigurationHandler> ToConfigurationHandlers(FrameworkHandlers handlers)
        {
            var configurationHandlers = new List<ConfigurationHandler>();
            if (handlers.HasFlag(FrameworkHandlers.All))
            {
                return configurationHandlers;
            }

            foreach (var handler in (FrameworkHandlers[])Enum.GetValues(typeof(FrameworkHandlers)))
            {
                if (handler == FrameworkHandlers.All || handler == FrameworkHandlers.None || !handlers.HasFlag(handler))
                {
                    continue;
                }

                if (handler == FrameworkHandlers.TermGroups)
                {
                    configurationHandlers.Add(ConfigurationHandler.Taxonomy);
                }
                else if (handler == FrameworkHandlers.PageContents)
                {
                    configurationHandlers.Add(ConfigurationHandler.Pages);
                }
                else if (Enum.TryParse(handler.ToString(), out ConfigurationHandler configurationHandler))
                {
                    configurationHandlers.Add(configurationHandler);
                }
            }

            return configurationHandlers.Distinct().ToList();
        }

        /// <summary>
        /// Inverts the flags of an -ExcludeHandlers parameter into the handlers to process.
        /// </summary>
        /// <param name="excludeHandlers">The handlers a cmdlet was told to exclude</param>
        /// <returns>The handlers to process</returns>
        internal static FrameworkHandlers InvertExcludedHandlers(FrameworkHandlers excludeHandlers)
        {
            var handlers = FrameworkHandlers.None;
            foreach (var handler in (FrameworkHandlers[])Enum.GetValues(typeof(FrameworkHandlers)))
            {
                if (handler != FrameworkHandlers.All && handler != FrameworkHandlers.None && !excludeHandlers.HasFlag(handler))
                {
                    handlers |= handler;
                }
            }
            return handlers;
        }

        /// <summary>
        /// Maps the -ExtensibilityHandlers parameter onto its PnP.Core.Provisioning equivalent.
        /// </summary>
        /// <param name="extensibilityHandlers">The extensibility handlers a cmdlet was given</param>
        /// <returns>The matching PnP.Core.Provisioning extensibility handlers</returns>
        internal static List<ExtensibilityHandler> ToCoreExtensibilityHandlers(PnP.Framework.Provisioning.Model.ExtensibilityHandler[] extensibilityHandlers)
        {
            if (extensibilityHandlers == null)
            {
                return new List<ExtensibilityHandler>();
            }

            return extensibilityHandlers
                .Where(handler => handler != null)
                .Select(handler => new ExtensibilityHandler
                {
                    Assembly = handler.Assembly,
                    Type = handler.Type,
                    Configuration = handler.Configuration,
                    Enabled = handler.Enabled
                })
                .ToList();
        }

        /// <summary>
        /// Loads a site template from a file on disk, which may be an .xml template or a .pnp package.
        /// </summary>
        /// <param name="templatePath">Path to the template file on disk</param>
        /// <param name="exceptionHandler">Called for every template in the source which cannot be read</param>
        /// <returns>The template, or null when it could not be read</returns>
        internal static ProvisioningTemplate LoadSiteTemplateFromFile(string templatePath, Action<Exception> exceptionHandler)
        {
            var templateFileName = Path.GetFileName(templatePath);
            var fileInfo = new FileInfo(templatePath);
            FileConnectorBase fileConnector = new FileSystemConnector(fileInfo.DirectoryName, string.Empty);

            using var stream = fileConnector.GetFileStream(templateFileName);
            if (stream == null)
            {
                throw new FileNotFoundException($"File {templatePath} does not exist.", templatePath);
            }

            var provider = GetProvider(stream, fileConnector, ref templateFileName);
            try
            {
                var template = provider.GetTemplate(templateFileName);
                if (template != null)
                {
                    template.Connector = provider.Connector;
                }
                return template;
            }
            catch (ApplicationException ex)
            {
                ReportInnerExceptions(ex, exceptionHandler);
            }
            return null;
        }

        /// <summary>
        /// Writes a site template back to the file it was loaded from, keeping the file's own format
        /// </summary>
        /// <param name="template">The template to write</param>
        /// <param name="templatePath">The path the template was loaded from</param>
        /// <param name="formatter">The formatter to write with, or null for the latest schema</param>
        internal static void SaveSiteTemplateToFile(ProvisioningTemplate template, string templatePath, ITemplateFormatter formatter = null)
        {
            formatter ??= XMLPnPSchemaFormatter.LatestFormatter;

            var fileInfo = new FileInfo(templatePath);
            if (!fileInfo.Extension.Equals(".pnp", StringComparison.OrdinalIgnoreCase))
            {
                new XMLFileSystemTemplateProvider(fileInfo.DirectoryName, string.Empty).SaveAs(template, templatePath, formatter);
                return;
            }

            var packageConnector = template.Connector as OpenXMLConnector
                ?? new OpenXMLConnector(templatePath, new FileSystemConnector(fileInfo.DirectoryName, string.Empty));
            var templateFileName = Path.GetFileNameWithoutExtension(templatePath) + ".xml";
            new XMLOpenXMLTemplateProvider(packageConnector).SaveAs(template, templateFileName, formatter);
        }

        /// <summary>
        /// Loads a site template from a string holding the template XML.
        /// </summary>
        /// <param name="xml">The template XML</param>
        /// <param name="exceptionHandler">Called for every template in the source which cannot be read</param>
        /// <returns>The template, or null when it could not be read</returns>
        internal static ProvisioningTemplate LoadSiteTemplateFromString(string xml, Action<Exception> exceptionHandler)
        {
            try
            {
                return XMLPnPSchemaFormatter.LatestFormatter.ToProvisioningTemplate(CreateXmlStream(xml));
            }
            catch (ApplicationException ex)
            {
                ReportInnerExceptions(ex, exceptionHandler);
            }
            return null;
        }

        /// <summary>
        /// Loads every site template held in a stream. Only a .pnp package can hold more than one.
        /// </summary>
        /// <param name="stream">Stream holding an .xml template or a .pnp package</param>
        /// <param name="exceptionHandler">Called for every template in the source which cannot be read</param>
        /// <returns>The templates found in the stream</returns>
        internal static List<ProvisioningTemplate> LoadSiteTemplatesFromStream(Stream stream, Action<Exception> exceptionHandler)
        {
            ArgumentNullException.ThrowIfNull(stream);

            using var memoryStream = Buffer(stream);
            if (!FileUtilities.IsOpenOfficeFile(memoryStream))
            {
                memoryStream.Position = 0;
                var xml = Encoding.UTF8.GetString(memoryStream.ToArray());
                var template = LoadSiteTemplateFromString(xml, exceptionHandler);
                return template != null ? new List<ProvisioningTemplate> { template } : new List<ProvisioningTemplate>();
            }

            memoryStream.Position = 0;
            var provider = new XMLOpenXMLTemplateProvider(new OpenXMLConnector(memoryStream));
            try
            {
                return provider.GetTemplates();
            }
            catch (ApplicationException ex)
            {
                ReportInnerExceptions(ex, exceptionHandler);
            }
            return new List<ProvisioningTemplate>();
        }

        /// <summary>
        /// Loads a tenant template from a file on disk, which may be an .xml template or a .pnp package.
        /// </summary>
        /// <param name="templatePath">Path to the template file on disk</param>
        /// <param name="exceptionHandler">Called for every template in the source which cannot be read</param>
        /// <returns>The hierarchy, or null when it could not be read</returns>
        internal static ProvisioningHierarchy LoadTenantTemplateFromFile(string templatePath, Action<Exception> exceptionHandler)
        {
            var templateFileName = Path.GetFileName(templatePath);
            var fileInfo = new FileInfo(templatePath);
            FileConnectorBase fileConnector = new FileSystemConnector(fileInfo.DirectoryName, string.Empty);

            using var stream = fileConnector.GetFileStream(templateFileName);
            if (stream == null)
            {
                throw new FileNotFoundException($"File {templatePath} does not exist.", templatePath);
            }

            var provider = GetProvider(stream, fileConnector, ref templateFileName);
            try
            {
                var hierarchy = provider is XMLOpenXMLTemplateProvider packageProvider
                    ? packageProvider.GetHierarchy()
                    : provider.GetHierarchy(templateFileName);
                if (hierarchy != null)
                {
                    hierarchy.Connector = provider.Connector;
                }
                return hierarchy;
            }
            catch (ApplicationException ex)
            {
                ReportInnerExceptions(ex, exceptionHandler);
            }
            return null;
        }

        /// <summary>
        /// Loads a tenant template from a string holding the template XML.
        /// </summary>
        /// <param name="xml">The template XML</param>
        /// <param name="exceptionHandler">Called for every template in the source which cannot be read</param>
        /// <returns>The hierarchy, or null when it could not be read</returns>
        internal static ProvisioningHierarchy LoadTenantTemplateFromString(string xml, Action<Exception> exceptionHandler)
        {
            try
            {
                return new XMLStreamTemplateProvider().GetHierarchy(CreateXmlStream(xml));
            }
            catch (ApplicationException ex)
            {
                ReportInnerExceptions(ex, exceptionHandler);
            }
            return null;
        }

        /// <summary>
        /// Loads a tenant template from a stream holding an .xml template or a .pnp package.
        /// </summary>
        /// <param name="stream">Stream holding an .xml template or a .pnp package</param>
        /// <param name="exceptionHandler">Called for every template in the source which cannot be read</param>
        /// <returns>The hierarchy, or null when it could not be read</returns>
        internal static ProvisioningHierarchy LoadTenantTemplateFromStream(Stream stream, Action<Exception> exceptionHandler)
        {
            ArgumentNullException.ThrowIfNull(stream);

            using var memoryStream = Buffer(stream);
            if (!FileUtilities.IsOpenOfficeFile(memoryStream))
            {
                memoryStream.Position = 0;
                return LoadTenantTemplateFromString(Encoding.UTF8.GetString(memoryStream.ToArray()), exceptionHandler);
            }

            memoryStream.Position = 0;
            var provider = new XMLOpenXMLTemplateProvider(new OpenXMLConnector(memoryStream));
            try
            {
                var hierarchy = provider.GetHierarchy();
                if (hierarchy != null)
                {
                    hierarchy.Connector = provider.Connector;
                }
                return hierarchy;
            }
            catch (ApplicationException ex)
            {
                ReportInnerExceptions(ex, exceptionHandler);
            }
            return null;
        }

        /// <summary>
        /// Converts a PnP Framework site template into its PnP.Core.Provisioning equivalent by writing it out
        /// with the PnP Framework serializer and reading it back with the PnP.Core.Provisioning one. 
        /// </summary>
        /// <param name="template">The PnP Framework template</param>
        /// <returns>The equivalent PnP.Core.Provisioning template</returns>
        internal static ProvisioningTemplate ToCoreTemplate(PnP.Framework.Provisioning.Model.ProvisioningTemplate template)
        {
            ArgumentNullException.ThrowIfNull(template);

            using var stream = PnP.Framework.Provisioning.Providers.Xml.XMLPnPSchemaFormatter.LatestFormatter.ToFormattedTemplate(template);
            stream.Position = 0;
            return XMLPnPSchemaFormatter.LatestFormatter.ToProvisioningTemplate(stream);
        }

        /// <summary>
        /// Converts a PnP Framework tenant template into its PnP.Core.Provisioning equivalent by writing it out
        /// with the PnP Framework serializer and reading it back with the PnP.Core.Provisioning one. 
        /// </summary>
        /// <param name="hierarchy">The PnP Framework hierarchy</param>
        /// <returns>The equivalent PnP.Core.Provisioning hierarchy</returns>
        internal static ProvisioningHierarchy ToCoreHierarchy(PnP.Framework.Provisioning.Model.ProvisioningHierarchy hierarchy)
        {
            ArgumentNullException.ThrowIfNull(hierarchy);

            var formatter = PnP.Framework.Provisioning.Providers.Xml.XMLPnPSchemaFormatter.LatestFormatter
                as PnP.Framework.Provisioning.Providers.IProvisioningHierarchyFormatter;
            using var stream = formatter.ToFormattedHierarchy(hierarchy);
            stream.Position = 0;
            return new XMLStreamTemplateProvider().GetHierarchy(stream);
        }

        /// <summary>
        /// Writes the XML of a template into a stream the formatters can read.
        /// </summary>
        /// <param name="xml">The template XML</param>
        /// <returns>A stream positioned at the start of the XML</returns>
        internal static Stream CreateXmlStream(string xml)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml ?? string.Empty));
            stream.Position = 0;
            return stream;
        }

        private static XMLTemplateProvider GetProvider(Stream stream, FileConnectorBase fileConnector, ref string templateFileName)
        {
            if (!FileUtilities.IsOpenOfficeFile(stream))
            {
                return new XMLFileSystemTemplateProvider(fileConnector.Parameters[FileConnectorBase.CONNECTIONSTRING] + string.Empty, string.Empty);
            }

            var openXmlConnector = new OpenXMLConnector(templateFileName, fileConnector);
            templateFileName = !string.IsNullOrEmpty(openXmlConnector.Info?.Properties?.TemplateFileName)
                ? openXmlConnector.Info.Properties.TemplateFileName
                : templateFileName.Substring(0, templateFileName.LastIndexOf(".", StringComparison.Ordinal)) + ".xml";
            return new XMLOpenXMLTemplateProvider(openXmlConnector);
        }

        private static MemoryStream Buffer(Stream source)
        {
            if (source.CanSeek)
            {
                source.Position = 0;
            }

            var buffered = new MemoryStream();
            source.CopyTo(buffered);
            buffered.Position = 0;

            if (source.CanSeek)
            {
                source.Position = 0;
            }

            return buffered;
        }

        private static void ReportInnerExceptions(ApplicationException exception, Action<Exception> exceptionHandler)
        {
            if (exceptionHandler == null)
            {
                throw exception;
            }

            if (exception.InnerException is AggregateException aggregateException)
            {
                foreach (var innerException in aggregateException.InnerExceptions)
                {
                    exceptionHandler(innerException);
                }
            }
            else
            {
                exceptionHandler(exception);
            }
        }
    }
}
