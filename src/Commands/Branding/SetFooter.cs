using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Set, "PnPFooter")]
    public class SetFooter : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled;

        [Parameter(Mandatory = false)]
        public FooterLayoutType Layout;

        [Parameter(Mandatory = false)]
        public FooterVariantThemeType BackgroundTheme;

        [Parameter(Mandatory = false)]
        public string Title;

        [Parameter(Mandatory = false)]
        public string LogoUrl;

        protected override void ExecuteCmdlet()
        {
            bool isDirty = false;

            if (ParameterSpecified(nameof(Enabled)))
            {
                CurrentWeb.FooterEnabled = Enabled.ToBool();
                isDirty = true;
            }

            if (ParameterSpecified(nameof(Layout)))
            {
                CurrentWeb.FooterLayout = Layout;
                isDirty = true;
            }

            if (ParameterSpecified(nameof(BackgroundTheme)))
            {
                CurrentWeb.FooterEmphasis = BackgroundTheme;
                isDirty = true;
            }

            if (ParameterSpecified(nameof(Title)))
            {
                CurrentWeb.SetFooterTitle(Title);
                // No isDirty is needed here as the above request will directly perform the update
            }

            if (ParameterSpecified(nameof(LogoUrl)))
            {
                if (LogoUrl == string.Empty)
                {
                    CurrentWeb.RemoveFooterLogoUrl();
                }
                else
                {
                    CurrentWeb.SetFooterLogoUrl(LogoUrl);
                }
                // No isDirty is needed here as the above request will directly perform the update
            }

            if (isDirty)
            {
                CurrentWeb.Update();
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}