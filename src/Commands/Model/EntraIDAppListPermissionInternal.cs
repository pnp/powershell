using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Internal model for deserializing Graph API beta list permission responses.
    /// List permissions use <c>grantedToV2</c> (singular) rather than <c>grantedToIdentities</c> (array).
    /// </summary>
    internal class EntraIDAppListPermissionInternal
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("roles")]
        public string[] Roles { get; set; }

        /// <summary>
        /// Used in the beta list permissions API response (singular object)
        /// </summary>
        [JsonPropertyName("grantedToV2")]
        public ListPermissionGrantedToV2Internal GrantedToV2 { get; set; }

        /// <summary>
        /// Fallback for APIs that still return the older grantedToIdentities array
        /// </summary>
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

    internal class ListPermissionGrantedToV2Internal
    {
        [JsonPropertyName("application")]
        public AppIdentityInternal Application { get; set; }
    }
}
