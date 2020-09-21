using System;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Utilities;
using File = Microsoft.SharePoint.Client.File;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Copy, "PnPFile", SupportsShouldProcess = true)]
    public class CopyFile : PnPWebCmdlet
    {
        private ProgressRecord _progressFolder = new ProgressRecord(0, "Activity", "Status") { Activity = "Copying folder" };
        private ProgressRecord _progressFile = new ProgressRecord(1, "Activity", "Status") { Activity = "Copying file" };
        private ClientContext _sourceContext;
        private ClientContext _targetContext;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [Alias("SiteRelativeUrl", "ServerRelativeUrl")] // Aliases are present to allow for switching between Move-PnPFile and Copy-PnPFile keeping the same parameters.
        public string SourceUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 1)]
        [Alias(nameof(MoveFile.TargetServerRelativeLibrary))] // Aliases is present to allow for switching between Move-PnPFile and Copy-PnPFile keeping the same parameters.
        public string TargetUrl = string.Empty;

        [Parameter(Mandatory = false)]
        public SwitchParameter OverwriteIfAlreadyExists;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipSourceFolderName;

        [Parameter(Mandatory = false)]
        public SwitchParameter IgnoreVersionHistory;

        protected override void ExecuteCmdlet()
        {
            var webServerRelativeUrl = SelectedWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!SourceUrl.StartsWith("/"))
            {
                SourceUrl = UrlUtility.Combine(webServerRelativeUrl, SourceUrl);
            }
            if (!TargetUrl.StartsWith("/"))
            {
                TargetUrl = UrlUtility.Combine(webServerRelativeUrl, TargetUrl);
            }

            Uri currentContextUri = new Uri(ClientContext.Url);
            Uri sourceUri = new Uri(currentContextUri, SourceUrl);
            Uri sourceWebUri = Microsoft.SharePoint.Client.Web.WebUrlFromFolderUrlDirect(ClientContext, sourceUri);
            Uri targetUri = new Uri(currentContextUri, TargetUrl);
            Uri targetWebUri = Microsoft.SharePoint.Client.Web.WebUrlFromFolderUrlDirect(ClientContext, targetUri);

            if (Force || ShouldContinue(string.Format(Resources.CopyFile0To1, SourceUrl, TargetUrl), Resources.Confirm))
            {
                if (sourceWebUri != targetWebUri)
                {
                    CopyToOtherSiteCollection(sourceUri, targetUri);
                }
                else
                {
                    CopyWithinSameSiteCollection(currentContextUri, sourceWebUri, targetWebUri);
                }
            }
        }

        /// <summary>
        /// Allows copying to within the same site collection
        /// </summary>
        private void CopyWithinSameSiteCollection(Uri currentContextUri, Uri sourceWebUri, Uri targetWebUri)
        {
            _sourceContext = ClientContext;
            if (!currentContextUri.AbsoluteUri.Equals(sourceWebUri.AbsoluteUri, StringComparison.InvariantCultureIgnoreCase))
            {
                _sourceContext = ClientContext.Clone(sourceWebUri);
            }

            bool isFile = true;
            bool srcIsFolder = false;

            File file = null;
            Folder folder = null;

            try
            {
                file = _sourceContext.Web.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(SourceUrl));
                file.EnsureProperties(f => f.Name, f => f.Exists);
                isFile = file.Exists;
            }
            catch
            {
                isFile = false;
            }

            if (!isFile)
            {
                folder = _sourceContext.Web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(SourceUrl));

                folder.EnsureProperties(f => f.Name, f => f.Exists);
                srcIsFolder = folder.Exists;
                folder.EnsureProperties(f => f.Name);

                try
                {
                    folder.EnsureProperties(f => f.ItemCount); //Using ItemCount as marker if this is a file or folder
                    srcIsFolder = true;
                }
                catch
                {
                    srcIsFolder = false;
                }
            }

            var srcWeb = _sourceContext.Web;
            srcWeb.EnsureProperty(s => s.Url);

            _targetContext = ClientContext.Clone(targetWebUri.AbsoluteUri);
            var dstWeb = _targetContext.Web;
            dstWeb.EnsureProperties(s => s.Url, s => s.ServerRelativeUrl);
            if (srcWeb.Url == dstWeb.Url)
            {
                try
                {
                    var targetFile = UrlUtility.Combine(TargetUrl, file?.Name);
                    // If src/dst are on the same Web, then try using CopyTo - backwards compability
                    file?.CopyToUsingPath(ResourcePath.FromDecodedUrl(targetFile), OverwriteIfAlreadyExists);
                    _sourceContext.ExecuteQueryRetry();
                    return;
                }
                catch
                {
                    SkipSourceFolderName = true; // target folder exist
                                                    //swallow exception, in case target was a lib/folder which exists
                }
            }

            //different site/site collection
            Folder targetFolder = null;
            string fileOrFolderName = null;
            bool targetFolderExists = false;
            try
            {
                targetFolder = _targetContext.Web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(TargetUrl));
                targetFolder.EnsureProperties(f => f.Name, f => f.Exists);
                if (!targetFolder.Exists) throw new Exception("TargetUrl is an existing file, not folder");
                targetFolderExists = true;
            }
            catch (Exception)
            {
                targetFolder = null;
                Expression<Func<List, object>> expressionRelativeUrl = l => l.RootFolder.ServerRelativeUrl;
                var query = _targetContext.Web.Lists.IncludeWithDefaultProperties(expressionRelativeUrl);
                var lists = _targetContext.LoadQuery(query);
                _targetContext.ExecuteQueryRetry();
                lists = lists.OrderByDescending(l => l.RootFolder.ServerRelativeUrl); // order descending in case more lists start with the same
                foreach (List targetList in lists)
                {
                    if (!TargetUrl.StartsWith(targetList.RootFolder.ServerRelativeUrl, StringComparison.InvariantCultureIgnoreCase)) continue;
                    fileOrFolderName = Regex.Replace(TargetUrl, _targetContext.Web.ServerRelativeUrl, "", RegexOptions.IgnoreCase).Trim('/');
                    targetFolder = srcIsFolder
                        ? _targetContext.Web.EnsureFolderPath(fileOrFolderName)
                        : targetList.RootFolder;
                    break;
                }
            }
            if (targetFolder == null) throw new Exception("Target does not exist");
            if (srcIsFolder)
            {
                if (!SkipSourceFolderName && targetFolderExists)
                {
                    targetFolder = targetFolder.EnsureFolder(folder.Name);
                }
                CopyFolder(folder, targetFolder);
            }
            else
            {
                UploadFile(file, targetFolder, fileOrFolderName);
            }
        }

        /// <summary>
        /// Allows copying to another site collection
        /// </summary>
        private void CopyToOtherSiteCollection(Uri source, Uri destination)
        {
            ClientContext.Site.CreateCopyJobs(new[] { source.ToString() }, destination.ToString(), new CopyMigrationOptions
            {
                IsMoveMode = false,
                IgnoreVersionHistory = IgnoreVersionHistory.ToBool(),
                NameConflictBehavior = OverwriteIfAlreadyExists.ToBool() ? MigrationNameConflictBehavior.Replace : MigrationNameConflictBehavior.Fail
            });
            ClientContext.ExecuteQueryRetry();
        }

        private void CopyFolder(Folder sourceFolder, Folder targetFolder)
        {
            sourceFolder.EnsureProperties(f => f.ServerRelativeUrl, f => f.Files, f => f.Folders, folder => folder.Files.Include(f => f.ServerRelativeUrl));
            targetFolder.EnsureProperty(f => f.ServerRelativeUrl);

            _progressFolder.RecordType = ProgressRecordType.Processing;
            _progressFolder.StatusDescription = $"{sourceFolder.ServerRelativeUrl} to {targetFolder.ServerRelativeUrl}";
            _progressFolder.PercentComplete = 0;
            WriteProgress(_progressFolder);

            _progressFile.RecordType = ProgressRecordType.Processing;
            foreach (File file in sourceFolder.Files)
            {
                _progressFile.StatusDescription = $"{file.ServerRelativeUrl}";
                _progressFile.PercentComplete = 0;
                WriteProgress(_progressFile);
                UploadFile(file, targetFolder);
                _progressFile.PercentComplete = 100;
                WriteProgress(_progressFile);
            }
            _progressFile.RecordType = ProgressRecordType.Completed;
            WriteProgress(_progressFile);

            _progressFolder.RecordType = ProgressRecordType.Processing;
            _progressFolder.PercentComplete = 100;
            WriteProgress(_progressFolder);

            foreach (Folder folder in sourceFolder.Folders)
            {
                var childFolder = targetFolder.EnsureFolder(folder.Name);
                CopyFolder(folder, childFolder);
            }
        }

        private void UploadFile(File srcFile, Folder targetFolder, string filename = "")
        {
            var binaryStream = srcFile.OpenBinaryStream();
            _sourceContext.ExecuteQueryRetry();
            if (string.IsNullOrWhiteSpace(filename))
            {
                filename = srcFile.Name;
            }
            this.UploadFileWithSpecialCharacters(targetFolder, filename, binaryStream.Value, OverwriteIfAlreadyExists);
            _targetContext.ExecuteQueryRetry();
        }

        private File UploadFileWithSpecialCharacters(Folder folder, string fileName, System.IO.Stream stream, bool overwriteIfExists)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("Filename is required");
            }

            // Create the file
            var newFileInfo = new FileCreationInformation()
            {
                ContentStream = stream,
                Url = fileName,
                Overwrite = overwriteIfExists
            };

            var file = folder.Files.Add(newFileInfo);
            folder.Context.Load(file);
            folder.Context.ExecuteQueryRetry();

            return file;
        }
    }
}
