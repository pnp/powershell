using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint.BrandCenter;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsOther.Use, "PnPBrandCenterFontPackage")]
    [OutputType(typeof(void))]
    public class UseBrandCenterFontPackage : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ArgumentCompleter(typeof(BrandCenterFontPackageCompleter))]
        public BrandCenterFontPackagePipeBind Identity { get; set; }

        [Parameter(Mandatory = false)]
        public Store Store { get; set; } = Store.All;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.Url);

            LogDebug("Trying to retrieve the font with the provided identity from the Brand Center");
            var font = Identity.GetFontPackage(this, ClientContext, CurrentWeb.Url, Store) ?? throw new PSArgumentException($"The font with the provided identity was not found in the Brand Center. Please check the identity and try again.", nameof(Identity));

            if (font.IsValid.HasValue && font.IsValid.Value == false)
            {
                LogWarning($"The font with identity {font.Id} titled '{font.Title}' is not valid. Will try to apply it anyway.");
            }

            var url = $"{BrandCenterUtility.GetStoreFontPackageUrlByStoreType(font.Store, CurrentWeb.Url)}/GetById('{font.Id}')/Apply";
            LogDebug($"Applying font by making a POST call to {url}");
            RestHelper.Post(Connection.HttpClient, url, ClientContext);
        }
    }
}
