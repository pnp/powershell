using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;

using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Get, "PnPUserOneDriveQuota")]
    [OutputType(typeof(long))]
    public class GetUserOneDriveQuota : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Account;

        protected override void ExecuteCmdlet()
        {
            var peopleManager = new PeopleManager(AdminContext);

            var result = Tenant.EncodeClaim(Account);
            AdminContext.ExecuteQueryRetry();
            Account = result.Value;

            var properties = peopleManager.GetPropertiesFor(Account);
            AdminContext.Load(properties);
            AdminContext.ExecuteQueryRetry();

            var personalSiteUrl = properties.PersonalUrl?.TrimEnd('/');
            if (string.IsNullOrWhiteSpace(personalSiteUrl))
            {
                LogWarning($"Couldn't find OneDrive quota for the account: {Account}");
                return;
            }

            try
            {
                var userSite = Tenant.GetSitePropertiesByUrl(personalSiteUrl, true);
                AdminContext.Load(userSite);
                AdminContext.ExecuteQueryRetry();

                WriteObject(userSite.StorageMaximumLevel * 1024 * 1024);
            }
            catch (ServerException e) when (string.Equals(e.ServerErrorTypeName, "Microsoft.Online.SharePoint.Common.SpoNoSiteException", StringComparison.InvariantCultureIgnoreCase))
            {
                LogWarning($"Couldn't find OneDrive quota for the account: {Account}");
            }
        }
    }
}