using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Purview
{
    [Cmdlet(VerbsCommon.Set, "PnPSiteClassification")]
    [OutputType(typeof(void))]

    // Next to SharePoint CSOM this cmdlet updates the classification of the Microsoft 365 group behind the site through Microsoft Graph
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Directory.ReadWrite.All")]
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