using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Get, "PnPFooter")]
    public class GettFooter : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperties(w => w.FooterEnabled, w => w.FooterLayout, w => w.FooterEmphasis);

            var footer = new PSObject();
            footer.Properties.Add(new PSVariableProperty(new PSVariable("IsEnabled", CurrentWeb.FooterEnabled)));
            footer.Properties.Add(new PSVariableProperty(new PSVariable("Layout", CurrentWeb.FooterLayout)));
            footer.Properties.Add(new PSVariableProperty(new PSVariable("BackgroundTheme", CurrentWeb.FooterEmphasis)));
            footer.Properties.Add(new PSVariableProperty(new PSVariable("Title", CurrentWeb.GetFooterTitle())));
            footer.Properties.Add(new PSVariableProperty(new PSVariable("LogoUrl", CurrentWeb.GetFooterLogoUrl())));

            WriteObject(footer);
        }
    }
}