using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.New, "PnPPersonalSite")]
    public class NewPersonalSite : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string[] Email;

        protected override void ExecuteCmdlet()
        {
            ProfileLoader profileLoader = ProfileLoader.GetProfileLoader(ClientContext);
            profileLoader.CreatePersonalSiteEnqueueBulk(Email);
            ClientContext.ExecuteQueryRetry();
        }
    }
}