
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Properties;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Remove, "PnPPage")]
    [Alias("Remove-PnPClientSidePage")]
    public class RemovePage : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public PagePipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public SwitchParameter Recycle;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue(Resources.RemoveClientSidePage, Resources.Confirm))
            {
                var clientSidePage = Identity.GetPage();
                if (clientSidePage == null)
                    throw new Exception($"Page '{Identity?.Name}' does not exist");

                if (Recycle.IsPresent)
                {
                    clientSidePage.PageListItem.Recycle();
                }
                else
                {
                    clientSidePage.Delete();
                }                
            }
        }
    }
}