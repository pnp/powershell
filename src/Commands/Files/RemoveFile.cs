using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Remove, "PnPFile")]
    public class RemoveFile : PnPWebCmdlet
    {
        private const string ParameterSet_SERVER = "Server Relative";
        private const string ParameterSet_SITE = "Site Relative";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_SERVER)]
        public string ServerRelativeUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_SITE)]
        public string SiteRelativeUrl = string.Empty;

        [Parameter(Mandatory = false)]
        public SwitchParameter Recycle;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterSet_SITE)
            {
                var webUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
                ServerRelativeUrl = UrlUtility.Combine(webUrl, SiteRelativeUrl);
            }
            var file = CurrentWeb.GetFileByServerRelativePath(ResourcePath.FromDecodedUrl(ServerRelativeUrl));
            ClientContext.Load(file, f => f.Name);
            ClientContext.ExecuteQueryRetry();

            if (Force || ShouldContinue(string.Format(Resources.Delete0, file.Name), Resources.Confirm))
            {
                if (Recycle)
                {
                    file.Recycle();
                }
                else
                {
                    file.DeleteObject();
                }

                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}
