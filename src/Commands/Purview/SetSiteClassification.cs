using Microsoft.SharePoint.Client;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Purview
{
    [Cmdlet(VerbsCommon.Set, "PnPSiteClassification")]
    [OutputType(typeof(void))]

    // Deliberately not declaring RequiredApi*Permissions attributes: Microsoft Graph is only called when the site has a Microsoft 365 group behind it, in which case
    // Directory.Read.All or Directory.ReadWrite.All is needed. Declaring that unconditionally would warn about a missing permission on sites without a group.
    // See documentation/Set-PnPSiteClassification.md for the full description.
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