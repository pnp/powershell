using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPIndexedProperties")]
    [OutputType(typeof(void))]
    public class SetIndexedProperties : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public List<string> Keys;

        protected override void ExecuteCmdlet()
        {
            if (Keys != null && Keys.Count > 0)
            {
                CurrentWeb.RemovePropertyBagValue("vti_indexedpropertykeys");

                foreach (var key in Keys)
                {
                    CurrentWeb.AddIndexedPropertyBagKey(key);
                }
            }
        }
    }
}
