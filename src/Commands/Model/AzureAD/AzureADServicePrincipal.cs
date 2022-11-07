using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class AzureADServicePrincipal
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("deletedDateTime")]
        public object DeletedDateTime { get; set; }

        [JsonPropertyName("accountEnabled")]
        public bool AccountEnabled { get; set; }

        [JsonPropertyName("alternativeNames")]
        public List<string> AlternativeNames { get; set; }

        [JsonPropertyName("appDisplayName")]
        public object AppDisplayName { get; set; }

        [JsonPropertyName("appDescription")]
        public object AppDescription { get; set; }

        [JsonPropertyName("appId")]
        public string AppId { get; set; }

        [JsonPropertyName("applicationTemplateId")]
        public object ApplicationTemplateId { get; set; }

        [JsonPropertyName("appOwnerOrganizationId")]
        public object AppOwnerOrganizationId { get; set; }

        [JsonPropertyName("appRoleAssignmentRequired")]
        public bool AppRoleAssignmentRequired { get; set; }

        [JsonPropertyName("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [JsonPropertyName("description")]
        public object Description { get; set; }

        [JsonPropertyName("disabledByMicrosoftStatus")]
        public object DisabledByMicrosoftStatus { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("homepage")]
        public object Homepage { get; set; }

        [JsonPropertyName("loginUrl")]
        public object LoginUrl { get; set; }

        [JsonPropertyName("logoutUrl")]
        public object LogoutUrl { get; set; }

        [JsonPropertyName("notes")]
        public object Notes { get; set; }

        [JsonPropertyName("notificationEmailAddresses")]
        public List<object> NotificationEmailAddresses { get; set; }

        [JsonPropertyName("preferredSingleSignOnMode")]
        public object PreferredSingleSignOnMode { get; set; }

        [JsonPropertyName("preferredTokenSigningKeyThumbprint")]
        public object PreferredTokenSigningKeyThumbprint { get; set; }

        [JsonPropertyName("replyUrls")]
        public List<object> ReplyUrls { get; set; }

        [JsonPropertyName("servicePrincipalNames")]
        public List<string> ServicePrincipalNames { get; set; }

        [JsonPropertyName("servicePrincipalType")]
        public string ServicePrincipalType { get; set; }

        [JsonPropertyName("signInAudience")]
        public object SignInAudience { get; set; }

        [JsonPropertyName("tags")]
        public List<object> Tags { get; set; }

        [JsonPropertyName("tokenEncryptionKeyId")]
        public object TokenEncryptionKeyId { get; set; }

        [JsonPropertyName("info")]
        public object Info { get; set; }

        [JsonPropertyName("samlSingleSignOnSettings")]
        public object SamlSingleSignOnSettings { get; set; }

        [JsonPropertyName("addIns")]
        public List<object> AddIns { get; set; }

        [JsonPropertyName("appRoles")]
        public List<AzureADServicePrincipalAppRole> AppRoles { get; set; }

        [JsonPropertyName("keyCredentials")]
        public List<AzureADServicePrincipalKeyCredential> KeyCredentials { get; set; }

        [JsonPropertyName("oauth2PermissionScopes")]
        public List<object> Oauth2PermissionScopes { get; set; }

        [JsonPropertyName("passwordCredentials")]
        public List<object> PasswordCredentials { get; set; }

        [JsonPropertyName("resourceSpecificApplicationPermissions")]
        public List<object> ResourceSpecificApplicationPermissions { get; set; }

        [JsonPropertyName("verifiedPublisher")]
        public AzureADServicePrincipalVerifiedPublisher VerifiedPublisher { get; set; }
    }
}
