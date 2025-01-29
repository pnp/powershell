using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantAdministration;
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

            var personalSiteUrl = properties.PersonalUrl;

            SPOSitePropertiesEnumerableFilter filter = new SPOSitePropertiesEnumerableFilter()
            {
                IncludePersonalSite = PersonalSiteFilter.Include,
                IncludeDetail = true,
                Template = "SPSPERS",
                Filter = $"Url -eq '{personalSiteUrl.TrimEnd('/')}'"
            };

            var sitesList = Tenant.GetSitePropertiesFromSharePointByFilters(filter);
            var sites = new List<SiteProperties>();
            do
            {
                Tenant.Context.Load(sitesList);
                Tenant.Context.ExecuteQueryRetry();
                sites.AddRange(sitesList.ToList());
            } while (!string.IsNullOrWhiteSpace(sitesList.NextStartIndexFromSharePoint));

            var userSite = sitesList.Where(s => s.Url.ToLower() == personalSiteUrl.TrimEnd('/').ToLower()).FirstOrDefault();

            if (userSite != null)
            {
                WriteObject(userSite.StorageMaximumLevel * 1024 * 1024);
            }
            else
            {
                WriteWarning($"Couldn't find onedrive quota for the account: {Account} ");
            }
        }
    }
}