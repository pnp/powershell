using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.New, "PnPPersonalSite")]
    [OutputType(typeof(void))]
    public class NewPersonalSite : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string[] Email;

        protected override void ExecuteCmdlet()
        {
            ProfileLoader profileLoader = ProfileLoader.GetProfileLoader(AdminContext);
            profileLoader.CreatePersonalSiteEnqueueBulk(Email);
            AdminContext.ExecuteQueryRetry();
        }
    }
}