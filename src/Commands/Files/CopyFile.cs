using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Alias("Copy-PnPFolder")]
    [Cmdlet(VerbsCommon.Copy, "PnPFile")]
    public class CopyFile : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [Alias("ServerRelativeUrl")]
        public string SourceUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 1)]
        [Alias("TargetServerRelativeUrl")]
        public string TargetUrl = string.Empty;

        [Parameter(Mandatory = false)]
        [Alias("OverwriteIfAlreadyExists")]
        public SwitchParameter Overwrite;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public SwitchParameter IgnoreVersionHistory;

        [Parameter(Mandatory = false)]
        public SwitchParameter AllowSchemaMismatch;

        [Parameter(Mandatory = false)]
        public SwitchParameter NoWait;

        protected override void ExecuteCmdlet()
        {
            var webServerRelativeUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!SourceUrl.StartsWith("/"))
            {
                SourceUrl = UrlUtility.Combine(webServerRelativeUrl, SourceUrl);
            }            
            if (!TargetUrl.StartsWith("https://") && !TargetUrl.StartsWith("/"))
            {
                TargetUrl = UrlUtility.Combine(webServerRelativeUrl, TargetUrl);
            }

            string sourceFolder = SourceUrl.Substring(0, SourceUrl.LastIndexOf('/'));
            string targetFolder = TargetUrl;
            if (System.IO.Path.HasExtension(TargetUrl))
            {
                targetFolder = TargetUrl.Substring(0, TargetUrl.LastIndexOf('/'));
            }
            Uri currentContextUri = new Uri(ClientContext.Url);
            Uri sourceUri = new Uri(currentContextUri, EncodePath(sourceFolder));
            Uri sourceWebUri = Microsoft.SharePoint.Client.Web.WebUrlFromFolderUrlDirect(ClientContext, sourceUri);
            Uri targetUri = new Uri(currentContextUri, EncodePath(targetFolder));
            Uri targetWebUri;
            if (TargetUrl.StartsWith("https://"))
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
            if (!targetUrl.StartsWith("https://") && !targetUrl.StartsWith(destination.ToString()))
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
