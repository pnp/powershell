using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model.SharePoint;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPWebHeader")]
    [OutputType(typeof(SharePointWebHeader))]
    public class GetWebHeader : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            ClientContext.Load(CurrentWeb, p => p.SiteLogoUrl, p => p.LogoAlignment, p => p.HeaderLayout, p => p.HeaderEmphasis, p => p.HideTitleInHeader, p => p.AllProperties);
            ClientContext.ExecuteQueryRetry();

            var response = new SharePointWebHeader
            {
                SiteLogoUrl = CurrentWeb.SiteLogoUrl,
                LogoAlignment = CurrentWeb.LogoAlignment,
                HeaderLayout = CurrentWeb.HeaderLayout,
                HeaderEmphasis = CurrentWeb.HeaderEmphasis,
                HideTitleInHeader = CurrentWeb.HideTitleInHeader,
                HeaderBackgroundImageUrl = CurrentWeb.AllProperties.FieldValues.ContainsKey("BackgroundImageUrl") ? UrlUtilities.UrlDecode(CurrentWeb.AllProperties["BackgroundImageUrl"] as string) : string.Empty
            };
            WriteObject(response);
        }
    }
}
