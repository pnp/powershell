using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Management.Automation;

using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.New, "LibraryLevelFileVersionExpirationReportJob")]
    public class NewLibraryLevelFileVersionExpirationReportJob : PnPWebCmdlet
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
                list.StartFileVersionExpirationReport(ReportUrl);
                ClientContext.ExecuteQueryRetry();

                WriteVerbose("Success. The file version expiration report will be gradually populated. It will take over 24 hours to complete for a small library, and a few days for a larger one.");
            }
        }
    }
}
