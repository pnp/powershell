using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.EntraID
{
    internal class AppPermissionInternal
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("roles")]
        public string[] Roles { get; set; }

        [JsonPropertyName("grantedToIdentities")]
        public List<PermissionIdentityInternal> GrantedToIdentities { get; set; }

        internal AppPermission Convert()
        {
            var app = new AppPermission
            {
                Id = Id,
                Roles = Roles
            };
            foreach (var identity in GrantedToIdentities)
            {
                app.Apps.Add(new AppIdentity
                {
                    DisplayName = identity.Application.DisplayName,
                    Id = identity.Application.Id
                });
            }
            return app;
        }
    }
}