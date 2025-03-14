using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Remove, "PnPUserProfilePhoto")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ProfilePhoto.ReadWrite.All")]
    [RequiredApiDelegatedPermissions("graph/User.ReadWrite")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/User.ReadWrite.All")]
    public class RemoveUserProfilePhoto : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADUserPipeBind Identity;
        protected override void ExecuteCmdlet()
        {
            LogDebug($"Looking up user provided through the {nameof(Identity)} parameter");
            Model.AzureAD.User user = Identity.GetUser(AccessToken, Connection.AzureEnvironment);

            if (user == null)
            {
                LogWarning($"User provided through the {nameof(Identity)} parameter could not be found");
                return;
            }

            LogDebug($"Removing profile photo for user {user.UserPrincipalName}");

            GraphRequestHelper.Delete($"users/{user.Id}/photo/$value");
        }
    }
}
