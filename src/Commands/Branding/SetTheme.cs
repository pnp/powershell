using System.Management.Automation;
using System.Text.Json;
using Microsoft.SharePoint.Client;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Set, "PnPTheme")]  
    public class SetTheme : PnPWebCmdlet
    {
        private const string PROPBAGKEY = "_PnP_ProvisioningTemplateComposedLookInfo";

        [Parameter(Mandatory = false)]
        public string ColorPaletteUrl;

        [Parameter(Mandatory = false)]
        public string FontSchemeUrl = null;

        [Parameter(Mandatory = false)]
        public string BackgroundImageUrl = null;

        [Parameter(Mandatory = false)]
        public SwitchParameter ResetSubwebsToInherit = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter UpdateRootWebOnly = false;


        protected override void ExecuteCmdlet()
        {
            var rootWebServerRelativeUrl = (CurrentWeb.Context as ClientContext).Site.RootWeb.EnsureProperty(r => r.ServerRelativeUrl);
            var serverRelativeUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
            if (ColorPaletteUrl == null)
            {
                ColorPaletteUrl = "/_catalogs/theme/15/palette001.spcolor";
            }

            if (!ColorPaletteUrl.ToLower().StartsWith(rootWebServerRelativeUrl.ToLower()))
            {
                ColorPaletteUrl = UrlUtility.Combine(rootWebServerRelativeUrl, ColorPaletteUrl);
            }

            if (!string.IsNullOrEmpty(FontSchemeUrl) && !FontSchemeUrl.ToLower().StartsWith(rootWebServerRelativeUrl.ToLower()))
            {
                FontSchemeUrl = UrlUtility.Combine(rootWebServerRelativeUrl, FontSchemeUrl);
            }

            if (!string.IsNullOrEmpty(BackgroundImageUrl) && BackgroundImageUrl.ToLower().StartsWith(rootWebServerRelativeUrl.ToLower()))
            {
                BackgroundImageUrl = UrlUtility.Combine(rootWebServerRelativeUrl, BackgroundImageUrl);
            }

            CurrentWeb.SetThemeByUrl(ColorPaletteUrl, FontSchemeUrl, BackgroundImageUrl, ResetSubwebsToInherit, UpdateRootWebOnly);

            ClientContext.ExecuteQueryRetry();

            if (!CurrentWeb.IsNoScriptSite())
            {
                ComposedLook composedLook;
                // Set the corresponding property bag value which is used by the provisioning engine
                if (CurrentWeb.PropertyBagContainsKey(PROPBAGKEY))
                {
                    composedLook =
                        JsonSerializer.Deserialize<ComposedLook>(CurrentWeb.GetPropertyBagValueString(PROPBAGKEY, ""));

                }
                else
                {
                    composedLook = new ComposedLook { BackgroundFile = "" };
                    CurrentWeb.EnsureProperty(w => w.AlternateCssUrl);
                    composedLook.ColorFile = "";
                    CurrentWeb.EnsureProperty(w => w.MasterUrl);
                    composedLook.FontFile = "";
                    CurrentWeb.EnsureProperty(w => w.SiteLogoUrl);
                }

                composedLook.Name = composedLook.Name ?? "Custom by PnP PowerShell";
                composedLook.ColorFile = ColorPaletteUrl ?? composedLook.ColorFile;
                composedLook.FontFile = FontSchemeUrl ?? composedLook.FontFile;
                composedLook.BackgroundFile = BackgroundImageUrl ?? composedLook.BackgroundFile;
                var composedLookJson = JsonSerializer.Serialize(composedLook);

                CurrentWeb.SetPropertyBagValue(PROPBAGKEY, composedLookJson);
            }
        }
    }
}
