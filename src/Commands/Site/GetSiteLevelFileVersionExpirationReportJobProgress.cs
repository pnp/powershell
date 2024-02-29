using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Management.Automation;

using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Sites
{
    [Cmdlet(VerbsCommon.Get, "SiteLevelFileVersionExpirationReportJobProgress")]
    public class GetSiteLevelFileVersionExpirationReportJobProgress : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true)]
        public string ReportUrl;

        protected override void ExecuteCmdlet()
        {
            var site = ClientContext.Site;
            var status = site.GetProgressForFileVersionExpirationReport(ReportUrl);
            ClientContext.ExecuteQueryRetry();
            WriteObject(status);
        }
    }
}
