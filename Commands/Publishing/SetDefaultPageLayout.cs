using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;

namespace PnP.PowerShell.Commands.Publishing
{
    [Cmdlet(VerbsCommon.Set, "PnPDefaultPageLayout")]
    public class SetDefaultPageLayout : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "TITLE")]
        public string Title = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = "INHERIT")]
        public SwitchParameter InheritFromParentSite;

        protected override void ExecuteCmdlet()
        {
            if (InheritFromParentSite.IsPresent)
            {
                SelectedWeb.SetSiteToInheritPageLayouts();
            }
            else
            {
                var rootWeb = ClientContext.Site.RootWeb;
                SelectedWeb.SetDefaultPageLayoutForSite(rootWeb, Title);
            }
        }
    }
}
