using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model.SharePoint.BrandCenter;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Add, "PnPBrandCenterFontPackage")]
    [OutputType(typeof(void))]
    public class AddBrandCenterFontPackage : PnPWebCmdlet
    {
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = false)]
        public bool? Visible = true;

        [Parameter(Mandatory = true)]
        public Store Store { get; set; } = Store.Tenant;

        protected override void ExecuteCmdlet()
        {
            LogDebug("Creating a new font package in the brand center");
            CurrentWeb.EnsureProperty(w => w.Url);
            BrandCenterUtility.AddFontPackage(this, ClientContext, Store, CurrentWeb.Url, Title, Visible.GetValueOrDefault(true));
        }
    }
}
