using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsLifecycle.Request, "PnPReIndexList")]
    [OutputType(typeof(void))]
    public class RequestReIndexList : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var list = Identity.GetList(CurrentWeb);

            if (list != null)
            {
                list.ReIndexList();
            }
         
        }
    }
}
