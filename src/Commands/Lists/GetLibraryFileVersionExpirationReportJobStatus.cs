using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;

using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPLibraryFileVersionExpirationReportJobStatus")]
    [OutputType(typeof(FileVersionExpirationReportJobStatus))]
    public class GetLibraryFileVersionExpirationReportJobStatus : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]        
        [ValidateNotNull]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string ReportUrl;

        protected override void ExecuteCmdlet()
        {
            var list = Identity.GetList(CurrentWeb);
            if (list != null)
            {
                var ret = list.GetProgressForFileVersionExpirationReport(ReportUrl);
                ClientContext.ExecuteQueryRetry();

                var status = JsonSerializer.Deserialize<FileVersionExpirationReportJobStatus>(ret.Value);
                status.Url = list.RootFolder.ServerRelativeUrl;
                status.ReportUrl = ReportUrl;

                WriteObject(status);
            }
        }
    }
}
