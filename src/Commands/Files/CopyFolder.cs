using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Copy, "PnPFolder", DefaultParameterSetName = ParameterSet_WITHINM365)]
    public class CopyFolder : PnPWebCmdlet
    {
        private const string ParameterSet_WITHINM365 = "Copy files within Microsoft 365";
        private const string ParameterSet_FROMLOCAL = "Copy files from local to Microsoft 365";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_WITHINM365)]
        [Alias("ServerRelativeUrl")]
        public string SourceUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_FROMLOCAL)]
        public string LocalPath = string.Empty;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSet_WITHINM365)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSet_FROMLOCAL)]
        [Alias("TargetServerRelativeUrl")]
        public string TargetUrl = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_WITHINM365)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FROMLOCAL)]
        [Alias("OverwriteIfAlreadyExists")]
        public SwitchParameter Overwrite;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_WITHINM365)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FROMLOCAL)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_WITHINM365)]
        public SwitchParameter IgnoreVersionHistory;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_WITHINM365)]
        public SwitchParameter AllowSchemaMismatch;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_WITHINM365)]
        public SwitchParameter NoWait;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FROMLOCAL)]
        public SwitchParameter Recurse;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_FROMLOCAL)]
        public SwitchParameter RemoveAfterCopy;

        protected override void ExecuteCmdlet()
        {
            var webServerRelativeUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            switch(ParameterSetName)
            {
                case ParameterSet_FROMLOCAL:
                    // Copy a folder from local to Microsoft 365
                    CopyFromLocalToMicrosoft365();
                    break;
                
                case ParameterSet_WITHINM365:
                    // Copy a folder within Microsoft 365
                    CopyWithinMicrosoft365();
                    break;
            }
        }

        /// <summary>
        /// Copies a folder from local to Microsoft 365
        /// </summary>
        private void CopyFromLocalToMicrosoft365()
        {
            if (TargetUrl.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
            {
                // Remove the server part from the full FQDN making it server relative
                TargetUrl = TargetUrl[8..];
                TargetUrl = TargetUrl[TargetUrl.IndexOf('/')..];
            }

            if (!TargetUrl.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase) && TargetUrl.StartsWith(CurrentWeb.ServerRelativeUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                // Remove the server relative path making it web relative
                TargetUrl = TargetUrl[CurrentWeb.ServerRelativeUrl.Length..];
            }

            if (!System.IO.Path.IsPathRooted(LocalPath))
            {
                LocalPath = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, LocalPath);
            }

            // Normalize the path, taking out relative links and such to make it more readable
            LocalPath = System.IO.Path.GetFullPath(new Uri(LocalPath).LocalPath);

            if (!System.IO.Directory.Exists(LocalPath))
            {
                throw new PSArgumentException($"{nameof(LocalPath)} does not exist", nameof(LocalPath));
            }

            WriteVerbose($"Copying folder from local path {LocalPath} to Microsoft 365 location {UrlUtility.Combine(CurrentWeb.ServerRelativeUrl, TargetUrl)}");

            // Ensure the destination folder exists on Microsoft 365
            //WriteVerbose($"Ensuring Microsoft 365 location {TargetUrl} exists");
            //var folder = CurrentWeb.EnsureFolderPath(TargetUrl);

            WriteVerbose($"Retrieving local files {(Recurse.ToBool() ? "recursively " : "")}to upload from {LocalPath}");
            var filesToCopy = System.IO.Directory.GetFiles(LocalPath, string.Empty, Recurse.ToBool() ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly);

            WriteVerbose($"Uploading {filesToCopy.Length} file{(filesToCopy.Length != 1 ? "s" : "")}");

            // Start with the root
            string currentRemotePath = null;
            Folder folder = null;

            foreach(var fileToCopy in filesToCopy)
            {
                var fileName = System.IO.Path.GetFileName(fileToCopy);
                var relativeLocalPathWithFileName = System.IO.Path.GetRelativePath(LocalPath, fileToCopy);
                var relativePath = relativeLocalPathWithFileName.Remove(relativeLocalPathWithFileName.Length - fileName.Length);

                // Check if we're dealing with a different subfolder now as we did during the previous cycle
                if(relativePath != currentRemotePath)
                {
                    // New subfolder, ensure the folder exists remotely as well
                    currentRemotePath = relativePath;
                    
                    var newRemotePath = UrlUtility.Combine(TargetUrl, relativePath);
                    
                    WriteVerbose($"* Ensuring remote folder {newRemotePath}");
                    
                    folder = CurrentWeb.EnsureFolderPath(newRemotePath);
                }

                // Upload the file from local to remote
                WriteVerbose($"  * Uploading {fileToCopy} => {relativeLocalPathWithFileName}");
                try
                {
                    folder.UploadFile(fileName, fileToCopy, Overwrite.ToBool());

                    if(RemoveAfterCopy.ToBool())
                    {
                        WriteVerbose($"  * Removing {fileToCopy}");
                        System.IO.File.Delete(fileToCopy);
                    }
                }
                catch(Exception ex)
                {
                    WriteWarning($"* Upload failed: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Copies a folder within Microsoft 365
        /// </summary>
        private void CopyWithinMicrosoft365()
        {
            if (!TargetUrl.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase) && !TargetUrl.StartsWith("/"))
            {
                TargetUrl = UrlUtility.Combine(CurrentWeb.ServerRelativeUrl, TargetUrl);
            }

            if (!SourceUrl.StartsWith("/"))
            {
                SourceUrl = UrlUtility.Combine(CurrentWeb.ServerRelativeUrl, SourceUrl);
            } 

            WriteVerbose($"Copying folder within Microsoft 365 from {SourceUrl} to {TargetUrl}");

            string sourceFolder = SourceUrl.Substring(0, SourceUrl.LastIndexOf('/'));
            string targetFolder = TargetUrl;
            if (System.IO.Path.HasExtension(TargetUrl))
            {
                targetFolder = TargetUrl[..TargetUrl.LastIndexOf('/')];
            }
            Uri currentContextUri = new(ClientContext.Url);
            Uri sourceUri = new(currentContextUri, EncodePath(sourceFolder));
            Uri sourceWebUri = Web.WebUrlFromFolderUrlDirect(ClientContext, sourceUri);
            Uri targetUri = new(currentContextUri, EncodePath(targetFolder));
            Uri targetWebUri;
            if (TargetUrl.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
            {
                targetUri = new Uri(TargetUrl);
                targetWebUri = targetUri;
            }
            else
            {
                targetWebUri = Microsoft.SharePoint.Client.Web.WebUrlFromFolderUrlDirect(ClientContext, targetUri);
            }

            if (Force || ShouldContinue(string.Format(Resources.CopyFile0To1, SourceUrl, TargetUrl), Resources.Confirm))
            {
                if (sourceWebUri != targetWebUri)
                {
                    Copy(currentContextUri, sourceUri, targetUri, SourceUrl, TargetUrl, false, NoWait);
                }
                else
                {
                    var isFolder = false;
                    try
                    {
                        var folder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(TargetUrl));
                        var folderServerRelativePath = folder.EnsureProperty(f => f.ServerRelativePath);
                        isFolder = folderServerRelativePath.DecodedUrl == ResourcePath.FromDecodedUrl(TargetUrl).DecodedUrl;
                    }
                    catch
                    {
                    }
                    if (isFolder)
                    {
                        Copy(currentContextUri, sourceUri, targetUri, SourceUrl, TargetUrl, true, NoWait);
                    }
                    else
                    {
                        var file = CurrentWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(SourceUrl));
                        file.CopyToUsingPath(ResourcePath.FromDecodedUrl(TargetUrl), Overwrite);
                        ClientContext.ExecuteQueryRetry();
                    }
                }
            }
        }

        private string EncodePath(string path)
        {
            var parts = path.Split("/");
            return string.Join("/", parts.Select(p => Uri.EscapeDataString(p)));
        }
        
        private void Copy(Uri currentContextUri, Uri source, Uri destination, string sourceUrl, string targetUrl, bool sameWebCopyMoveOptimization, bool noWait)
        {
            if (!sourceUrl.StartsWith(source.ToString()))
            {
                sourceUrl = $"{source.Scheme}://{source.Host}/{sourceUrl.TrimStart('/')}";
            }
            if (!targetUrl.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase) && !targetUrl.StartsWith(destination.ToString()))
            {
                targetUrl = $"{destination.Scheme}://{destination.Host}/{targetUrl.TrimStart('/')}";
            }
            var results = Utilities.CopyMover.CopyAsync(HttpClient, ClientContext, currentContextUri, sourceUrl, targetUrl, IgnoreVersionHistory, Overwrite, AllowSchemaMismatch, sameWebCopyMoveOptimization, false, noWait).GetAwaiter().GetResult();
            if (NoWait)
            {
                WriteObject(results.jobInfo);
            }
            else
            {
                foreach (var log in results.logs)
                {
                    if (log.Event == "JobError")
                    {
                        WriteObject(log);
                    }
                }
            }
        }
    }
}
