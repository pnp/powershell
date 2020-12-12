using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Add, "PnPIndexedProperty")]
    public class AddIndexedProperty : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Key;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public ListPipeBind List;

        protected override void ExecuteCmdlet()
        {
            if (!string.IsNullOrEmpty(Key))
            {
                if (List != null)
                {
                    var list = List.GetList(SelectedWeb);
                    if (list != null)
                    {
                        list.AddIndexedPropertyBagKey(Key);
                    }
                }
                else
                {
                    SelectedWeb.AddIndexedPropertyBagKey(Key);
                }
            }
        }
    }
}
