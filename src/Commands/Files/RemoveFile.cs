using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Model.SharePoint;
using System.Management.Automation;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Remove, "PnPFile")]
    public class RemoveFile : PnPWebCmdlet
    {
        private const string ParameterSet_SERVER_Delete = "Delete by Server Relative";
        private const string ParameterSet_SITE_Delete = "Delete by Site Relative";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_SERVER_Delete)]
        [ValidateNotNullOrEmpty]
        public string ServerRelativeUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_SITE_Delete)]
        [ValidateNotNullOrEmpty]
        public string SiteRelativeUrl = string.Empty;

        [Parameter(Mandatory = false)]
        public SwitchParameter Recycle;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(SiteRelativeUrl)))
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
                    var recycleResult = file.Recycle();
                    ClientContext.ExecuteQueryRetry();
                    WriteObject(new RecycleResult { RecycleBinItemId = recycleResult.Value });
                }
                else
                {
                    file.DeleteObject();
                    ClientContext.ExecuteQueryRetry();
                }
            }
        }
    }
}
