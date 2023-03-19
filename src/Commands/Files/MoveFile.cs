using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Utilities;
using System;
using System.Linq;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Move, "PnPFile", DefaultParameterSetName = ParameterSet_SITE)]
    public class MoveFile : PnPWebCmdlet
    {
        private const string ParameterSet_SERVER = "Server Relative";
        private const string ParameterSet_SITE = "Site Relative";
        private const string ParameterSet_OTHERSITE = "Other Site Collection";
        
        [Parameter(Mandatory = true)]
        [Alias("ServerRelativeUrl", "SiteRelativeUrl")]
        public string SourceUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 1)]
        [Alias("TargetServerRelativeLibrary")]
        public string TargetUrl = string.Empty;

        [Parameter(Mandatory = false)]
        [Alias("OverwriteIfAlreadyExists")]
        public SwitchParameter Overwrite;

        [Parameter(Mandatory = false)]
        public SwitchParameter AllowSchemaMismatch;

        [Parameter(Mandatory = false)]
        public SwitchParameter AllowSmallerVersionLimitOnDestination;

        [Parameter(Mandatory = false)]
        public SwitchParameter IgnoreVersionHistory;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)] 
        public SwitchParameter NoWait;

        protected override void ExecuteCmdlet()
        {

            var webServerRelativeUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!SourceUrl.StartsWith("/"))
            {
                SourceUrl = UrlUtility.Combine(webServerRelativeUrl, SourceUrl);
            }
            if (!TargetUrl.StartsWith("/"))
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
            Uri sourceWebUri = Web.WebUrlFromFolderUrlDirect(ClientContext, sourceUri);
            Uri targetUri = new Uri(currentContextUri, EncodePath(targetFolder));
            Uri targetWebUri = Web.WebUrlFromFolderUrlDirect(ClientContext, targetUri);

            if (Force || ShouldContinue(string.Format(Resources.MoveFile0To1, SourceUrl, TargetUrl), Resources.Confirm))
            {
                if (sourceWebUri != targetWebUri)
                {
                    Move(currentContextUri, sourceUri, targetUri, SourceUrl, TargetUrl, false);
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
                    catch (Exception ex)
                    {
                        WriteVerbose(ex.Message);
                    }
                    if (isFolder)
                    {
                        Move(currentContextUri, sourceUri, targetUri, SourceUrl, TargetUrl, true);
                    }
                    else
                    {

                        var file = CurrentWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(SourceUrl));
                        file.MoveToUsingPath(ResourcePath.FromDecodedUrl(TargetUrl), Overwrite ? MoveOperations.Overwrite : MoveOperations.None);
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

        private void Move(Uri currentContextUri, Uri source, Uri destination, string sourceUrl, string targetUrl, bool sameWebCopyMoveOptimization)
        {
            if (!sourceUrl.StartsWith(source.ToString()))
            {
                sourceUrl = $"{source.Scheme}://{source.Host}/{sourceUrl.TrimStart('/')}";
            }
            if (!targetUrl.StartsWith(destination.ToString()))
            {
                targetUrl = $"{destination.Scheme}://{destination.Host}/{targetUrl.TrimStart('/')}";
            }
            var results = Utilities.CopyMover.MoveAsync(HttpClient, ClientContext, currentContextUri, sourceUrl, targetUrl, IgnoreVersionHistory, Overwrite, AllowSchemaMismatch, sameWebCopyMoveOptimization, AllowSmallerVersionLimitOnDestination, NoWait).GetAwaiter().GetResult();
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
