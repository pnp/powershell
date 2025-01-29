using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsData.Export, "PnPUserInfo")]
    [OutputType(typeof(PSObject))]
    public class ExportUserInfo : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string LoginName;

        [Parameter(Mandatory = false)]
        public string Site;

        protected override void ExecuteCmdlet()
        {
            var siteUrl = Connection.Url;
            if (ParameterSpecified(nameof(Site)))
            {
                siteUrl = Site;
            }
            var hostUrl = AdminContext.Url;
            if (hostUrl.EndsWith("/"))
            {
                hostUrl = hostUrl.Substring(0, hostUrl.Length - 1);
            }
            var site = Tenant.GetSiteByUrl(siteUrl);
            AdminContext.Load(site);
            AdminContext.ExecuteQueryRetry();
            var normalizedUserName = UrlUtilities.UrlEncode($"i:0#.f|membership|{LoginName}");
            var results = RestHelper.Get<RestResultCollection<ExportEntity>>(Connection.HttpClient, $"{hostUrl}/_api/sp.userprofiles.peoplemanager/GetSPUserInformation(accountName=@a,siteId=@b)?@a='{normalizedUserName}'&@b='{site.Id}'", AdminContext, false);
            var record = new PSObject();
            foreach (var item in results.Items)
            {
                record.Properties.Add(new PSVariableProperty(new PSVariable(item.Key.Split('|')[1], item.Value)));
            }
            WriteObject(record);
        }
    }
}