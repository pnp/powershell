using System.Management.Automation;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Rename, "PnPFile")]
    public class RenameFile : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "SERVER")]
        public string ServerRelativeUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "SITE")]
        public string SiteRelativeUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 1)]
        public string TargetFileName = string.Empty;

        [Parameter(Mandatory = false)]
        public SwitchParameter OverwriteIfAlreadyExists;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            string webUrl = null;
            var url = ServerRelativeUrl;

            if (ParameterSetName == "SITE")
            {
                webUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
                url = SiteRelativeUrl;
            }

            // Use the Url as provided when a file exists there, only fall back to its decoded form when it does not.
            var serverRelativeUrl = Utilities.FileUrlResolver.Resolve(url, webUrl, ClientContext, CurrentWeb);

            // Moved through CSOM, as PnP Core turns a %20 still held by the path into a space before calling SharePoint
            var file = CurrentWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(serverRelativeUrl));
            ClientContext.Load(file, f => f.Name);
            ClientContext.ExecuteQueryRetry();

            if (Force || ShouldContinue(string.Format(Resources.RenameFile0To1, file.Name, TargetFileName), Resources.Confirm))
            {
                var targetPath = string.Concat(serverRelativeUrl.Substring(0, serverRelativeUrl.LastIndexOf('/') + 1), TargetFileName);

                file.MoveToUsingPath(ResourcePath.FromDecodedUrl(targetPath), OverwriteIfAlreadyExists ? MoveOperations.Overwrite : MoveOperations.None);
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}
