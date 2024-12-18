using AngleSharp.Io;
using PnP.Framework.Diagnostics;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.IO;
using System.Management.Automation;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Get, "PnPUserProfilePhoto")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/ProfilePhoto.ReadWrite.All")]
    [RequiredApiDelegatedPermissions("graph/User.ReadWrite")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/User.ReadWrite.All")]
    public class GetUserProfilePhoto : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADUserPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string Filename;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;


        protected override void ExecuteCmdlet()
        {
            WriteVerbose($"Looking up user provided through the {nameof(Identity)} parameter");
            Model.AzureAD.User user = Identity.GetUser(AccessToken, Connection.AzureEnvironment);

            if (user == null)
            {
                Log.Error("Get-PnPUserProfilePhoto", $"User provided through the {nameof(Identity)} parameter could not be found");
                throw new PSArgumentException($"User provided through the {nameof(Identity)} parameter could not be found");
            }

            WriteVerbose($"Setting profile photo for user {user.UserPrincipalName}");

            if (Filename == null)
            {
                // retrieve the metadata first to figure out the file type
                var photoData = RequestHelper.Get<PhotoMetadata>($"users/{user.Id}/photo");
                if (photoData != null)
                {
                    switch (photoData.ContentType)
                    {
                        case "image/jpeg":
                            {
                                Filename = $"{user.DisplayName}.jpg";
                                break;
                            }
                        case "image/png":
                            {
                                Filename = $"{user.DisplayName}.png";
                                break;
                            }
                    }
                }
                else
                {
                    Log.Error("Get-PnPUserProfilePhoto", "Photo not found");
                    throw new PSArgumentException("Photo for user not found");
                }
            }
            
            if (!System.IO.Path.IsPathRooted(Filename))
            {
                Filename = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Filename);
            }

            var getphoto = true;
            if (File.Exists(Filename))
            {
                if (Force || ShouldContinue($"File {Filename} exists. Overwrite?", Properties.Resources.Confirm))
                {
                    getphoto = true;
                }
                else
                {
                    getphoto = false;
                }
            }
            if (getphoto)
            {
                var response = RequestHelper.GetResponse($"users/{user.Id}/photo/$value");
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                    System.IO.File.WriteAllBytes(Filename, content);
                    WriteObject($"File saved as: {Filename}");
                }
            }
        }

        internal class PhotoMetadata
        {
            [JsonPropertyName("@odata.mediaContentType")]
            public string ContentType { get; set; }
        }
    }
}
