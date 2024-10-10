using PnP.Core.Model.SharePoint;
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
                var pnpWeb = Connection.PnPContext.Web;
                pnpWeb.EnsureProperties(w => w.ServerRelativeUrl);

                ServerRelativeUrl = UrlUtility.Combine(pnpWeb.ServerRelativeUrl, SiteRelativeUrl);
            }

            IFile file = Connection.PnPContext.Web.GetFileByServerRelativeUrl(ServerRelativeUrl);
            file.EnsureProperties(f => f.Name);

            if (Force || ShouldContinue(string.Format(Resources.Delete0, file.Name), Resources.Confirm))
            {
                if (Recycle)
                {
                    var recycleResult = file.Recycle();
                    WriteObject(new RecycleResult { RecycleBinItemId = recycleResult });
                }
                else
                {
                    file.Delete();
                }
            }
        }
    }
}
