using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    public class PowerPlatformConnector
    {

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("id")]
        public string id { get; set; }

        [JsonPropertyName("type")]
        public string type { get; set; }

        [JsonPropertyName("properties")]
        public Properties properties { get; set; }


        public class ApiDefinitions
        {
            [JsonPropertyName("originalSwaggerUrl")]
            public string originalSwaggerUrl { get; set; }

            [JsonPropertyName("modifiedSwaggerUrl")]
            public string modifiedSwaggerUrl { get; set; }
        }

        public class AuthorizationUrl
        {
            [JsonPropertyName("value")]
            public string value { get; set; }
        }

        public class BackendService
        {
            [JsonPropertyName("serviceUrl")]
            public string serviceUrl { get; set; }
        }

        public class ConnectionParameters
        {
            [JsonPropertyName("token")]
            public Token token { get; set; }

            [JsonPropertyName("token:TenantId")]
            public TokenTenantId tokenTenantId { get; set; }
        }

        public class Constraints
        {
            [JsonPropertyName("required")]
            public string required { get; set; }

            [JsonPropertyName("hidden")]
            public string hidden { get; set; }
        }

        public class Contact
        {
        }

        public class CreatedBy
        {
            [JsonPropertyName("id")]
            public string id { get; set; }

            [JsonPropertyName("displayName")]
            public string displayName { get; set; }

            [JsonPropertyName("email")]
            public string email { get; set; }

            [JsonPropertyName("type")]
            public string type { get; set; }

            [JsonPropertyName("tenantId")]
            public string tenantId { get; set; }

            [JsonPropertyName("userPrincipalName")]
            public string userPrincipalName { get; set; }
        }

        public class CustomParameters
        {
            [JsonPropertyName("loginUri")]
            public LoginUri loginUri { get; set; }

            [JsonPropertyName("tenantId")]
            public TenantId tenantId { get; set; }

            [JsonPropertyName("resourceUri")]
            public ResourceUri resourceUri { get; set; }

            [JsonPropertyName("authorizationUrl")]
            public AuthorizationUrl authorizationUrl { get; set; }

            [JsonPropertyName("tokenUrl")]
            public TokenUrl tokenUrl { get; set; }

            [JsonPropertyName("refreshUrl")]
            public RefreshUrl refreshUrl { get; set; }

            [JsonPropertyName("enableOnbehalfOfLogin")]
            public EnableOnbehalfOfLogin enableOnbehalfOfLogin { get; set; }
        }

        public class EnableOnbehalfOfLogin
        {
            [JsonPropertyName("value")]
            public string value { get; set; }
        }

        public class Environment
        {
            [JsonPropertyName("id")]
            public string id { get; set; }

            [JsonPropertyName("name")]
            public string name { get; set; }
        }

        public class License
        {
        }

        public class LoginUri
        {
            [JsonPropertyName("value")]
            public string value { get; set; }
        }

        public class Metadata
        {
            [JsonPropertyName("sourceType")]
            public string sourceType { get; set; }

            [JsonPropertyName("source")]
            public string source { get; set; }

            [JsonPropertyName("brandColor")]
            public string brandColor { get; set; }

            [JsonPropertyName("contact")]
            public Contact contact { get; set; }

            [JsonPropertyName("license")]
            public License license { get; set; }

            [JsonPropertyName("publisherUrl")]
            public object publisherUrl { get; set; }

            [JsonPropertyName("serviceUrl")]
            public object serviceUrl { get; set; }

            [JsonPropertyName("documentationUrl")]
            public object documentationUrl { get; set; }

            [JsonPropertyName("environmentName")]
            public string environmentName { get; set; }

            [JsonPropertyName("xrmConnectorId")]
            public object xrmConnectorId { get; set; }

            [JsonPropertyName("almMode")]
            public string almMode { get; set; }

            [JsonPropertyName("useNewApimVersion")]
            public bool useNewApimVersion { get; set; }

            [JsonPropertyName("createdBy")]
            public string createdBy { get; set; }

            [JsonPropertyName("modifiedBy")]
            public string modifiedBy { get; set; }

            [JsonPropertyName("allowSharing")]
            public bool allowSharing { get; set; }

            [JsonPropertyName("parameters")]
            public Parameters parameters { get; set; }
        }

        public class ModifiedBy
        {
            [JsonPropertyName("id")]
            public string id { get; set; }

            [JsonPropertyName("displayName")]
            public string displayName { get; set; }

            [JsonPropertyName("email")]
            public string email { get; set; }

            [JsonPropertyName("type")]
            public string type { get; set; }

            [JsonPropertyName("tenantId")]
            public string tenantId { get; set; }

            [JsonPropertyName("userPrincipalName")]
            public string userPrincipalName { get; set; }
        }

        public class OAuthSettings
        {
            [JsonPropertyName("identityProvider")]
            public string identityProvider { get; set; }

            [JsonPropertyName("clientId")]
            public string clientId { get; set; }

            [JsonPropertyName("scopes")]
            public List<string> scopes { get; set; }

            [JsonPropertyName("redirectMode")]
            public string redirectMode { get; set; }

            [JsonPropertyName("redirectUrl")]
            public string redirectUrl { get; set; }

            [JsonPropertyName("properties")]
            public Properties properties { get; set; }

            [JsonPropertyName("customParameters")]
            public CustomParameters customParameters { get; set; }
        }

        public class Parameters
        {
            [JsonPropertyName("x-ms-apimTemplateParameter.name")]
            public string xmsapimTemplateParametername { get; set; }

            [JsonPropertyName("x-ms-apimTemplateParameter.value")]
            public string xmsapimTemplateParametervalue { get; set; }

            [JsonPropertyName("x-ms-apimTemplateParameter.existsAction")]
            public string xmsapimTemplateParameterexistsAction { get; set; }

            [JsonPropertyName("x-ms-apimTemplate-policySection")]
            public string xmsapimTemplatepolicySection { get; set; }
        }

        public class PolicyTemplateInstance
        {
            [JsonPropertyName("templateId")]
            public string templateId { get; set; }

            [JsonPropertyName("title")]
            public string title { get; set; }

            [JsonPropertyName("parameters")]
            public Parameters parameters { get; set; }
        }

        public class Properties
        {
            [JsonPropertyName("displayName")]
            public string displayName { get; set; }

            [JsonPropertyName("iconUri")]
            public string iconUri { get; set; }

            [JsonPropertyName("iconBrandColor")]
            public string iconBrandColor { get; set; }

            [JsonPropertyName("contact")]
            public Contact contact { get; set; }

            [JsonPropertyName("license")]
            public License license { get; set; }

            [JsonPropertyName("apiEnvironment")]
            public string apiEnvironment { get; set; }

            [JsonPropertyName("isCustomApi")]
            public bool isCustomApi { get; set; }

            [JsonPropertyName("connectionParameters")]
            public ConnectionParameters connectionParameters { get; set; }

            [JsonPropertyName("runtimeUrls")]
            public List<string> runtimeUrls { get; set; }

            [JsonPropertyName("primaryRuntimeUrl")]
            public string primaryRuntimeUrl { get; set; }

            [JsonPropertyName("metadata")]
            public Metadata metadata { get; set; }

            [JsonPropertyName("capabilities")]
            public List<object> capabilities { get; set; }

            [JsonPropertyName("description")]
            public string description { get; set; }

            [JsonPropertyName("apiDefinitions")]
            public ApiDefinitions apiDefinitions { get; set; }

            [JsonPropertyName("backendService")]
            public BackendService backendService { get; set; }

            [JsonPropertyName("createdBy")]
            public CreatedBy createdBy { get; set; }

            [JsonPropertyName("modifiedBy")]
            public ModifiedBy modifiedBy { get; set; }

            [JsonPropertyName("createdTime")]
            public DateTimeOffset createdTime { get; set; }

            [JsonPropertyName("changedTime")]
            public DateTimeOffset changedTime { get; set; }

            [JsonPropertyName("environment")]
            public Environment environment { get; set; }

            [JsonPropertyName("tier")]
            public string tier { get; set; }

            [JsonPropertyName("publisher")]
            public string publisher { get; set; }

            [JsonPropertyName("almMode")]
            public string almMode { get; set; }

            [JsonPropertyName("parameters")]
            public Parameters parameters { get; set; }

            [JsonPropertyName("policyTemplateInstances")]
            public List<PolicyTemplateInstance> policyTemplateInstances { get; set; }

            [JsonPropertyName("IsFirstParty")]
            public string IsFirstParty { get; set; }

            [JsonPropertyName("AzureActiveDirectoryResourceId")]
            public string AzureActiveDirectoryResourceId { get; set; }

            [JsonPropertyName("IsOnbehalfofLoginSupported")]
            public bool IsOnbehalfofLoginSupported { get; set; }
        }

        public class RefreshUrl
        {
            [JsonPropertyName("value")]
            public string value { get; set; }
        }

        public class ResourceUri
        {
            [JsonPropertyName("value")]
            public string value { get; set; }
        }

        public class TenantId
        {
            [JsonPropertyName("value")]
            public string value { get; set; }
        }

        public class Token
        {
            [JsonPropertyName("type")]
            public string type { get; set; }

            [JsonPropertyName("oAuthSettings")]
            public OAuthSettings oAuthSettings { get; set; }
        }

        public class TokenTenantId
        {
            [JsonPropertyName("type")]
            public string type { get; set; }

            [JsonPropertyName("metadata")]
            public Metadata metadata { get; set; }

            [JsonPropertyName("uiDefinition")]
            public UiDefinition uiDefinition { get; set; }
        }

        public class TokenUrl
        {
            [JsonPropertyName("value")]
            public string value { get; set; }
        }

        public class UiDefinition
        {
            [JsonPropertyName("constraints")]
            public Constraints constraints { get; set; }
        }

       
           
       


    }
}
