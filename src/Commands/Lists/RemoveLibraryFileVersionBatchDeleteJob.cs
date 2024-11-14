using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Management.Automation;

using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Remove, "PnPLibraryFileVersionBatchDeleteJob")]
    public class RemoveLibraryFileVersionBatchDeleteJob : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]        
        [ValidateNotNull]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue("It will stop processing further version deletion batches. Are you sure you want to continue?", Resources.Confirm))
            {
                var list = Identity.GetList(CurrentWeb);
                if (list != null)
                {
                    list.CancelDeleteFileVersions();
                    ClientContext.ExecuteQueryRetry();
                }

                WriteObject("Future deletion is successfully stopped.");
            }
            else
            {
                WriteObject("Did not receive confirmation to stop deletion. Continuing to delete specified versions.");
            }
        }
    }
}
