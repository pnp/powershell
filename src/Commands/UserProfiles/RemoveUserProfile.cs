using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Remove, "PnPUserProfile")]
    [OutputType(typeof(void))]
    public class RemoveUserProfile : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string LoginName;

        [Parameter(Mandatory = false)]
        public string UserId;

        protected override void ExecuteCmdlet()
        {
            var hostUrl = AdminContext.Url;
            if (hostUrl.EndsWith("/"))
            {
                hostUrl = hostUrl.Substring(0, hostUrl.Length - 1);
            }
            var normalizedUserName = UrlUtilities.UrlEncode($"i:0#.f|membership|{LoginName}");

            if (!ParameterSpecified(nameof(UserId)))
            {
                RestHelper.PostAsync(this.HttpClient, $"{hostUrl}/_api/sp.userprofiles.peoplemanager/HardDeleteUserProfile(accountName=@a)?@a='{normalizedUserName}'", AdminContext).GetAwaiter().GetResult();
            }
            else
            {
                RestHelper.PostAsync(this.HttpClient, $"{hostUrl}/_api/sp.userprofiles.peoplemanager/HardDeleteUserProfile(accountName=@a,userId='{UserId}')?@a='{normalizedUserName}'", AdminContext).GetAwaiter().GetResult();
            }

            WriteVerbose($"Completed deletion of user profile {LoginName}");
        }
    }
}
