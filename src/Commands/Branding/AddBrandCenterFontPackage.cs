using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
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
        public string Name;

        [Parameter(Mandatory = true)]
        public BrandCenterFontPipeBind DisplayFont { get; set; }

        [Parameter(Mandatory = true)]
        public BrandCenterFontPipeBind ContentFont { get; set; }

        [Parameter(Mandatory = true)]
        public BrandCenterFontPipeBind TitleFont { get; set; }

        [Parameter(Mandatory = true)]
        public string TitleFontStyle { get; set; }

        [Parameter(Mandatory = true)]
        public BrandCenterFontPipeBind HeadlineFont { get; set; }

        [Parameter(Mandatory = true)]
        public string HeadlineFontStyle { get; set; }

        [Parameter(Mandatory = true)]
        public BrandCenterFontPipeBind BodyFont { get; set; }

        [Parameter(Mandatory = true)]
        public string BodyFontStyle { get; set; }

        [Parameter(Mandatory = true)]
        public BrandCenterFontPipeBind InteractiveFont { get; set; }

        [Parameter(Mandatory = true)]
        public string InteractiveFontStyle { get; set; }

        [Parameter(Mandatory = false)]
        public bool? Visible = true;

        [Parameter(Mandatory = false)]
        public Store Store { get; set; } = Store.Tenant;

        protected override void ExecuteCmdlet()
        {
            LogDebug("Creating a new font package in the brand center");
            CurrentWeb.EnsureProperty(w => w.Url);
            var fontPackage = BrandCenterUtility.AddFontPackage(this, ClientContext, Store, CurrentWeb.Url, Name, DisplayFont.GetFont(this, ClientContext), ContentFont.GetFont(this, ClientContext), TitleFont.GetFont(this, ClientContext), TitleFontStyle, HeadlineFont.GetFont(this, ClientContext), HeadlineFontStyle, BodyFont.GetFont(this, ClientContext), BodyFontStyle, InteractiveFont.GetFont(this, ClientContext), InteractiveFontStyle, Visible.GetValueOrDefault(true));

            WriteObject(fontPackage);
        }
    }
}
