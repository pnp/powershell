using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Management.Automation;

using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Sites
{
    [Cmdlet(VerbsCommon.New, "SiteLevelFileVersionExpirationReportJob")]
    public class NewSiteLevelFileVersionExpirationReportJob : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true)]
        public string ReportUrl;

        protected override void ExecuteCmdlet()
        {
            var site = ClientContext.Site;
            site.StartFileVersionExpirationReport(ReportUrl);
            ClientContext.ExecuteQueryRetry();

            WriteVerbose("Success. The file version expiration report will be gradually populated. It will take over 24 hours to complete for a small site, and a few days for a larger one.");
        }
    }
}
