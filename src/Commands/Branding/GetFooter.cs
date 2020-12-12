using System.Management.Automation;
using Microsoft.SharePoint.Client;


namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Get, "PnPFooter")]
    public class GettFooter : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            SelectedWeb.EnsureProperties(w => w.FooterEnabled, w => w.FooterLayout, w => w.FooterEmphasis);

            var footer = new PSObject();
            footer.Properties.Add(new PSVariableProperty(new PSVariable("IsEnabled", SelectedWeb.FooterEnabled)));
            footer.Properties.Add(new PSVariableProperty(new PSVariable("Layout", SelectedWeb.FooterLayout)));
            footer.Properties.Add(new PSVariableProperty(new PSVariable("BackgroundTheme", SelectedWeb.FooterEmphasis)));
            footer.Properties.Add(new PSVariableProperty(new PSVariable("Title", SelectedWeb.GetFooterTitle())));
            footer.Properties.Add(new PSVariableProperty(new PSVariable("LogoUrl", SelectedWeb.GetFooterLogoUrl())));

            WriteObject(footer);
        }
    }
}