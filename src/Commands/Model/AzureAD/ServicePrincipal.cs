using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class ServicePrincipal
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("deletedDateTime")]
        public DateTime? DeletedDateTime { get; set; }

        [JsonPropertyName("accountEnabled")]
        public bool AccountEnabled { get; set; }

        [JsonPropertyName("alternativeNames")]
        public List<string> AlternativeNames { get; set; }

        [JsonPropertyName("appDisplayName")]
        public string AppDisplayName { get; set; }

        [JsonPropertyName("appDescription")]
        public string AppDescription { get; set; }

        [JsonPropertyName("appId")]
        public string AppId { get; set; }

        [JsonPropertyName("applicationTemplateId")]
        public string ApplicationTemplateId { get; set; }

        [JsonPropertyName("appOwnerOrganizationId")]
        public string AppOwnerOrganizationId { get; set; }

        [JsonPropertyName("appRoleAssignmentRequired")]
        public bool? AppRoleAssignmentRequired { get; set; }

        [JsonPropertyName("createdDateTime")]
        public DateTime? CreatedDateTime { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("disabledByMicrosoftStatus")]
        public string DisabledByMicrosoftStatus { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("homepage")]
        public string Homepage { get; set; }

        [JsonPropertyName("loginUrl")]
        public string LoginUrl { get; set; }

        [JsonPropertyName("logoutUrl")]
        public string LogoutUrl { get; set; }

        [JsonPropertyName("notes")]
        public string Notes { get; set; }

        [JsonPropertyName("notificationEmailAddresses")]
        public List<string> NotificationEmailAddresses { get; set; }

        [JsonPropertyName("preferredSingleSignOnMode")]
        public string PreferredSingleSignOnMode { get; set; }

        [JsonPropertyName("preferredTokenSigningKeyThumbprint")]
        public string PreferredTokenSigningKeyThumbprint { get; set; }

        [JsonPropertyName("replyUrls")]
        public List<string> ReplyUrls { get; set; }

        [JsonPropertyName("servicePrincipalNames")]
        public List<string> ServicePrincipalNames { get; set; }

        [JsonPropertyName("servicePrincipalType")]
        public string ServicePrincipalType { get; set; }

        [JsonPropertyName("signInAudience")]
        public string SignInAudience { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        [JsonPropertyName("tokenEncryptionKeyId")]
        public string TokenEncryptionKeyId { get; set; }

        [JsonPropertyName("info")]
        public ServicePrincipalInfo Info { get; set; }

        [JsonPropertyName("samlSingleSignOnSettings")]
        public object SamlSingleSignOnSettings { get; set; }

        [JsonPropertyName("addIns")]
        public List<object> AddIns { get; set; }

        [JsonPropertyName("appRoles")]
        public List<ServicePrincipalAppRole> AppRoles { get; set; }

        [JsonPropertyName("keyCredentials")]
        public List<ServicePrincipalKeyCredential> KeyCredentials { get; set; }

        [JsonPropertyName("oauth2PermissionScopes")]
        public List<ServicePrincipalOauth2PermissionScopes> Oauth2PermissionScopes { get; set; }

        [JsonPropertyName("passwordCredentials")]
        public List<ServicePrincipalPasswordCredentials> PasswordCredentials { get; set; }

        [JsonPropertyName("resourceSpecificApplicationPermissions")]
        public List<ServicePrincipalResourceSpecificApplicationPermissions> ResourceSpecificApplicationPermissions { get; set; }

        [JsonPropertyName("verifiedPublisher")]
        public ServicePrincipalVerifiedPublisher VerifiedPublisher { get; set; }
    }
}
