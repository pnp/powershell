using Microsoft.SharePoint.Client;
using System.Management.Automation;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Sites
{
    [Cmdlet(VerbsCommon.Remove, "PnPSiteFileVersionBatchDeleteJob")]
    public class RemoveSiteFileVersionBatchDeleteJob : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue("It will stop processing further version deletion batches. Are you sure you want to continue?", Resources.Confirm))
            {
                var site = ClientContext.Site;
                site.CancelDeleteFileVersions();
                ClientContext.ExecuteQueryRetry();

                WriteObject("Future deletion is successfully stopped.");
            }
            else
            {
                WriteObject("Did not receive confirmation to stop deletion. Continuing to delete specified versions.");
            }
        }
    }
}
