using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Add, "PnPViewsFromXML")]
    [OutputType(typeof(void))]
    public class AddViewXML : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public string ViewsXML;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb);
            if (list != null)
            {
                list.CreateViewsFromXMLString(ViewsXML);
            }
        }
    }
}
