using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Management.Automation;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "LibraryLevelFileVersionExpirationReportJobProgress")]
    [OutputType(typeof(string))]
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
