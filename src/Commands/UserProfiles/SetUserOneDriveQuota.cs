using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Set, "PnPUserOneDriveQuota")]
    [OutputType(typeof(ClientResult<string>))]
    public class SetUserOneDriveQuota : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Account;

        [Parameter(Mandatory = true, Position = 1)]
        public long Quota;

        [Parameter(Mandatory = true, Position = 2)]
        public long QuotaWarning;

        protected override void ExecuteCmdlet()
        {
            var peopleManager = new PeopleManager(AdminContext);
            var oneDriveQuota = peopleManager.SetUserOneDriveQuota(Account, Quota, QuotaWarning);
            AdminContext.ExecuteQueryRetry();
            WriteObject(oneDriveQuota);
        }
    }
}