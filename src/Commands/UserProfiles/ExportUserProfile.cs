using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsData.Export, "PnPUserProfile")]
    [OutputType(typeof(object))]
    public class ExportUserProfile : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string LoginName;

        protected override void ExecuteCmdlet()
        {
            var hostUrl = AdminContext.Url;
            if (hostUrl.EndsWith("/"))
            {
                hostUrl = hostUrl.Substring(0, hostUrl.Length - 1);
            }
            var normalizedUserName = UrlUtilities.UrlEncode($"i:0#.f|membership|{LoginName}");
            var results = RestHelper.Get<RestResultCollection<ExportEntity>>(Connection.HttpClient, $"{hostUrl}/_api/sp.userprofiles.peoplemanager/GetUserProfileProperties(accountName=@a)?@a='{normalizedUserName}'", AdminContext, false);
            var record = new PSObject();
            foreach (var item in results.Items)
            {
                record.Properties.Add(new PSVariableProperty(new PSVariable(item.Key.Split('|')[1], item.Value)));
            }
            WriteObject(record);
        }
    }
}
