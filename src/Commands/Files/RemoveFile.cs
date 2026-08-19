using PnP.Core.Model.SharePoint;
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
            string webUrl = null;
            var url = ServerRelativeUrl;

            if (ParameterSpecified(nameof(SiteRelativeUrl)))
            {
                Connection.PnPContext.Web.EnsureProperties(w => w.ServerRelativeUrl);

                webUrl = Connection.PnPContext.Web.ServerRelativeUrl;
                url = SiteRelativeUrl;
            }

            // Use the Url as provided when a file exists there, only fall back to its decoded form when it does not.
            IFile file = Utilities.FileUrlResolver.ResolveFile(url, webUrl, ClientContext, CurrentWeb, Connection.PnPContext, f => f.Name);

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
