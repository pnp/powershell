using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Purview
{
    [Cmdlet(VerbsCommon.Set, "PnPSiteClassification")]
    [OutputType(typeof(void))]

    // Deliberately not declaring RequiredApi*Permissions attributes: Microsoft Graph is only called when the site has a Microsoft 365 group behind it, so declaring
    // it unconditionally would warn about a missing permission on sites without a group. It is surfaced informationally instead.
    [ApiPermissionsDependOnResource(
        Remarks = "When the site has a Microsoft 365 group behind it, the classification is placed on that group through Microsoft Graph, which requires Directory.Read.All or Directory.ReadWrite.All. For a site without a group the classification is set on the SharePoint site itself and Microsoft Graph is not called.",
        DocumentationUrl = "https://pnp.github.io/powershell/cmdlets/Set-PnPSiteClassification.html")]
    public class SetSiteClassification : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            ClientContext.Site.SetSiteClassification(Identity, GraphAccessToken);
        }
    }
}