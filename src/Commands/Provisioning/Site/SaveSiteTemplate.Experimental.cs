using PnP.Core.Provisioning.Connectors;
using PnP.Core.Provisioning.Model;
using PnP.Core.Provisioning.Providers.Xml;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.IO;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning
{
    public partial class SaveSiteTemplate
    {
        private void ProcessRecordExperimental()
        {
            if (ParameterSpecified(nameof(TemplateProviderExtensions)))
            {
                LogWarning("-TemplateProviderExtensions has no effect with -Experimental: template provider extensions are not run by the PnP.Core.Provisioning engine.");
            }

            var templateObject = Template.GetCoreTemplate(SessionState.Path.CurrentFileSystemLocation.Path, LogError);
            if (templateObject == null)
            {
                LogError("No template to save. Pass a template object or the path to a template file.");
                return;
            }

            var outFileName = Path.GetFileName(Out);
            if (!Path.IsPathRooted(Out))
            {
                Out = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Out);
            }
            if (System.IO.File.Exists(Out) && !Force && !ShouldContinue(string.Format(Properties.Resources.File0ExistsOverwrite, Out), Properties.Resources.Confirm))
            {
                return;
            }

            var outPath = new FileInfo(Out).DirectoryName;
            var extension = outFileName != null && outFileName.IndexOf(".", StringComparison.Ordinal) > -1
                ? outFileName.Substring(outFileName.LastIndexOf(".", StringComparison.Ordinal)).ToLowerInvariant()
                : ".pnp";

            var fileSystemConnector = new FileSystemConnector(outPath, string.Empty);
            var formatter = CoreProvisioningHelper.GetFormatter(Schema);

            if (extension != ".pnp")
            {
                new XMLFileSystemTemplateProvider(outPath, string.Empty).SaveAs(templateObject, Out, formatter);
                return;
            }

            var templateFileName = outFileName.Substring(0, outFileName.LastIndexOf(".", StringComparison.Ordinal)) + ".xml";
            new XMLOpenXMLTemplateProvider(Out, fileSystemConnector).SaveAs(templateObject, templateFileName, formatter);
            ProcessFilesExperimental(templateObject, Out, fileSystemConnector);
        }

        private void ProcessFilesExperimental(ProvisioningTemplate template, string packagePath, FileConnectorBase fileSystemConnector)
        {
            var packagedTemplate = CoreProvisioningHelper.LoadSiteTemplateFromFile(packagePath, LogError);
            if (packagedTemplate == null)
            {
                return;
            }

            if (template.Tenant?.AppCatalog != null)
            {
                foreach (var app in template.Tenant.AppCatalog.Packages)
                {
                    WriteObject($"Processing {app.Src}");
                    AddFileExperimental(app.Src, packagedTemplate, fileSystemConnector);
                }
            }
            if (template.Tenant?.SiteScripts != null)
            {
                foreach (var siteScript in template.Tenant.SiteScripts)
                {
                    WriteObject($"Processing {siteScript.JsonFilePath}");
                    AddFileExperimental(siteScript.JsonFilePath, packagedTemplate, fileSystemConnector);
                }
            }
            if (template.Localizations != null)
            {
                foreach (var localization in template.Localizations)
                {
                    WriteObject($"Processing {localization.ResourceFile}");
                    AddFileExperimental(localization.ResourceFile, packagedTemplate, fileSystemConnector);
                }
            }
            if (!string.IsNullOrEmpty(template.WebSettings?.SiteLogo))
            {
                bool isFile;
                using (var fileStream = fileSystemConnector.GetFileStream(template.WebSettings.SiteLogo))
                {
                    isFile = fileStream != null;
                }
                if (isFile)
                {
                    WriteObject($"Processing {template.WebSettings.SiteLogo}");
                    AddFileExperimental(template.WebSettings.SiteLogo, packagedTemplate, fileSystemConnector);
                }
            }
            foreach (var file in template.Files)
            {
                WriteObject($"Processing {file.Src}");
                AddFileExperimental(file.Src, packagedTemplate, fileSystemConnector);
            }

            if (packagedTemplate.Connector is ICommitableFileConnector commitableConnector)
            {
                commitableConnector.Commit();
            }
        }

        private static void AddFileExperimental(string sourceName, ProvisioningTemplate template, FileConnectorBase fileSystemConnector)
        {
            using var fileStream = fileSystemConnector.GetFileStream(sourceName);
            if (fileStream == null)
            {
                return;
            }

            var separatorIndex = sourceName.LastIndexOfAny(new[] { '\\', '/' });
            var fileName = separatorIndex > -1 ? sourceName.Substring(separatorIndex + 1) : sourceName;
            var folderName = separatorIndex > -1 ? sourceName.Substring(0, separatorIndex) : string.Empty;
            template.Connector.SaveFileStream(fileName, folderName, fileStream);
        }
    }
}
