using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers;
using PnP.Framework.Provisioning.Providers.Xml;

using PnP.PowerShell.Commands.Utilities;
using System;
using System.IO;
using System.Management.Automation;
using System.Text;

namespace PnP.PowerShell.Commands.Provisioning
{
    [Cmdlet(VerbsCommunications.Read, "PnPProvisioningTemplate")]
    [Alias("Load-PnPProvisioningTemplate")]
    public class ReadProvisioningTemplate : PSCmdlet
    {
        const string ParameterSet_PATH = "By Path";
        const string ParameterSet_XML = "By XML";

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_PATH)]
        public string Path;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSet_XML)]
        public string Xml;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ITemplateProviderExtension[] TemplateProviderExtensions;

        protected override void ProcessRecord()
        {
            if (MyInvocation.InvocationName.ToLower() == "load-pnpprovisioningtemplate")
            {
                WriteWarning("Load-PnPProvisioningTemplate has been deprecated in favor of Read-PnPProvisioningTemplate which supports the same parameters.");
            }
            switch (ParameterSetName)
            {
                case ParameterSet_PATH:
                    {

                        if (!System.IO.Path.IsPathRooted(Path))
                        {
                            Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                        }
                        WriteObject(LoadProvisioningTemplateFromFile(Path, TemplateProviderExtensions, (e) =>
                        {
                            WriteError(new ErrorRecord(e, "TEMPLATENOTVALID", ErrorCategory.SyntaxError, null));
                        }));
                        break;
                    }
                case ParameterSet_XML:
                    {
                        WriteObject(LoadProvisioningTemplateFromString(Xml, TemplateProviderExtensions, (e) =>
                        {
                            WriteError(new ErrorRecord(e, "TEMPLATENOTVALID", ErrorCategory.SyntaxError, null));
                        }));
                        break;
                    }
            }
        }

        internal static ProvisioningTemplate LoadProvisioningTemplateFromString(string xml, ITemplateProviderExtension[] templateProviderExtensions, Action<Exception> exceptionHandler)
        {
            var formatter = new XMLPnPSchemaFormatter();

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

        internal static ProvisioningTemplate LoadProvisioningTemplateFromFile(string templatePath, ITemplateProviderExtension[] templateProviderExtensions, Action<Exception> exceptionHandler)
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
}
