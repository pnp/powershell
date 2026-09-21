using PnP.Core.Provisioning.Connectors;
using PnP.Core.Provisioning.Providers.Xml;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections;
using System.IO;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    public partial class SetSiteTemplateMetadata
    {
        private void ExecuteCmdletExperimental()
        {
            if (ParameterSpecified(nameof(TemplateProviderExtensions)))
            {
                LogWarning("-TemplateProviderExtensions has no effect with -Experimental: template provider extensions are not run by the PnP.Core.Provisioning engine.");
            }

            var templateFileName = System.IO.Path.GetFileName(Path);
            var fileConnector = Path.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                ? BuildSharePointConnectorExperimental()
                : BuildFileSystemConnectorExperimental();

            bool isPackage;
            using (var stream = fileConnector.GetFileStream(templateFileName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"File {Path} does not exist.", Path);
                }
                isPackage = FileUtilities.IsOpenOfficeFile(stream);
            }

            XMLTemplateProvider provider;
            if (isPackage)
            {
                var openXmlConnector = new OpenXMLConnector(templateFileName, fileConnector);
                templateFileName = !string.IsNullOrEmpty(openXmlConnector.Info?.Properties?.TemplateFileName)
                    ? openXmlConnector.Info.Properties.TemplateFileName
                    : System.IO.Path.GetFileNameWithoutExtension(templateFileName) + ".xml";
                provider = new XMLOpenXMLTemplateProvider(openXmlConnector);
            }
            else
            {
                if (Path.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    throw new NotSupportedException("Only .pnp package files are supported from a SharePoint library");
                }
                provider = new XMLFileSystemTemplateProvider(new FileInfo(Path).DirectoryName, string.Empty);
            }

            var template = provider.GetTemplate(templateFileName);
            if (template == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(TemplateDisplayName))
            {
                template.DisplayName = TemplateDisplayName;
            }
            if (!string.IsNullOrEmpty(TemplateImagePreviewUrl))
            {
                template.ImagePreviewUrl = TemplateImagePreviewUrl;
            }
            if (TemplateProperties != null)
            {
                foreach (DictionaryEntry entry in TemplateProperties)
                {
                    var key = (string)entry.Key;
                    if (!string.IsNullOrEmpty(key))
                    {
                        template.Properties[key] = (string)entry.Value;
                    }
                }
            }

            provider.SaveAs(template, templateFileName);
        }

        private FileConnectorBase BuildFileSystemConnectorExperimental()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }
            return new FileSystemConnector(new FileInfo(Path).DirectoryName, string.Empty);
        }

        private FileConnectorBase BuildSharePointConnectorExperimental()
        {
            var siteUrl = PnPContext.Uri.ToString().TrimEnd('/');
            if (!Path.StartsWith(siteUrl + "/", StringComparison.OrdinalIgnoreCase))
            {
                throw new PSInvalidOperationException($"With -Experimental the template is read through the site you are connected to, and '{Path}' is not in '{siteUrl}'. Connect to the site holding the template and try again.");
            }

            var library = Path.Substring(siteUrl.Length).TrimStart('/');
            var separatorIndex = library.IndexOf("/", StringComparison.Ordinal);
            if (separatorIndex < 0)
            {
                throw new PSInvalidOperationException($"'{Path}' does not point at a file in a library of '{siteUrl}'.");
            }

            return new SharePointConnector(PnPContext, siteUrl, library.Substring(0, separatorIndex));
        }
    }
}
