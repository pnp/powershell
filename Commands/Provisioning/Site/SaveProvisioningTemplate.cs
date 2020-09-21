using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers;
using PnP.Framework.Provisioning.Providers.Xml;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Provisioning
{
    [Cmdlet(VerbsData.Save, "PnPProvisioningTemplate")]
    public class SaveProvisioningTemplate : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [Alias("InputInstance")]
        public ProvisioningTemplatePipeBind Template;

        [Parameter(Mandatory = true, Position = 0)]
        public string Out;

        [Parameter(Mandatory = false)]
        public XMLPnPSchemaVersion Schema = XMLPnPSchemaVersion.LATEST;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public ITemplateProviderExtension[] TemplateProviderExtensions;

        protected override void ProcessRecord()
        {
            var templateObject = Template.GetTemplate(SessionState.Path.CurrentFileSystemLocation.Path, (e) =>
            {
                WriteError(new ErrorRecord(e, "TEMPLATENOTVALID", ErrorCategory.SyntaxError, null));
            });

            // Determine the output file name and path
            string outFileName = Path.GetFileName(Out);

            if (!Path.IsPathRooted(Out))
            {
                Out = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Out);
            }

            bool proceed = false;

            if (System.IO.File.Exists(Out))
            {
                if (Force || ShouldContinue(string.Format(Properties.Resources.File0ExistsOverwrite, Out),
                    Properties.Resources.Confirm))
                {
                    proceed = true;
                }
            }
            else
            {
                proceed = true;
            }

            string outPath = new FileInfo(Out).DirectoryName;

            // Determine if it is an .XML or a .PNP file
            var extension = "";
            if (proceed && outFileName != null)
            {
                if (outFileName.IndexOf(".", StringComparison.Ordinal) > -1)
                {
                    extension = outFileName.Substring(outFileName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
                }
                else
                {
                    extension = ".pnp";
                }
            }

            var fileSystemConnector = new FileSystemConnector(outPath, "");

            ITemplateFormatter formatter = ProvisioningHelper.GetFormatter(Schema);

            if (extension == ".pnp")
            {
#if !PNPPSCORE
                IsolatedStorage.InitializeIsolatedStorage();
#endif
                XMLTemplateProvider provider = new XMLOpenXMLTemplateProvider(
                      Out, fileSystemConnector);
                var templateFileName = outFileName.Substring(0, outFileName.LastIndexOf(".", StringComparison.Ordinal)) + ".xml";

                provider.SaveAs(templateObject, templateFileName, formatter, TemplateProviderExtensions);
                ProcessFiles(templateObject, Out, fileSystemConnector, provider.Connector);
            }
            else
            {
                XMLTemplateProvider provider = new XMLFileSystemTemplateProvider(outPath, "");
                provider.SaveAs(templateObject, Out, formatter, TemplateProviderExtensions);
            }
        }

        private void ProcessFiles(ProvisioningTemplate template, string templateFileName, FileConnectorBase fileSystemConnector, FileConnectorBase connector)
        {
            var templateFile = ReadProvisioningTemplate.LoadProvisioningTemplateFromFile(templateFileName, null, (e) =>
            {
                WriteError(new ErrorRecord(e, "TEMPLATENOTVALID", ErrorCategory.SyntaxError, null));
            });
            if (template.Tenant?.AppCatalog != null)
            {
                foreach (var app in template.Tenant.AppCatalog.Packages)
                {
                    WriteObject($"Processing {app.Src}");
                    AddFile(app.Src, templateFile, fileSystemConnector, connector);
                }
            }
            if (template.Tenant?.SiteScripts != null)
            {
                foreach (var siteScript in template.Tenant.SiteScripts)
                {
                    WriteObject($"Processing {siteScript.JsonFilePath}");
                    AddFile(siteScript.JsonFilePath, templateFile, fileSystemConnector, connector);
                }
            }
            if (template.Localizations != null && template.Localizations.Any())
            {
                foreach (var location in template.Localizations)
                {
                    WriteObject($"Processing {location.ResourceFile}");
                    AddFile(location.ResourceFile, templateFile, fileSystemConnector, connector);
                }
            }

            if (template.WebSettings != null && !String.IsNullOrEmpty(template.WebSettings.SiteLogo))
            {
                // is it a file?
                var isFile = false;
                using (var fileStream = fileSystemConnector.GetFileStream(template.WebSettings.SiteLogo))
                {
                    isFile = fileStream != null;
                }
                if (isFile)
                {
                    WriteObject($"Processing {template.WebSettings.SiteLogo}");
                    AddFile(template.WebSettings.SiteLogo, templateFile, fileSystemConnector, connector);
                }
            }
            if (template.Files.Any())
            {
                foreach (var file in template.Files)
                {
                    WriteObject($"Processing {file.Src}");
                    AddFile(file.Src, templateFile, fileSystemConnector, connector);
                }
            }

            if (templateFile.Connector is ICommitableFileConnector)
            {
                ((ICommitableFileConnector)templateFile.Connector).Commit();
            }
        }

        private void AddFile(string sourceName, ProvisioningTemplate provisioningTemplate, FileConnectorBase fileSystemConnector, FileConnectorBase connector)
        {
            using (var fs = fileSystemConnector.GetFileStream(sourceName))
            {
                var fileName = sourceName.IndexOf("\\") > 0 ? sourceName.Substring(sourceName.LastIndexOf("\\") + 1) : sourceName;
                var folderName = sourceName.IndexOf("\\") > 0 ? sourceName.Substring(0, sourceName.LastIndexOf("\\")) : "";
                provisioningTemplate.Connector.SaveFileStream(fileName, folderName, fs);
            }
        }
    }
}
