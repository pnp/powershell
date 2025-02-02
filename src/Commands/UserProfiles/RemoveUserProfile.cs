using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Remove, "PnPUserProfile")]
    [OutputType(typeof(void))]
    public class RemoveUserProfile : PnPSharePointOnlineAdminCmdlet
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
                RestHelper.Post(Connection.HttpClient, $"{hostUrl}/_api/sp.userprofiles.peoplemanager/HardDeleteUserProfile(accountName=@a)?@a='{normalizedUserName}'", AdminContext);
            }
            else
            {
                RestHelper.Post(Connection.HttpClient, $"{hostUrl}/_api/sp.userprofiles.peoplemanager/HardDeleteUserProfile(accountName=@a,userId='{UserId}')?@a='{normalizedUserName}'", AdminContext);
            }

            WriteVerbose($"Completed deletion of user profile {LoginName}");
        }
    }
}
