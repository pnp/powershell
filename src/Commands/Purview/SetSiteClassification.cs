using Microsoft.SharePoint.Client;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Purview
{
    [Cmdlet(VerbsCommon.Set, "PnPSiteClassification")]
    [OutputType(typeof(void))]
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