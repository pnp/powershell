
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ClientSidePages
{
    [Cmdlet(VerbsCommon.Get, "AvailableClientSideComponents")]
    public class GetAvailableClientSideComponents : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ClientSidePagePipeBind Page;

        [Parameter(Mandatory = false)]
        public ClientSideComponentPipeBind Component;

        protected override void ExecuteCmdlet()
        {
            var clientSidePage = Page.GetPage(ClientContext);
            if (clientSidePage == null)
                throw new PSArgumentException($"Page '{Page}' does not exist", "List");

            if (Component == null)
            {
                var allComponents = clientSidePage.AvailableClientSideComponents().Where(c => c.ComponentType == 1);
                WriteObject(allComponents, true);
            }
            else
            {
                WriteObject(Component.GetComponent(clientSidePage));
            }
        }
    }
}