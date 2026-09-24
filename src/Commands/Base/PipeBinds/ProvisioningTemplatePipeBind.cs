using System;
using System.IO;
using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers.Xml;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class ProvisioningTemplatePipeBind
    {
        private ProvisioningTemplate template;
        private PnP.Core.Provisioning.Model.ProvisioningTemplate coreTemplate;
        private string templatePath;

        public ProvisioningTemplatePipeBind(ProvisioningTemplate template)
        {
            this.template = template;
        }

        public ProvisioningTemplatePipeBind(PnP.Core.Provisioning.Model.ProvisioningTemplate template)
        {
            this.coreTemplate = template;
        }

        public ProvisioningTemplatePipeBind(string templatePath)
        {
            this.templatePath = templatePath;
        }

        /// <summary>
        /// Returns the template as a PnP.Core.Provisioning template
        /// </summary>
        /// <param name="rootPath">The location to resolve a relative path against</param>
        /// <param name="exceptionHandler">Called for every template in the source which cannot be read</param>
        /// <returns>The template, or null when nothing was passed in</returns>
        internal PnP.Core.Provisioning.Model.ProvisioningTemplate GetCoreTemplate(string rootPath, Action<Exception> exceptionHandler)
        {
            if (this.coreTemplate != null)
            {
                return this.coreTemplate;
            }
            if (this.template != null)
            {
                return CoreProvisioningHelper.ToCoreTemplate(this.template);
            }
            if (!string.IsNullOrEmpty(templatePath))
            {
                if (!System.IO.Path.IsPathRooted(templatePath))
                {
                    templatePath = System.IO.Path.Combine(rootPath, templatePath);
                }
                return CoreProvisioningHelper.LoadSiteTemplateFromFile(templatePath, exceptionHandler);
            }
            return null;
        }

        public ProvisioningTemplate GetTemplate(string rootPath, Action<Exception> exceptionHandler)
        {
            if (this.template != null)
            {
                return this.template;
            }
            if(!string.IsNullOrEmpty(templatePath))
            {
                if (!System.IO.Path.IsPathRooted(templatePath))
                {
                    templatePath = System.IO.Path.Combine(rootPath, templatePath);
                }
                return LoadProvisioningTemplateFromFile(templatePath, (e) =>
                {
                    if(exceptionHandler != null)
                    {
                        exceptionHandler(e);
                    }
                });
            }
            return null;
        }

        internal static ProvisioningTemplate LoadProvisioningTemplateFromFile(string templatePath, Action<Exception> exceptionHandler)
        {
            // Prepare the File Connector
            string templateFileName = System.IO.Path.GetFileName(templatePath);

            // Prepare the template path
            var fileInfo = new FileInfo(templatePath);
            FileConnectorBase fileConnector = new FileSystemConnector(fileInfo.DirectoryName, "");

            // Load the provisioning template file
            Stream stream = fileConnector.GetFileStream(templateFileName);
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
                ProvisioningTemplate provisioningTemplate = provider.GetTemplate(templateFileName);
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
}
