using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.IO;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Set, "PnPUserProfilePhoto")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ProfilePhoto.ReadWrite.All")]
    [RequiredApiDelegatedPermissions("graph/User.ReadWrite")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/User.ReadWrite.All")]
    public class SetUserProfilePhoto : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADUserPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string Path;

        protected override void ExecuteCmdlet()
        {
            WriteVerbose($"Looking up user provided through the {nameof(Identity)} parameter");
            Model.AzureAD.User user = Identity.GetUser(AccessToken, Connection.AzureEnvironment);

            if (user == null)
            {
                WriteWarning($"User provided through the {nameof(Identity)} parameter could not be found");
                return;
            }

            WriteVerbose($"Setting profile photo for user {user.UserPrincipalName}");

            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }
            if (File.Exists(Path))
            {
                var contentType = "";
                var fileInfo = new FileInfo(Path);
                switch (fileInfo.Extension.ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        {
                            contentType = "image/jpeg";
                            break;
                        }
                    case ".png":
                        {
                            contentType = "image/png";
                            break;
                        }
                }
                if (string.IsNullOrEmpty(contentType))
                {
                    throw new PSArgumentException("File is not of a supported content type (jpg/png/jpeg)");
                }
                Microsoft365GroupsUtility.UploadProfilePhotoAsync(GraphRequestHelper, user.Id.Value, Path);
            }
            else
            {
                throw new PSArgumentException("File not found");
            }
        }
    }
}
