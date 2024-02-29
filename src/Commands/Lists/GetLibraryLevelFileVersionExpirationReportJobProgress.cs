using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Management.Automation;

using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "LibraryLevelFileVersionExpirationReportJobProgress")]
    public class GetLibraryLevelFileVersionExpirationReportJobProgress : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]        
        [ValidateNotNull]
        public ListPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string ReportUrl;

        protected override void ExecuteCmdlet()
        {
            var list = Identity.GetList(CurrentWeb);
            if (list != null)
            {
                var status = list.GetProgressForFileVersionExpirationReport(ReportUrl);
                ClientContext.ExecuteQueryRetry();
                WriteObject(status);
            }
        }
    }
}
