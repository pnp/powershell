using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Publishing
{
    [Cmdlet(VerbsCommon.Set, "PnPAvailablePageLayouts")]
    public class SetAvailablePageLayouts : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "SPECIFIC")]
        public string[] PageLayouts;

        [Parameter(Mandatory = true, ParameterSetName = "ALL")]
        public SwitchParameter AllowAllPageLayouts;

        [Parameter(Mandatory = true, ParameterSetName = "INHERIT")]
        public SwitchParameter InheritPageLayouts;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == "SPECIFIC")
            {
                if (PageLayouts.Length < 1) throw new ArgumentException("You must provide at least 1 page layout.");

                var rootWeb = ClientContext.Site.RootWeb;
                CurrentWeb.SetAvailablePageLayouts(rootWeb, PageLayouts);
            }
            else if (ParameterSetName == "INHERIT")
            {
                CurrentWeb.SetSiteToInheritPageLayouts();
            }
            else
            {
                CurrentWeb.AllowAllPageLayouts();
            }
        }
    }
}
