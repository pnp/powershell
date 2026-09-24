using PnP.Core.Provisioning.Connectors;
using PnP.Core.Provisioning.Model;
using PnP.Core.Provisioning.Model.Configuration;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.IO;
using System.Management.Automation;
using File = System.IO.File;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    public partial class ExportListToSiteTemplate
    {
        private void ExecuteCmdletExperimental()
        {
            if (string.IsNullOrEmpty(Out))
            {
                ExtractListsExperimental(SessionState.Path.CurrentFileSystemLocation.Path, null);
                return;
            }

            if (!Path.IsPathRooted(Out))
            {
                Out = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Out);
            }
            if (File.Exists(Out) && !Force && !ShouldContinue(string.Format(Resources.File0ExistsOverwrite, Out), Resources.Confirm))
            {
                return;
            }

            ExtractListsExperimental(new FileInfo(Out).DirectoryName, new FileInfo(Out).Name);
        }

        private void ExtractListsExperimental(string path, string packageName)
        {
            var extension = string.Empty;
            if (packageName != null)
            {
                if (packageName.IndexOf(".", StringComparison.Ordinal) > -1)
                {
                    extension = packageName.Substring(packageName.LastIndexOf(".", StringComparison.Ordinal)).ToLowerInvariant();
                }
                else
                {
                    packageName += ".pnp";
                    extension = ".pnp";
                }
            }

            var fileSystemConnector = new FileSystemConnector(path, string.Empty);
            var configuration = new ExtractConfiguration
            {
                Handlers = { ConfigurationHandler.Lists },
                FileConnector = extension == ".pnp"
                    ? new OpenXMLConnector(packageName, fileSystemConnector)
                    : fileSystemConnector
            };

            if (List != null)
            {
                foreach (var list in List)
                {
                    configuration.Lists.Lists.Add(new PnP.Core.Provisioning.Model.Configuration.Lists.Lists.ExtractListsListsConfiguration { Title = list });
                }
            }

            var reporter = new CoreProvisioningReporter($"Extracting lists from {PnPContext.Uri}", WriteProgress, LogWarning);
            configuration.ProgressDelegate = reporter.ProgressDelegate;
            configuration.MessagesDelegate = reporter.MessagesDelegate;

            var template = reporter.Run(() => PnPContext.GetProvisioningManager().GetTemplateAsync(configuration));

            if (OutputInstance)
            {
                WriteObject(template);
                return;
            }

            var formatter = CoreProvisioningHelper.GetFormatter(Schema);

            if (extension == ".pnp")
            {
                var provider = new PnP.Core.Provisioning.Providers.Xml.XMLOpenXMLTemplateProvider(configuration.FileConnector as OpenXMLConnector);
                var templateFileName = packageName.Substring(0, packageName.LastIndexOf(".", StringComparison.Ordinal)) + ".xml";
                provider.SaveAs(template, templateFileName, formatter);
            }
            else if (Out != null)
            {
                new PnP.Core.Provisioning.Providers.Xml.XMLFileSystemTemplateProvider(path, string.Empty)
                    .SaveAs(template, Path.Combine(path, packageName), formatter);
            }
            else
            {
                using var outputStream = formatter.ToFormattedTemplate(template);
                using var reader = new StreamReader(outputStream);
                WriteObject(reader.ReadToEnd());
            }
        }
    }
}
