using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Management.Automation;

using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Sites
{
    [Cmdlet(VerbsCommon.New, "SiteLevelFileVersionBatchDeleteJob")]
    public class NewSiteLevelFileVersionBatchDeleteJob : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true)]
        public int DeleteBeforeDays;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue("By executing this command, versions specified will be permanently deleted. These versions cannot be restored from the recycle bin. Are you sure you want to continue?", Resources.Confirm))
            {
                var site = ClientContext.Site;
                site.StartDeleteFileVersions(DeleteBeforeDays);
                ClientContext.ExecuteQueryRetry();

                WriteVerbose("Success. Versions specified will be permanently deleted in the upcoming days.");
            }
            else
            {
                WriteVerbose("Cancelled. No versions will be deleted.");
            }
        }
    }
}
