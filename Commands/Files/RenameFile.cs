using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Rename, "PnPFile", SupportsShouldProcess = true)]
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
            if (ParameterSetName == "SITE")
            {
                var webUrl = SelectedWeb.EnsureProperty(w => w.ServerRelativeUrl);
                ServerRelativeUrl = UrlUtility.Combine(webUrl, SiteRelativeUrl);
            }
            var file = SelectedWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(ServerRelativeUrl));

            ClientContext.Load(file, f => f.Name, f => f.ServerRelativePath);
            ClientContext.ExecuteQueryRetry();

            if (Force || ShouldContinue(string.Format(Resources.RenameFile0To1, file.Name, TargetFileName), Resources.Confirm))
            {
                var targetPath = string.Concat(file.ServerRelativePath.DecodedUrl.Remove(file.ServerRelativePath.DecodedUrl.Length - file.Name.Length), TargetFileName);
                file.MoveToUsingPath(ResourcePath.FromDecodedUrl(targetPath), OverwriteIfAlreadyExists ? MoveOperations.Overwrite : MoveOperations.None);

                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}
