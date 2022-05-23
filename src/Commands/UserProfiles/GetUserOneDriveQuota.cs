using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;

using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Get, "PnPUserOneDriveQuota")]
    [OutputType(typeof(long))]
    public class GetUserOneDriveQuota : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Account;

        protected override void ExecuteCmdlet()
        {
            var peopleManager = new PeopleManager(ClientContext);
            var oneDriveQuota = peopleManager.GetUserOneDriveQuotaMax(Account);
            ClientContext.ExecuteQueryRetry();
            WriteObject(oneDriveQuota);
        }
    }
}