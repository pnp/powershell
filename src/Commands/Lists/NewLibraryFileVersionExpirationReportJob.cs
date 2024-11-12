using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Management.Automation;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.New, "PnPLibraryFileVersionExpirationReportJob")]
    public class NewLibraryFileVersionExpirationReportJob : PnPWebCmdlet
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
                list.StartFileVersionExpirationReport(ReportUrl);
                ClientContext.ExecuteQueryRetry();

                WriteObject("Success. The file version expiration report will be gradually populated. It will take over 24 hours to complete for a small library, and a few days for a larger one.");
            }
        }
    }
}
