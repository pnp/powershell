using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Management.Automation;

using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.New, "LibraryLevelFileVersionBatchDeleteJob")]
    public class NewLibraryLevelFileVersionBatchDeleteJob : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]        
        [ValidateNotNull]
        public ListPipeBind Identity;

        [Parameter(Mandatory = true)]
        public int DeleteBeforeDays;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue("By executing this command, versions specified will be permanently deleted. These versions cannot be restored from the recycle bin. Are you sure you want to continue?", Resources.Confirm))
            {
                var list = Identity.GetList(CurrentWeb);
                if (list != null)
                {
                    list.StartDeleteFileVersions(DeleteBeforeDays);
                    ClientContext.ExecuteQueryRetry();
                }

                WriteVerbose("Success. Versions specified will be permanently deleted in the upcoming days.");
            }
            else
            {
                WriteVerbose("Cancelled. No versions will be deleted.");
            }
        }
    }
}
