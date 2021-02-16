using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
    internal class AzureADAppPermissionInternal
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("roles")]
        public string[] Roles { get; set; }

        [JsonPropertyName("grantedToIdentities")]
        public List<PermissionIdentityInternal> GrantedToIdentities { get; set; }

        internal AzureADAppPermission Convert()
        {
            var app = new AzureADAppPermission()
            {
                Id = Id,
                Roles = Roles
            };
            foreach (var identity in GrantedToIdentities)
            {
                app.Apps.Add(new AzureADAppIdentity()
                {
                    DisplayName = identity.Application.DisplayName,
                    Id = identity.Application.Id
                });
            }
            return app;
        }
    }

    internal class PermissionIdentityInternal
    {
        [JsonPropertyName("application")]
        public AppIdentityInternal Application { get; set; }
    }

    internal class AppIdentityInternal
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }


    }



}