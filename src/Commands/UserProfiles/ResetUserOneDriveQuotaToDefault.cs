using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Reset, "PnPUserOneDriveQuotaToDefault")]
    [OutputType(typeof(ClientResult<string>))]
    public class ResetUserOneDriveQuotaMax : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Account;

        protected override void ExecuteCmdlet()
        {
            var peopleManager = new PeopleManager(AdminContext);
            var result = peopleManager.ResetUserOneDriveQuotaToDefault(Account);
            AdminContext.ExecuteQueryRetry();
            WriteObject(result);
        }
    }
}