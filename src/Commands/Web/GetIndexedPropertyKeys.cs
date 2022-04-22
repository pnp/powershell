﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPIndexedPropertyKeys")]
    [OutputType(typeof(string))]
    public class GetIndexedProperties : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public ListPipeBind List;

        protected override void ExecuteCmdlet()
        {
            if (List != null)
            {
                var list = List.GetList(CurrentWeb);
                if (list != null)
                {
                    var keys = list.GetIndexedPropertyBagKeys();
                    WriteObject(keys);
                }
            }
            else
            {
                var keys = CurrentWeb.GetIndexedPropertyBagKeys();
                WriteObject(keys);
            }
        }
    }
}
