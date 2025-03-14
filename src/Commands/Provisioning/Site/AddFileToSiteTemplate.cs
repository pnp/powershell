using Microsoft.SharePoint.Client;
using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers;
using PnP.Framework.Provisioning.Providers.Xml;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net;
using PnPFileLevel = PnP.Framework.Provisioning.Model.FileLevel;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    [Cmdlet(VerbsCommon.Add, "PnPFileToSiteTemplate")]
    public class AddFileToSiteTemplate : PnPWebCmdlet
    {
        const string parameterSet_LOCALFILE = "Local File";
        const string parameterSet_REMOTEFILE = "Remote File";

        [Parameter(Mandatory = true, Position = 0)]
        public string Path;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = parameterSet_LOCALFILE)]
        public string Source;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = parameterSet_REMOTEFILE)]
        public string SourceUrl;

        [Parameter(Mandatory = true, Position = 2, ParameterSetName = parameterSet_LOCALFILE)]
        public string Folder;

        [Parameter(Mandatory = false, Position = 3)]
        public string Container;

        [Parameter(Mandatory = false, Position = 4)]
        public PnPFileLevel FileLevel = PnPFileLevel.Published;

        [Parameter(Mandatory = false, Position = 5)]
        public SwitchParameter FileOverwrite = true;

        [Parameter(Mandatory = false, Position = 4)]
        public ITemplateProviderExtension[] TemplateProviderExtensions;

        protected override void ProcessRecord()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }
            // Load the template
            var template = ProvisioningHelper.LoadSiteTemplateFromFile(Path, TemplateProviderExtensions, (e) =>
                {
                    WriteError(new ErrorRecord(e, "TEMPLATENOTVALID", ErrorCategory.SyntaxError, null));
                });

            if (template == null)
            {
                throw new ApplicationException("Invalid template file!");
            }
            if (this.ParameterSetName == parameterSet_REMOTEFILE)
            {
                CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
                var sourceUri = new Uri(SourceUrl, UriKind.RelativeOrAbsolute);
                var serverRelativeUrl =
                    sourceUri.IsAbsoluteUri ? sourceUri.AbsolutePath :
                    SourceUrl.StartsWith("/", StringComparison.Ordinal) ? SourceUrl :
                    CurrentWeb.ServerRelativeUrl.TrimEnd('/') + "/" + SourceUrl;

                var file = CurrentWeb.GetFileByServerRelativeUrl(serverRelativeUrl);

                var fileName = file.EnsureProperty(f => f.Name);
                var folderRelativeUrl = serverRelativeUrl.Substring(0, serverRelativeUrl.Length - fileName.Length - 1);
                var folderWebRelativeUrl = System.Net.WebUtility.UrlDecode(folderRelativeUrl.Substring(CurrentWeb.ServerRelativeUrl.TrimEnd('/').Length + 1));
                if (ClientContext.HasPendingRequest) ClientContext.ExecuteQueryRetry();
                try
                {
                    var fi = CurrentWeb.GetFileByServerRelativeUrl(serverRelativeUrl);
                    var fileStream = fi.OpenBinaryStream();
                    ClientContext.ExecuteQueryRetry();
                    using (var ms = fileStream.Value)
                    {
                        AddFileToTemplate(template, ms, folderWebRelativeUrl, fileName, folderWebRelativeUrl);
                    }
                }
                catch (WebException exc)
                {
                    LogWarning($"Can't add file from url {serverRelativeUrl} : {exc}");
                }
            }
            else
            {
                if (!System.IO.Path.IsPathRooted(Source))
                {
                    Source = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Source);
                }

                // Load the file and add it to the .PNP file
                using (var fs = System.IO.File.OpenRead(Source))
                {
                    Folder = Folder.Replace("\\", "/");

                    var fileName = Source.IndexOf("\\", StringComparison.Ordinal) > 0 ? Source.Substring(Source.LastIndexOf("\\") + 1) : Source;
                    var container = !string.IsNullOrEmpty(Container) ? Container : string.Empty;
                    AddFileToTemplate(template, fs, Folder, fileName, container);
                }
            }
        }

        private void AddFileToTemplate(ProvisioningTemplate template, Stream fs, string folder, string fileName, string container)
        {
            var source = !string.IsNullOrEmpty(container) ? (container + "/" + fileName) : fileName;

            //See if sourcefile already is in same directory as template, if so we dont need to save it again
            if (!System.IO.File.Exists(System.IO.Path.Combine(new FileInfo(Path).DirectoryName, source)))
            {
                template.Connector.SaveFileStream(fileName, container, fs);
            }

            if (template.Connector is ICommitableFileConnector connector)
            {
                connector.Commit();
            }

            var existing = template.Files.FirstOrDefault(f =>
              f.Src == $"{container}/{fileName}"
              && f.Folder == folder);

            if (existing != null)
                template.Files.Remove(existing);

            var newFile = new PnP.Framework.Provisioning.Model.File
            {
                Src = source,
                Folder = folder,
                Level = FileLevel,
                Overwrite = FileOverwrite,
            };

            template.Files.Add(newFile);

            // Determine the output file name and path
            var outFileName = System.IO.Path.GetFileName(Path);
            var outPath = new FileInfo(Path).DirectoryName;

            var fileSystemConnector = new FileSystemConnector(outPath, "");
            var formatter = XMLPnPSchemaFormatter.LatestFormatter;
            var extension = new FileInfo(Path).Extension.ToLowerInvariant();
            if (extension == ".pnp")
            {
                var provider = new XMLOpenXMLTemplateProvider(template.Connector as OpenXMLConnector);
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