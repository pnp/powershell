using PnP.Core.Provisioning.Connectors;
using PnP.Core.Provisioning.Model;
using PnP.Core.Provisioning.Providers.Xml;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.IO;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    public partial class SaveTenantTemplate
    {
        private void ProcessRecordExperimental()
        {
            var templateObject = Template.GetCoreHierarchy(SessionState.Path.CurrentFileSystemLocation.Path, LogError);
            if (templateObject == null)
            {
                LogError("No tenant template to save. Pass a tenant template object or the path to a template file.");
                return;
            }

            var outFileName = Path.GetFileName(Out);
            if (!Path.IsPathRooted(Out))
            {
                Out = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Out);
            }
            if (System.IO.File.Exists(Out))
            {
                if (!Force && !ShouldContinue(string.Format(Properties.Resources.File0ExistsOverwrite, Out), Properties.Resources.Confirm))
                {
                    return;
                }
                System.IO.File.Delete(Out);
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
            WriteObject("Processing template");
            new XMLOpenXMLTemplateProvider(Out, fileSystemConnector, templateFileName: templateFileName)
                .SaveAs(templateObject, templateFileName, formatter);
            ProcessFilesExperimental(templateObject, Out, fileSystemConnector);
        }

        private void ProcessFilesExperimental(ProvisioningHierarchy tenantTemplate, string packagePath, FileConnectorBase fileSystemConnector)
        {
            var packagedTemplate = CoreProvisioningHelper.LoadTenantTemplateFromFile(packagePath, LogError);
            if (packagedTemplate == null)
            {
                return;
            }

            if (tenantTemplate.Tenant?.AppCatalog != null)
            {
                foreach (var app in tenantTemplate.Tenant.AppCatalog.Packages)
                {
                    WriteObject($"Processing {app.Src}");
                    AddFileExperimental(app.Src, packagedTemplate, fileSystemConnector);
                }
            }
            if (tenantTemplate.Tenant?.SiteScripts != null)
            {
                foreach (var siteScript in tenantTemplate.Tenant.SiteScripts)
                {
                    WriteObject($"Processing {siteScript.JsonFilePath}");
                    AddFileExperimental(siteScript.JsonFilePath, packagedTemplate, fileSystemConnector);
                }
            }
            if (tenantTemplate.Localizations != null)
            {
                foreach (var localization in tenantTemplate.Localizations)
                {
                    WriteObject($"Processing {localization.ResourceFile}");
                    AddFileExperimental(localization.ResourceFile, packagedTemplate, fileSystemConnector);
                }
            }

            foreach (var template in tenantTemplate.Templates)
            {
                if (!string.IsNullOrEmpty(template.WebSettings?.SiteLogo))
                {
                    var isFile = false;
                    try
                    {
                        using var fileStream = fileSystemConnector.GetFileStream(template.WebSettings.SiteLogo);
                        isFile = fileStream != null;
                    }
                    catch (Exception exception)
                    {
                        LogDebug($"'{template.WebSettings.SiteLogo}' is not a file next to the template: {exception.Message}");
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

                foreach (var list in template.Lists)
                {
                    foreach (var dataRow in list.DataRows)
                    {
                        if (dataRow.Attachments.Count == 0)
                        {
                            continue;
                        }
                        WriteObject("List attachments");
                        foreach (var attachment in dataRow.Attachments)
                        {
                            AddFileExperimental(attachment.Src, packagedTemplate, fileSystemConnector);
                        }
                    }
                }
            }

            if (packagedTemplate.Connector is ICommitableFileConnector commitableConnector)
            {
                commitableConnector.Commit();
            }
        }

        private static void AddFileExperimental(string sourceName, ProvisioningHierarchy hierarchy, FileConnectorBase fileSystemConnector)
        {
            using var fileStream = fileSystemConnector.GetFileStream(sourceName);
            if (fileStream == null)
            {
                return;
            }

            var separatorIndex = sourceName.LastIndexOfAny(new[] { '\\', '/' });
            var fileName = separatorIndex > -1 ? sourceName.Substring(separatorIndex + 1) : sourceName;
            var folderName = separatorIndex > -1 ? sourceName.Substring(0, separatorIndex) : string.Empty;
            hierarchy.Connector.SaveFileStream(fileName, folderName, fileStream);
        }
    }
}
