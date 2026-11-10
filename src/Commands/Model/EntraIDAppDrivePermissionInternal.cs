using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
    internal class EntraIDAppDrivePermissionInternal
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("roles")]
        public string[] Roles { get; set; }

        [JsonPropertyName("grantedToV2")]
        public DrivePermissionGrantedToV2Internal GrantedToV2 { get; set; }

        [JsonPropertyName("grantedToIdentities")]
        public List<PermissionIdentityInternal> GrantedToIdentities { get; set; }

        internal AzureADAppPermission Convert()
        {
            var permission = new AzureADAppPermission
            {
                Id = Id,
                Roles = Roles
            };

            if (GrantedToV2?.Application != null)
            {
                permission.Apps.Add(new AzureADAppIdentity
                {
                    DisplayName = GrantedToV2.Application.DisplayName,
                    Id = GrantedToV2.Application.Id
                });
            }
            else if (GrantedToIdentities != null)
            {
                foreach (var identity in GrantedToIdentities)
                {
                    if (identity?.Application != null)
                    {
                        permission.Apps.Add(new AzureADAppIdentity
                        {
                            DisplayName = identity.Application.DisplayName,
                            Id = identity.Application.Id
                        });
                    }
                }
            }

            return permission;
        }
    }

    internal class DrivePermissionGrantedToV2Internal
    {
        [JsonPropertyName("application")]
        public AppIdentityInternal Application { get; set; }
    }
}
