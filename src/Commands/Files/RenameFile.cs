using System.Management.Automation;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Utilities;
using PnP.Core.Model.SharePoint;

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
            if (ParameterSetName == "SITE")
            {
                var pnpWeb = Connection.PnPContext.Web;
                pnpWeb.EnsureProperties(w => w.ServerRelativeUrl);

                ServerRelativeUrl = UrlUtility.Combine(pnpWeb.ServerRelativeUrl, SiteRelativeUrl);
            }

            IFile file = Connection.PnPContext.Web.GetFileByServerRelativeUrl(ServerRelativeUrl);
            file.EnsureProperties(f => f.Name, f => f.ServerRelativeUrl);

            if (Force || ShouldContinue(string.Format(Resources.RenameFile0To1, file.Name, TargetFileName), Resources.Confirm))
            {
                var targetPath = string.Concat(file.ServerRelativeUrl.Remove(file.ServerRelativeUrl.Length - file.Name.Length), TargetFileName);

                file.MoveTo(targetPath, OverwriteIfAlreadyExists ? MoveOperations.Overwrite : MoveOperations.None);
            }
        }
    }
}
