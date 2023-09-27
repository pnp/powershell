using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers;
using PnP.Framework.Provisioning.Providers.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
