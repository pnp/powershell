using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Remove, "PnPUserInfo")]
    [OutputType(typeof(PSObject))]
    public class RemoveUserInfo : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string LoginName;

        [Parameter(Mandatory = false)]
        public string Site;

        [Parameter(Mandatory = false)]
        public string RedactName;

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
            RestResultCollection<ExportEntity> results = null;
            if (!ParameterSpecified(nameof(RedactName)))
            {
                results = RestHelper.Post<RestResultCollection<ExportEntity>>(HttpClient, $"{hostUrl}/_api/sp.userprofiles.peoplemanager/RemoveSPUserInformation(accountName=@a,siteId=@b)?@a='{normalizedUserName}'&@b='{site.Id}'", this.AccessToken, false);
            }
            else
            {
                results = RestHelper.Post<RestResultCollection<ExportEntity>>(HttpClient, $"{hostUrl}/_api/sp.userprofiles.peoplemanager/RemoveSPUserInformation(accountName=@a,siteId=@b,redactName=@c)?@a='{normalizedUserName}'&@b='{site.Id}'&@c='{RedactName}'", this.AccessToken, false);
            }
            var record = new PSObject();
            foreach (var item in results.Items)
            {
                record.Properties.Add(new PSVariableProperty(new PSVariable(item.Key.Split('|')[1], item.Value)));
            }
            WriteObject(record);
        }
    }
}