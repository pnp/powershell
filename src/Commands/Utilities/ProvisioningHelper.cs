using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers;
using PnP.Framework.Provisioning.Providers.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PnP.PowerShell.Commands.Utilities
{
    public static class ProvisioningHelper
    {
        public static ITemplateFormatter GetFormatter(XMLPnPSchemaVersion schema)
        {
            ITemplateFormatter formatter = null;
            switch (schema)
            {
                case XMLPnPSchemaVersion.LATEST:
                    {
                        formatter = XMLPnPSchemaFormatter.LatestFormatter;
                        break;
                    }
                case XMLPnPSchemaVersion.V201909:
                    {
                        formatter = XMLPnPSchemaFormatter.GetSpecificFormatter(XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2019_09);
                        break;
                    }
                case XMLPnPSchemaVersion.V202002:
                    {
                        formatter = XMLPnPSchemaFormatter.GetSpecificFormatter(XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2020_02);
                        break;
                    }
                case XMLPnPSchemaVersion.V202103:
                    {
                        formatter = XMLPnPSchemaFormatter.GetSpecificFormatter(XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2021_03);
                        break;
                    }
            }
            return formatter;
        }

        #region Site Templates

        /// <summary>
        /// Loads a PnP Site Provisioning Template from a file on disk
        /// </summary>
        /// <param name="templatePath">Path to the template file on disk</param>
        /// <param name="templateProviderExtensions"></param>
        /// <param name="exceptionHandler"></param>
        /// <returns>Template definition</returns>
        internal static ProvisioningTemplate LoadSiteTemplateFromFile(string templatePath, ITemplateProviderExtension[] templateProviderExtensions, Action<Exception> exceptionHandler)
        {
            // Prepare the File Connector
            string templateFileName = System.IO.Path.GetFileName(templatePath);

            // Prepare the template path
            var fileInfo = new FileInfo(templatePath);
            FileConnectorBase fileConnector = new FileSystemConnector(fileInfo.DirectoryName, "");

            // Load the site template file
            using (var stream = fileConnector.GetFileStream(templateFileName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"File {templatePath} does not exist.", templatePath);
                }
                var isOpenOfficeFile = FileUtilities.IsOpenOfficeFile(stream);

                XMLTemplateProvider provider;
                if (isOpenOfficeFile)
                {
                    var openXmlConnector = new OpenXMLConnector(templateFileName, fileConnector);
                    provider = new XMLOpenXMLTemplateProvider(openXmlConnector);
                    if (!String.IsNullOrEmpty(openXmlConnector.Info?.Properties?.TemplateFileName))
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
                    provider = new XMLFileSystemTemplateProvider(fileConnector.Parameters[FileConnectorBase.CONNECTIONSTRING] + "", "");
                }
                try
                {
                    ProvisioningTemplate provisioningTemplate = provider.GetTemplate(templateFileName, templateProviderExtensions);
                    provisioningTemplate.Connector = provider.Connector;
                    return provisioningTemplate;
                }
                catch (ApplicationException ex)
                {
                    if (ex.InnerException is AggregateException)
                    {
                        if (exceptionHandler != null)
                        {
                            foreach (var exception in ((AggregateException)ex.InnerException).InnerExceptions)
                            {
                                exceptionHandler(exception);
                            }
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Loads a PnP Site Provisioning Template from a stream
        /// </summary>
        /// <param name="stream">Stream containing the provisioning template</param>
        /// <param name="templateProviderExtensions"></param>
        /// <param name="exceptionHandler"></param>
        /// <returns>List with template definitions found within the stream</returns>
        /// <exception cref="ArgumentNullException">Thrown when stream is not provided</exception>
        internal static List<ProvisioningTemplate> LoadSiteTemplatesFromStream(Stream stream, ITemplateProviderExtension[] templateProviderExtensions, Action<Exception> exceptionHandler)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), "Stream must be provided");
            }

            if (stream.CanSeek)
            {
                stream.Position = 0;
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                stream.CopyTo(memoryStream);
                memoryStream.Position = 0;

                // Validate if the stream contains an OpenXML .pnp template or a .xml template
                var isOpenOfficeFile = FileUtilities.IsOpenOfficeFile(memoryStream);
                memoryStream.Position = 0;

                if (!isOpenOfficeFile)
                {
                    var xml = Encoding.UTF8.GetString(memoryStream.ToArray());
                    return new List<ProvisioningTemplate> { LoadSiteTemplateFromString(xml, templateProviderExtensions, exceptionHandler) };
                }

                var openXmlConnector = new OpenXMLConnector(memoryStream);
                var provider = new XMLOpenXMLTemplateProvider(openXmlConnector);

                try
                {
                    var provisioningTemplates = provider.GetTemplates();
                    return provisioningTemplates;
                }
                catch (ApplicationException ex)
                {
                    if (ex.InnerException is AggregateException)
                    {
                        if (exceptionHandler != null)
                        {
                            foreach (var exception in ((AggregateException)ex.InnerException).InnerExceptions)
                            {
                                exceptionHandler(exception);
                            }
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Loads every PnP Site Provisioning Template from a stream and reports invalid package members instead of skipping them.
        /// </summary>
        /// <param name="stream">Stream containing the provisioning template</param>
        /// <param name="templateId">Optional ID of the template to load</param>
        /// <param name="templateProviderExtensions">Template provider extensions to run while loading</param>
        /// <param name="connector">Connector used to resolve includes when the source is not a package</param>
        /// <param name="exceptionHandler">Delegate to call for every invalid template, with the package member it came from</param>
        /// <param name="schemaNamespaceHandler">Delegate to call with each template, its package member, its provisioning schema namespace, and its source element</param>
        /// <param name="duplicateTemplateIdHandler">Delegate to call for every duplicate template ID</param>
        /// <param name="unaddressableTemplateHandler">Delegate to call for a source holding several templates of which one has no ID</param>
        /// <returns>Template definitions found within the stream</returns>
        internal static List<ProvisioningTemplate> LoadSiteTemplatesFromStreamStrict(
            Stream stream,
            string templateId,
            FileConnectorBase connector,
            ITemplateProviderExtension[] templateProviderExtensions,
            Action<Exception, string> exceptionHandler,
            Action<ProvisioningTemplate, string, string, XElement> schemaNamespaceHandler,
            Action<string, string> duplicateTemplateIdHandler,
            Action<string> unaddressableTemplateHandler)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), "Stream must be provided");
            }

            using var memoryStream = new MemoryStream();
            if (stream.CanSeek)
            {
                stream.Position = 0;
            }
            stream.CopyTo(memoryStream);
            memoryStream.Position = 0;

            var isOpenOfficeFile = FileUtilities.IsOpenOfficeFile(memoryStream);
            memoryStream.Position = 0;

            XMLTemplateProvider provider;
            List<string> templateFiles;
            if (isOpenOfficeFile)
            {
                // OpenXMLConnector eagerly unpacks the package, so disposing this buffer after loading is safe.
                var openXmlConnector = new OpenXMLConnector(memoryStream);
                provider = new XMLOpenXMLTemplateProvider(openXmlConnector);
                var primaryTemplateFile = openXmlConnector.Info?.Properties?.TemplateFileName;
                templateFiles = FindTemplateFiles(openXmlConnector, primaryTemplateFile, templateProviderExtensions);
            }
            else
            {
                // The connector lets the provider resolve XInclude references the same way Read-PnPSiteTemplate does.
                provider = new XMLStreamTemplateProvider { Connector = connector };
                templateFiles = [null];
            }

            var provisioningTemplates = new List<ProvisioningTemplate>();
            var packageTemplateIdentifiers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var templateFile in templateFiles)
            {
                try
                {
                    var (templateIdentifiers, schemaNamespace, normalizedDocument, sourceDocument) = GetTemplateIdentifiers(provider, templateFile, memoryStream, templateId, templateProviderExtensions, duplicateTemplateIdHandler, unaddressableTemplateHandler);
                    foreach (var currentTemplateIdentifier in templateIdentifiers.Where(identifier => !string.IsNullOrWhiteSpace(identifier)))
                    {
                        // A duplicate elsewhere in the source says nothing about the template that was asked for.
                        if (!packageTemplateIdentifiers.Add(currentTemplateIdentifier) &&
                            (string.IsNullOrEmpty(templateId) || string.Equals(currentTemplateIdentifier, templateId, StringComparison.OrdinalIgnoreCase)))
                        {
                            duplicateTemplateIdHandler?.Invoke(currentTemplateIdentifier, templateFile ?? "stream");
                        }
                    }
                    if (!string.IsNullOrEmpty(templateId))
                    {
                        var matchingIdentifier = templateIdentifiers.FirstOrDefault(identifier => string.Equals(identifier, templateId, StringComparison.OrdinalIgnoreCase));
                        if (matchingIdentifier == null)
                        {
                            continue;
                        }
                        templateIdentifiers = [matchingIdentifier];
                    }

                    foreach (var templateIdentifier in templateIdentifiers)
                    {
                        // This XML was already pre-processed while reading it, so the provider only resolves includes
                        // and deserializes here, and the extensions contribute their post-processing afterwards.
                        using var templateStream = CreateStream(normalizedDocument ?? sourceDocument);
                        var provisioningTemplate = string.IsNullOrEmpty(templateIdentifier)
                            ? provider.GetTemplate(templateStream, (ITemplateProviderExtension[])null)
                            : provider.GetTemplate(templateStream, templateIdentifier, null, null);
                        provisioningTemplate = ApplyPostProcessing(provisioningTemplate, templateProviderExtensions);

                        if (provisioningTemplate != null)
                        {
                            provisioningTemplate.Connector = provider.Connector;
                            provisioningTemplates.Add(provisioningTemplate);
                            schemaNamespaceHandler?.Invoke(
                                provisioningTemplate,
                                templateFile,
                                schemaNamespace,
                                GetTemplateElement(normalizedDocument ?? sourceDocument, templateIdentifier));
                        }
                    }
                }
                catch (Exception exception) when (exception is ApplicationException or System.Xml.XmlException or InvalidDataException or FileNotFoundException)
                {
                    if (exception.InnerException is AggregateException aggregateException)
                    {
                        foreach (var innerException in aggregateException.InnerExceptions)
                        {
                            exceptionHandler?.Invoke(innerException, templateFile);
                        }
                    }
                    else
                    {
                        exceptionHandler?.Invoke(exception, templateFile);
                    }
                }
            }

            return provisioningTemplates;
        }

        private static List<string> FindTemplateFiles(OpenXMLConnector connector, string primaryTemplateFile, ITemplateProviderExtension[] templateProviderExtensions)
        {
            var templateFiles = new List<string>();
            if (!string.IsNullOrWhiteSpace(primaryTemplateFile))
            {
                templateFiles.Add(primaryTemplateFile);
            }

            // GetFolders lists every nested folder, so templates below the package root are found too.
            var containers = new List<string> { string.Empty };
            containers.AddRange(connector.GetFolders().Distinct(StringComparer.OrdinalIgnoreCase));

            foreach (var container in containers)
            {
                foreach (var file in connector.GetFiles(container).Where(file => file.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)))
                {
                    var templateFile = string.IsNullOrEmpty(container) ? file : $"{container.Replace('\\', '/').Trim('/')}/{file}";
                    if (templateFiles.Contains(templateFile, StringComparer.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    using var stream = ApplyPreProcessing(connector.GetFileStream(file, container), templateProviderExtensions);
                    try
                    {
                        using var reader = System.Xml.XmlReader.Create(stream);
                        reader.MoveToContent();
                        if (IsSiteTemplateRoot(reader.LocalName, reader.NamespaceURI))
                        {
                            templateFiles.Add(templateFile);
                        }
                    }
                    catch (System.Xml.XmlException)
                    {
                        // XML that fails before exposing its root cannot safely be distinguished from a non-template resource.
                    }
                }
            }
            return templateFiles;
        }

        private static (List<string> Identifiers, string SchemaNamespace, XDocument NormalizedDocument, XDocument SourceDocument) GetTemplateIdentifiers(
            XMLTemplateProvider provider,
            string templateFile,
            Stream sourceStream,
            string templateId,
            ITemplateProviderExtension[] templateProviderExtensions,
            Action<string, string> duplicateTemplateIdHandler,
            Action<string> unaddressableTemplateHandler)
        {
            var sourceTemplateStream = templateFile == null
                ? CopyStream(sourceStream)
                : provider.Connector.GetFileStream(templateFile);
            if (sourceTemplateStream == null)
            {
                throw new FileNotFoundException($"Template file '{templateFile}' could not be found in the package.", templateFile);
            }

            using var templateStream = ApplyPreProcessing(sourceTemplateStream, templateProviderExtensions);

            var document = XDocument.Load(templateStream);
            if (!IsSiteTemplateDocument(document))
            {
                throw new InvalidDataException($"The XML document '{templateFile ?? "stream"}' does not contain a site template.");
            }

            var schemaNamespace = document.Root.Name.NamespaceName;
            var templateElements = document.Root.Name.LocalName == "ProvisioningTemplate"
                ? [document.Root]
                : document.Descendants(XName.Get("ProvisioningTemplate", schemaNamespace)).ToList();
            var identifiers = templateElements.Select(element => (string)element.Attribute("ID")).ToList();

            // Templates can live inside an XInclude, which only the provider can resolve, so leave the selection to it.
            if (identifiers.Count == 0 && document.Descendants(XName.Get("{http://www.w3.org/2001/XInclude}include")).Any())
            {
                return ([null], schemaNamespace, null, document);
            }

            // A template without an ID cannot be addressed individually once the document holds more than one.
            if (templateElements.Count > 1 && identifiers.Any(string.IsNullOrWhiteSpace))
            {
                if (string.IsNullOrEmpty(templateId))
                {
                    unaddressableTemplateHandler?.Invoke(templateFile ?? "stream");
                }
                identifiers = identifiers.Where(identifier => !string.IsNullOrWhiteSpace(identifier)).ToList();
            }

            var duplicateIdentifiers = identifiers
                .Where(identifier => !string.IsNullOrWhiteSpace(identifier))
                .GroupBy(identifier => identifier, StringComparer.OrdinalIgnoreCase)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();
            // A duplicate elsewhere in the document says nothing about the template that was asked for.
            foreach (var duplicateIdentifier in duplicateIdentifiers.Where(identifier =>
                string.IsNullOrEmpty(templateId) || string.Equals(identifier, templateId, StringComparison.OrdinalIgnoreCase)))
            {
                duplicateTemplateIdHandler?.Invoke(duplicateIdentifier, templateFile ?? "stream");
            }

            XDocument normalizedDocument = null;
            if (duplicateIdentifiers.Count > 0)
            {
                normalizedDocument = new XDocument(document);
                var seenIdentifiers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var normalizedTemplateElements = normalizedDocument.Root.Name.LocalName == "ProvisioningTemplate"
                    ? [normalizedDocument.Root]
                    : normalizedDocument.Descendants(XName.Get("ProvisioningTemplate", schemaNamespace)).ToList();
                foreach (var templateElement in normalizedTemplateElements.ToList())
                {
                    var identifier = (string)templateElement.Attribute("ID");
                    if (!string.IsNullOrWhiteSpace(identifier) && !seenIdentifiers.Add(identifier))
                    {
                        templateElement.Remove();
                    }
                }
            }

            return (identifiers.Distinct(StringComparer.OrdinalIgnoreCase).ToList(), schemaNamespace, normalizedDocument, document);
        }

        private static XElement GetTemplateElement(XDocument document, string templateIdentifier)
        {
            var templateElements = document.Root.Name.LocalName == "ProvisioningTemplate"
                ? [document.Root]
                : document.Descendants(XName.Get("ProvisioningTemplate", document.Root.Name.NamespaceName)).ToList();
            return string.IsNullOrEmpty(templateIdentifier)
                ? templateElements.FirstOrDefault()
                : templateElements.FirstOrDefault(element => string.Equals((string)element.Attribute("ID"), templateIdentifier, StringComparison.OrdinalIgnoreCase));
        }

        private static bool IsSiteTemplateDocument(XDocument document)
        {
            return document.Root != null &&
                IsSiteTemplateRoot(document.Root.Name.LocalName, document.Root.Name.NamespaceName);
        }

        private static bool IsSiteTemplateRoot(string localName, string schemaNamespace)
        {
            return IsPotentialSiteTemplateRoot(localName) &&
                schemaNamespace.Contains("/PnP/", StringComparison.OrdinalIgnoreCase) &&
                schemaNamespace.EndsWith("/ProvisioningSchema", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsPotentialSiteTemplateRoot(string localName)
        {
            return localName == "Provisioning" || localName == "ProvisioningTemplate";
        }

        // Extensions may decrypt or rewrite the XML, so they have to run before it is parsed here.
        private static Stream ApplyPreProcessing(Stream stream, ITemplateProviderExtension[] templateProviderExtensions)
        {
            foreach (var extension in templateProviderExtensions?.Where(extension => extension.SupportsGetTemplatePreProcessing) ?? [])
            {
                stream = extension.PreProcessGetTemplate(stream);
            }
            return stream;
        }

        private static ProvisioningTemplate ApplyPostProcessing(ProvisioningTemplate template, ITemplateProviderExtension[] templateProviderExtensions)
        {
            foreach (var extension in templateProviderExtensions?.Where(extension => extension.SupportsGetTemplatePostProcessing) ?? [])
            {
                template = extension.PostProcessGetTemplate(template);
            }
            return template;
        }

        private static MemoryStream CopyStream(Stream source)
        {
            if (source.CanSeek)
            {
                source.Position = 0;
            }
            var copy = new MemoryStream();
            source.CopyTo(copy);
            copy.Position = 0;
            return copy;
        }

        private static MemoryStream CreateStream(XDocument document)
        {
            var stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;
            return stream;
        }

        internal static MemoryStream CreateXmlStream(string xml)
        {
            var document = XDocument.Parse(xml, LoadOptions.PreserveWhitespace);
            // The XML arrives already decoded, so the declared encoding is restated as the one actually written.
            if (document.Declaration != null)
            {
                document.Declaration.Encoding = "utf-8";
            }

            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, new UTF8Encoding(false), leaveOpen: true))
            {
                document.Save(writer, SaveOptions.DisableFormatting);
            }
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Loads a PnP Site Provisioning Template from passed in XML
        /// </summary>
        /// <param name="xml">String containing the XML of the template</param>
        /// <param name="templateProviderExtensions"></param>
        /// <param name="exceptionHandler"></param>
        /// <returns>Template definition</returns>
        internal static ProvisioningTemplate LoadSiteTemplateFromString(string xml, ITemplateProviderExtension[] templateProviderExtensions, Action<Exception> exceptionHandler)
        {
            XMLTemplateProvider provider = new XMLStreamTemplateProvider();

            try
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                {
                    return provider.GetTemplate(stream, templateProviderExtensions);
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.InnerException is AggregateException)
                {
                    if (exceptionHandler != null)
                    {
                        foreach (var exception in ((AggregateException)ex.InnerException).InnerExceptions)
                        {
                            exceptionHandler(exception);
                        }
                    }
                }
            }
            return null;
        }

        #endregion

        #region Tenant Templates

       /// <summary>
        /// Loads a PnP Tenant Provisioning Template from a file on disk
        /// </summary>
        /// <param name="templatePath">Path to the template file on disk</param>
        /// <param name="exceptionHandler">Delegate to call if applying the template fails</param>
        /// <returns>ProvisioningHierarchy definition</returns>
        internal static ProvisioningHierarchy LoadTenantTemplateFromFile(string templatePath, Action<Exception> exceptionHandler)
        {
            // Prepare the File Connector
            string templateFileName = Path.GetFileName(templatePath);

            // Prepare the template path
            var fileInfo = new FileInfo(templatePath);
            FileConnectorBase fileConnector = new FileSystemConnector(fileInfo.DirectoryName, "");

            // Load the provisioning template file
            var isOpenOfficeFile = false;
            using (var stream = fileConnector.GetFileStream(templateFileName))
            {
                isOpenOfficeFile = FileUtilities.IsOpenOfficeFile(stream);
            }

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

                var hierarchy = (provider as XMLOpenXMLTemplateProvider).GetHierarchy();
                if (hierarchy != null)
                {
                    hierarchy.Connector = provider.Connector;
                    return hierarchy;
                }
            }
            else
            {
                provider = new XMLFileSystemTemplateProvider(fileConnector.Parameters[FileConnectorBase.CONNECTIONSTRING] + "", "");
            }

            try
            {
                ProvisioningHierarchy provisioningHierarchy = provider.GetHierarchy(templateFileName);
                provisioningHierarchy.Connector = provider.Connector;
                return provisioningHierarchy;
            }
            catch (ApplicationException ex)
            {
                if(ex.InnerException is AggregateException exception1)
                {
                    if (exceptionHandler != null)
                    {
                        foreach (var exception in exception1.InnerExceptions)
                        {
                            exceptionHandler(exception);
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Loads a PnP Tenant Provisioning Template from a stream
        /// </summary>
        /// <param name="stream">Stream containing the tenant template</param>
        /// <param name="exceptionHandler">Delegate to call if applying the template fails</param>
        /// <returns>List with ProvisioningHierarchy instances found within the stream</returns>
        /// <exception cref="ArgumentNullException">Thrown when stream is not provided</exception>
        internal static List<ProvisioningHierarchy> LoadTenantTemplatesFromStream(Stream stream, Action<Exception> exceptionHandler)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), "Stream must be provided");
            }

            if (stream.CanSeek)
            {
                stream.Position = 0;
            }

            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            memoryStream.Position = 0;

            // Validate if the stream contains an OpenXML .pnp template or a .xml template
            var isOpenOfficeFile = FileUtilities.IsOpenOfficeFile(memoryStream);
            memoryStream.Position = 0;

            if (!isOpenOfficeFile)
            {
                var xml = Encoding.UTF8.GetString(memoryStream.ToArray());
                return new List<ProvisioningHierarchy> { LoadTenantTemplateFromString(xml, exceptionHandler) };
            }

            var openXmlConnector = new OpenXMLConnector(memoryStream);
            var provider = new XMLOpenXMLTemplateProvider(openXmlConnector);

            try
            {
                var provisioningTemplates = provider.GetHierarchies();
                return provisioningTemplates;
            }
            catch (ApplicationException ex)
            {
                if (ex.InnerException is AggregateException exception1)
                {
                    if (exceptionHandler != null)
                    {
                        foreach (var exception in exception1.InnerExceptions)
                        {
                            exceptionHandler(exception);
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Loads a PnP Tenant Provisioning Template from passed in XML
        /// </summary>
        /// <param name="xml">String containing the XML of the tenant template</param>
        /// <param name="exceptionHandler">Delegate to call if applying the template fails</param>
        /// <returns>ProvisioningHierarchy definition</returns>
        internal static ProvisioningHierarchy LoadTenantTemplateFromString(string xml, Action<Exception> exceptionHandler)
        {
            XMLTemplateProvider provider = new XMLStreamTemplateProvider();

            try
            {
                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
                return provider.GetHierarchy(stream);
            }
            catch (ApplicationException ex)
            {
                if (ex.InnerException is AggregateException exception1)
                {
                    if (exceptionHandler != null)
                    {
                        foreach (var exception in exception1.InnerExceptions)
                        {
                            exceptionHandler(exception);
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
