using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers;
using PnP.Framework.Provisioning.Providers.Xml;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    [Cmdlet(VerbsCommon.Remove, "PnPFileFromSiteTemplate")]
    public class RemoveFileFromSiteTemplate : BasePSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Path;

        [Parameter(Mandatory = true, Position = 1)]
        public string FilePath;

        [Parameter(Mandatory = false, Position = 2)]
        public ITemplateProviderExtension[] TemplateProviderExtensions;

        protected override void ProcessRecord()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }
            // Load the template
            ProvisioningTemplate template = ProvisioningHelper.LoadSiteTemplateFromFile(Path, TemplateProviderExtensions, (e) =>
                {
                    LogError(e);
                });

            if (template == null)
            {
                throw new ApplicationException("Invalid template file!");
            }

            var fileToRemove = template.Files.FirstOrDefault(f => f.Src == FilePath);
            if (fileToRemove != null)
            {
                template.Files.Remove(fileToRemove);
                template.Connector.DeleteFile(FilePath);

                if (template.Connector is ICommitableFileConnector)
                {
                    ((ICommitableFileConnector)template.Connector).Commit();
                }

                // Determine the output file name and path
                var outFileName = System.IO.Path.GetFileName(Path);
                var outPath = new FileInfo(Path).DirectoryName;

                var fileSystemConnector = new FileSystemConnector(outPath, "");
                var formatter = XMLPnPSchemaFormatter.LatestFormatter;
                var extension = new FileInfo(Path).Extension.ToLowerInvariant();
                if (extension == ".pnp")
                {
                    XMLTemplateProvider provider = new XMLOpenXMLTemplateProvider(new OpenXMLConnector(outPath, fileSystemConnector));
                    var templateFileName = outFileName.Substring(0, outFileName.LastIndexOf(".", StringComparison.Ordinal)) + ".xml";

                    provider.SaveAs(template, templateFileName, formatter, TemplateProviderExtensions);
                }
                else
                {
                    XMLTemplateProvider provider = new XMLFileSystemTemplateProvider(Path, "");
                    provider.SaveAs(template, Path, formatter, TemplateProviderExtensions);
                }
            }
        }
    }
}
