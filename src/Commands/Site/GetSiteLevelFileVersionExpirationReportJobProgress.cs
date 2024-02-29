using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Management.Automation;

namespace PnP.PowerShell.Commands.Sites
{
    [Cmdlet(VerbsCommon.Get, "SiteLevelFileVersionExpirationReportJobProgress")]
    [OutputType(typeof(string))]
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
