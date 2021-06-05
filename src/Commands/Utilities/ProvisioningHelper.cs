using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers;
using PnP.Framework.Provisioning.Providers.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                case XMLPnPSchemaVersion.V201903:
                    {
                        formatter = XMLPnPSchemaFormatter.GetSpecificFormatter(XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2019_03);
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
    }
}
