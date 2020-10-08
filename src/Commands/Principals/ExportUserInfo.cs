using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsData.Export, "UserInfo")]
    public class ExportUserInfo : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string LoginName;

        [Parameter(Mandatory = true)]
        public string Site;

        protected override void ExecuteCmdlet()
        {
            var hostUrl = ClientContext.Url;
            if (hostUrl.EndsWith("/"))
            {
                hostUrl = hostUrl.Substring(0, hostUrl.Length - 1);
            }
            var site = this.Tenant.GetSiteByUrl(Site);
            ClientContext.Load(site);
            ClientContext.ExecuteQuery();
            var normalizedUserName = UrlUtilities.UrlEncode($"i:0#.f|membership|{LoginName}");
            var results = RestHelper.GetAsync<RestResultCollection<ExportEntity>>(this.HttpClient, $"{hostUrl}/_api/sp.userprofiles.peoplemanager/GetSPUserInformation(accountName=@a,siteId=@b)?@a='{normalizedUserName}'&@b='{site.Id}'", this.AccessToken, false).GetAwaiter().GetResult();
            var record = new PSObject();
            foreach (var item in results.Items)
            {
                record.Properties.Add(new PSVariableProperty(new PSVariable(item.Key.Split("|")[1], item.Value)));
            }
            WriteObject(record);
        }
    }
}