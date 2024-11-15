
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Get, "PnPAvailablePageComponents")]
    public class GetAvailablePageComponents : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(PageCompleter))]
        public PagePipeBind Page;

        [Parameter(Mandatory = false)]
        public PageComponentPipeBind Component;

        protected override void ExecuteCmdlet()
        {
            var clientSidePage = Page.GetPage(Connection);
            if (clientSidePage == null)
                throw new PSArgumentException($"Page '{Page}' does not exist", "List");

            if (Component == null)
            {
                var allComponents = clientSidePage.AvailablePageComponents().Where(c => c.ComponentType == 1);
                WriteObject(allComponents, true);
            }
            else
            {
                WriteObject(Component.GetComponent(clientSidePage));
            }
        }
    }
}