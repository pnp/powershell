using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Remove, "UserProfile")]
    public class RemoveUserProfile : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string LoginName;

        [Parameter(Mandatory = false)]
        public string UserId;

        protected override void ExecuteCmdlet()
        {
            var hostUrl = ClientContext.Url;
            if (hostUrl.EndsWith("/"))
            {
                hostUrl = hostUrl.Substring(0, hostUrl.Length - 1);
            }
            var normalizedUserName = UrlUtilities.UrlEncode($"i:0#.f|membership|{LoginName}");

            if (!ParameterSpecified(nameof(UserId)))
            {
                RestHelper.PostAsync(this.HttpClient, $"{hostUrl}/_api/sp.userprofiles.peoplemanager/HardDeleteUserProfile(accountName=@a)?@a='{normalizedUserName}'", this.AccessToken).GetAwaiter().GetResult();
            }
            else
            {
                RestHelper.PostAsync(this.HttpClient, $"{hostUrl}/_api/sp.userprofiles.peoplemanager/HardDeleteUserProfile(accountName=@a,userId='{UserId}')?@a='{normalizedUserName}'", this.AccessToken).GetAwaiter().GetResult();
            }
            WriteObject($"Completed deletion of user profile {LoginName}");
        }
    }
}
