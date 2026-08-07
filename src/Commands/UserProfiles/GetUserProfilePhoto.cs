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
    [RequiredApiDelegatedOrApplicationPermissions("graph/ProfilePhoto.Read.All")]
    [RequiredApiDelegatedPermissions("graph/User.ReadBasic.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/User.Read.All")]
    public class GetUserProfilePhoto : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public EntraIDUserPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string Filename;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;


        protected override void ExecuteCmdlet()
        {
            // The identifier as it was provided already addresses the photo endpoints, so it acts as the fallback for a connection which holds
            // ProfilePhoto.Read.All only. Such a connection cannot read the user object that the id and display name below come from.
            var userIdentifier = Identity.User?.Id?.ToString() ?? Identity.UserId?.ToString() ?? Identity.User?.UserPrincipalName ?? Identity.Upn;
            if (string.IsNullOrWhiteSpace(userIdentifier))
            {
                throw new PSArgumentException($"User provided through the {nameof(Identity)} parameter cannot be resolved", nameof(Identity));
            }

            LogDebug($"Looking up user provided through the {nameof(Identity)} parameter");
            Model.AzureAD.User user = null;
            var lookupSucceeded = true;
            try
            {
                user = Identity.GetUser(AccessToken, Connection.AzureEnvironment);
            }
            catch (System.Exception e)
            {
                // Reading the user object needs a scope such as User.ReadBasic.All, which a connection holding ProfilePhoto.Read.All only does not
                // have. The photo can still be retrieved by addressing the user through the identifier that was provided, so rather than failing,
                // the lookup is skipped and the identifier is used instead of the id and the display name.
                lookupSucceeded = false;
                LogDebug($"Unable to look up the user, continuing with the identifier provided through the {nameof(Identity)} parameter: {e.Message}");
            }

            // Only a lookup which completed can tell that the user does not exist. One which could not be performed says nothing about that, so it
            // must not be reported as an unknown user.
            if (lookupSucceeded && user == null)
            {
                Log.Error("Get-PnPUserProfilePhoto", $"User provided through the {nameof(Identity)} parameter could not be found");
                throw new PSArgumentException($"User provided through the {nameof(Identity)} parameter could not be found");
            }

            var userSegment = System.Uri.EscapeDataString(user?.Id?.ToString() ?? userIdentifier);
            LogDebug($"Retrieving profile photo for user {user?.UserPrincipalName ?? userIdentifier}");

            if (Filename == null)
            {
                // retrieve the metadata first to figure out the file type
                var photoData = GraphRequestHelper.Get<PhotoMetadata>($"users/{userSegment}/photo");
                if (photoData != null)
                {
                    // Falls back to the identifier that was provided when the display name could not be read, which is the case on a connection
                    // holding ProfilePhoto.Read.All only
                    var fileNameWithoutExtension = user?.DisplayName ?? userIdentifier;
                    switch (photoData.ContentType)
                    {
                        case "image/jpeg":
                            {
                                Filename = $"{fileNameWithoutExtension}.jpg";
                                break;
                            }
                        case "image/png":
                            {
                                Filename = $"{fileNameWithoutExtension}.png";
                                break;
                            }
                        default:
                            {
                                // Microsoft Graph answers the metadata request with a 1x1 image/gif placeholder for a user without a photo, while
                                // the request for the binary data behind it fails with a 404. There is nothing to download and no extension to
                                // derive a file name from, so this used to fail further down on the file name still being NULL.
                                Log.Error("Get-PnPUserProfilePhoto", $"No profile photo found, Microsoft Graph returned a placeholder of type {photoData.ContentType}");
                                throw new PSArgumentException($"The user does not have a profile photo. Microsoft Graph returned a placeholder of type '{photoData.ContentType}' rather than an image.");
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
                // A non successful response will throw from within GetResponse
                using var response = GraphRequestHelper.GetResponse($"users/{userSegment}/photo/$value");
                var content = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                System.IO.File.WriteAllBytes(Filename, content);
                WriteObject($"File saved as: {Filename}");
            }
        }

        internal class PhotoMetadata
        {
            [JsonPropertyName("@odata.mediaContentType")]
            public string ContentType { get; set; }
        }
    }
}
