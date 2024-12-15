using Microsoft.SharePoint.Client;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Sites
{
    [Cmdlet(VerbsCommon.New, "PnPSiteFileVersionExpirationReportJob")]
    public class NewSiteFileVersionExpirationReportJob : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true)]
        public string ReportUrl;

        protected override void ExecuteCmdlet()
        {
            var site = ClientContext.Site;
            site.StartFileVersionExpirationReport(ReportUrl);
            ClientContext.ExecuteQueryRetry();

            WriteObject("Success. The file version expiration report will be gradually populated. It will take over 24 hours to complete for a small site, and a few days for a larger one.");
        }
    }
}
