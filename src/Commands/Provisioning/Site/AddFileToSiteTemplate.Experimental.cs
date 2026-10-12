using PnP.Core.Provisioning.Connectors;
using PnP.Core.Provisioning.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using CoreFile = PnP.Core.Provisioning.Model.File;
using CoreFileLevel = PnP.Core.Provisioning.Model.FileLevel;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    public partial class AddFileToSiteTemplate
    {
        private void ProcessRecordExperimental()
        {
            if (ParameterSpecified(nameof(TemplateProviderExtensions)))
            {
                LogWarning("-TemplateProviderExtensions has no effect with -Experimental: template provider extensions are not run by the PnP.Core.Provisioning engine.");
            }

            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }

            var template = CoreProvisioningHelper.LoadSiteTemplateFromFile(Path, LogError);
            if (template == null)
            {
                throw new ApplicationException("Invalid template file!");
            }

            if (ParameterSetName == parameterSet_REMOTEFILE)
            {
                AddRemoteFileExperimental(template);
                return;
            }

            if (!System.IO.Path.IsPathRooted(Source))
            {
                Source = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Source);
            }

            using var sourceStream = System.IO.File.OpenRead(Source);
            Folder = Folder.Replace("\\", "/");
            AddFileToTemplateExperimental(template, sourceStream, Folder, System.IO.Path.GetFileName(Source), Container ?? string.Empty);
        }

        private void AddRemoteFileExperimental(ProvisioningTemplate template)
        {
            var webUrl = PnPContext.Uri.AbsolutePath.TrimEnd('/');
            var sourceUri = new Uri(SourceUrl, UriKind.RelativeOrAbsolute);
            var serverRelativeUrl =
                sourceUri.IsAbsoluteUri ? sourceUri.AbsolutePath :
                SourceUrl.StartsWith("/", StringComparison.Ordinal) ? SourceUrl :
                webUrl + "/" + SourceUrl;

            if (!serverRelativeUrl.StartsWith(webUrl + "/", StringComparison.OrdinalIgnoreCase))
            {
                throw new PSInvalidOperationException($"With -Experimental the file is read through the site you are connected to, and '{serverRelativeUrl}' is not in '{PnPContext.Uri}'. Connect to the site holding the file and try again.");
            }

            var fileName = System.IO.Path.GetFileName(serverRelativeUrl);
            var folderRelativeUrl = serverRelativeUrl.Substring(0, serverRelativeUrl.Length - fileName.Length - 1);
            var folderWebRelativeUrl = System.Net.WebUtility.UrlDecode(folderRelativeUrl.Substring(webUrl.Length + 1));

            try
            {
                var contents = PnPContext.Web.GetFileByServerRelativeUrl(serverRelativeUrl).GetContentBytes();
                using var sourceStream = new MemoryStream(contents);
                AddFileToTemplateExperimental(template, sourceStream, folderWebRelativeUrl, fileName, folderWebRelativeUrl);
            }
            catch (Exception exception)
            {
                LogWarning($"Can't add file from url {serverRelativeUrl} : {exception.Message}");
            }
        }

        private void AddFileToTemplateExperimental(ProvisioningTemplate template, Stream sourceStream, string folder, string fileName, string container)
        {
            var source = !string.IsNullOrEmpty(container) ? container + "/" + fileName : fileName;

            if (!System.IO.File.Exists(System.IO.Path.Combine(new FileInfo(Path).DirectoryName, source)))
            {
                template.Connector.SaveFileStream(fileName, container, sourceStream);
            }

            if (template.Connector is ICommitableFileConnector commitableConnector)
            {
                commitableConnector.Commit();
            }

            var existing = template.Files.FirstOrDefault(f => f.Src == $"{container}/{fileName}" && f.Folder == folder);
            if (existing != null)
            {
                template.Files.Remove(existing);
            }

            template.Files.Add(new CoreFile
            {
                Src = source,
                Folder = folder,
                Level = (CoreFileLevel)Enum.Parse(typeof(CoreFileLevel), FileLevel.ToString()),
                Overwrite = FileOverwrite
            });

            CoreProvisioningHelper.SaveSiteTemplateToFile(template, Path);
        }
    }
}
