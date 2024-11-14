using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "PnPIndexedProperty")]
    [OutputType(typeof(void))]
    public class RemovedIndexedProperty : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Key;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;
        protected override void ExecuteCmdlet()
        {
            if (!string.IsNullOrEmpty(Key))
            {
                if (List != null)
                {
                    var list = List.GetList(CurrentWeb);
                    if (list != null)
                    {
                        list.RemoveIndexedPropertyBagKey(Key);
                    }
                }
                else
                {
                    CurrentWeb.RemoveIndexedPropertyBagKey(Key);
                }
            }
        }
    }
}
