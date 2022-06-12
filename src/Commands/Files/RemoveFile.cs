using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Model.SharePoint;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Remove, "PnPFile")]
    public class RemoveFile : PnPWebCmdlet
    {
        private const string ParameterSet_SERVER_Delete = "Delete by Server Relative";
        private const string ParameterSet_SITE_Delete = "Delete by Site Relative";
        private const string ParameterSet_SERVER_Recycle = "Recycle by Server Relative";
        private const string ParameterSet_SITE_Recycle = "Recycle by Site Relative";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_SERVER_Delete)]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_SERVER_Recycle)]
        [ValidateNotNullOrEmpty]
        public string ServerRelativeUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_SITE_Delete)]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_SITE_Recycle)]
        [ValidateNotNullOrEmpty]
        public string SiteRelativeUrl = string.Empty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SERVER_Recycle)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SITE_Recycle)]
        public SwitchParameter Recycle;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (!string.IsNullOrEmpty(ServerRelativeUrl))
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
