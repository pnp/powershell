
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Remove, "PnPPageComponent")]
    public class RemovePageComponent : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public PagePipeBind Page;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Guid InstanceId;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var clientSidePage = Page.GetPage(Connection);

            if (clientSidePage == null)
                throw new Exception($"Page '{Page?.Name}' does not exist");

            var control = clientSidePage.Controls.FirstOrDefault(c => c.InstanceId == InstanceId);
            if (control != null)
            {
                if (Force || ShouldContinue(string.Format(Properties.Resources.RemoveComponentWithInstanceId0, control.InstanceId), Properties.Resources.Confirm))
                {
                    control.Delete();
                    clientSidePage.Save();
                }
            }
            else
            {
                throw new Exception($"Component with id {InstanceId} does not exist on this page");
            }
        }
    }
}