using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public class PowerAppProperties
    {
        [JsonPropertyName("appVersion")]
        public DateTimeOffset AppVersion { get; set; }

        [JsonPropertyName("createdByClientVersion")]
        public ClientVersion CreatedByClientVersion { get; set; }

        [JsonPropertyName("minClientVersion")]
        public ClientVersion MinClientVersion { get; set; }

        [JsonPropertyName("owner")]
        public CreatedBy Owner { get; set; }

        [JsonPropertyName("createdBy")]
        public CreatedBy CreatedBy { get; set; }

        [JsonPropertyName("lastModifiedBy")]
        public CreatedBy LastModifiedBy { get; set; }

        [JsonPropertyName("backgroundColor")]
        public string BackgroundColor { get; set; }

        [JsonPropertyName("backgroundImageUri")]
        public Uri BackgroundImageUri { get; set; }

        [JsonPropertyName("teamsColorIconUrl")]
        public Uri TeamsColorIconUrl { get; set; }

        [JsonPropertyName("teamsOutlineIconUrl")]
        public Uri TeamsOutlineIconUrl { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("appUris")]
        public AppUris AppUris { get; set; }

        [JsonPropertyName("createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [JsonPropertyName("lastModifiedTime")]
        public DateTimeOffset LastModifiedTime { get; set; }

        [JsonPropertyName("sharedGroupsCount")]
        public long SharedGroupsCount { get; set; }

        [JsonPropertyName("sharedUsersCount")]
        public long SharedUsersCount { get; set; }

        [JsonPropertyName("appOpenProtocolUri")]
        public string AppOpenProtocolUri { get; set; }

        [JsonPropertyName("appOpenUri")]
        public Uri AppOpenUri { get; set; }

        [JsonPropertyName("appPlayUri")]
        public Uri AppPlayUri { get; set; }

        [JsonPropertyName("appPlayEmbeddedUri")]
        public Uri AppPlayEmbeddedUri { get; set; }

        [JsonPropertyName("appPlayTeamsUri")]
        public string AppPlayTeamsUri { get; set; }

        [JsonPropertyName("connectionReferences")]
        public System.Collections.Generic.Dictionary<string, ConnectionReference> ConnectionReferences { get; set; }

        [JsonPropertyName("userAppMetadata")]
        public UserAppMetadata UserAppMetadata { get; set; }

        [JsonPropertyName("isFeaturedApp")]
        public bool? IsFeaturedApp { get; set; }

        [JsonPropertyName("bypassConsent")]
        public bool? BypassConsent { get; set; }

        [JsonPropertyName("isHeroApp")]
        public bool? IsHeroApp { get; set; }

        [JsonPropertyName("environment")]
        public Environment Environment { get; set; }

        [JsonPropertyName("almMode")]
        public string AlmMode { get; set; }

        [JsonPropertyName("performanceOptimizationEnabled")]
        public bool? PerformanceOptimizationEnabled { get; set; }

        [JsonPropertyName("unauthenticatedWebPackageHint")]
        public Guid? UnauthenticatedWebPackageHint { get; set; }

        [JsonPropertyName("canConsumeAppPass")]
        public bool? CanConsumeAppPass { get; set; }

        [JsonPropertyName("enableModernRuntimeMode")]
        public bool? EnableModernRuntimeMode { get; set; }

        [JsonPropertyName("executionRestrictions")]
        public ExecutionRestrictions ExecutionRestrictions { get; set; }

        [JsonPropertyName("appPlanClassification")]
        public string AppPlanClassification { get; set; }

        [JsonPropertyName("usesPremiumApi")]
        public bool? UsesPremiumApi { get; set; }

        [JsonPropertyName("usesOnlyGrandfatheredPremiumApis")]
        public bool? UsesOnlyGrandfatheredPremiumApis { get; set; }

        [JsonPropertyName("usesCustomApi")]
        public bool? UsesCustomApi { get; set; }

        [JsonPropertyName("usesOnPremiseGateway")]
        public bool? UsesOnPremiseGateway { get; set; }

        [JsonPropertyName("usesPcfExternalServiceUsage")]
        public bool? UsesPcfExternalServiceUsage { get; set; }

        [JsonPropertyName("isCustomizable")]
        public bool? IsCustomizable { get; set; }

        [JsonPropertyName("embeddedApp")]
        public EmbeddedApp EmbeddedApp { get; set; }

        [JsonPropertyName("publisher")]
        public string Publisher { get; set; }

        [JsonPropertyName("databaseReferences")]
        public DatabaseReferences DatabaseReferences { get; set; }

        [JsonPropertyName("authorizationReferences")]
        public AuthorizationReference[] AuthorizationReferences { get; set; }
    }

    public partial class AppUris
    {
        [JsonPropertyName("documentUri")]
        public DocumentUri DocumentUri { get; set; }

        [JsonPropertyName("imageUris")]
        public object[] ImageUris { get; set; }

        [JsonPropertyName("additionalUris")]
        public object[] AdditionalUris { get; set; }
    }

    public partial class DocumentUri
    {
        [JsonPropertyName("value")]
        public Uri Value { get; set; }

        [JsonPropertyName("readonlyValue")]
        public Uri ReadonlyValue { get; set; }
    }

    public partial class AuthorizationReference
    {
        [JsonPropertyName("resourceId")]
        public string ResourceId { get; set; }
    }

    public partial class ConnectionReference
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("iconUri")]
        public Uri IconUri { get; set; }

        [JsonPropertyName("dataSources")]
        public string[] DataSources { get; set; }

        [JsonPropertyName("dependencies")]
        public Guid[] Dependencies { get; set; }

        [JsonPropertyName("dependents")]
        public Guid[] Dependents { get; set; }

        [JsonPropertyName("isOnPremiseConnection")]
        public bool? IsOnPremiseConnection { get; set; }

        [JsonPropertyName("bypassConsent")]
        public bool? BypassConsent { get; set; }

        [JsonPropertyName("apiTier")]
        public string ApiTier { get; set; }

        [JsonPropertyName("isCustomApiConnection")]
        public bool? IsCustomApiConnection { get; set; }

        [JsonPropertyName("actions")]
        public string[] Actions { get; set; }

        [JsonPropertyName("nestedActions")]
        public NestedAction[] NestedActions { get; set; }

        [JsonPropertyName("gatewayObjectIdHint")]
        public Guid? GatewayObjectIdHint { get; set; }

        [JsonPropertyName("sharedConnectionId")]
        public string SharedConnectionId { get; set; }

        [JsonPropertyName("authenticationType")]
        public string AuthenticationType { get; set; }

        [JsonPropertyName("endpoints")]
        public string[] Endpoints { get; set; }


    }

    public partial class NestedAction
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("apiName")]
        public string ApiName { get; set; }

        [JsonPropertyName("actionName")]
        public string ActionName { get; set; }

        [JsonPropertyName("referencedResources")]
        public ReferencedResource[] ReferencedResources { get; set; }
    }

    public partial class ReferencedResource
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public partial class CreatedBy
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("tenantId")]
        public Guid TenantId { get; set; }

        [JsonPropertyName("userPrincipalName")]
        public string UserPrincipalName { get; set; }
    }

    public partial class ClientVersion
    {
        [JsonPropertyName("major")]
        public long Major { get; set; }

        [JsonPropertyName("minor")]
        public long Minor { get; set; }

        [JsonPropertyName("build")]
        public long Build { get; set; }

        [JsonPropertyName("revision")]
        public long Revision { get; set; }

        [JsonPropertyName("majorRevision")]
        public long MajorRevision { get; set; }

        [JsonPropertyName("minorRevision")]
        public long MinorRevision { get; set; }
    }

    public partial class DatabaseReferences
    {
        [JsonPropertyName("default.cds")]
        public DefaultCds DefaultCds { get; set; }
    }

    public partial class DefaultCds
    {
        [JsonPropertyName("databaseDetails")]
        public DatabaseDetails DatabaseDetails { get; set; }

        [JsonPropertyName("dataSources")]
        public System.Collections.Generic.Dictionary<string, DataSource> DataSources { get; set; }

        [JsonPropertyName("actions")]
        public string[] Actions { get; set; }
    }

    public partial class DataSource
    {
        [JsonPropertyName("entitySetName")]
        public string EntitySetName { get; set; }

        [JsonPropertyName("logicalName")]
        public string LogicalName { get; set; }
    }

    public partial class DatabaseDetails
    {
        [JsonPropertyName("referenceType")]
        public string ReferenceType { get; set; }

        [JsonPropertyName("environmentName")]
        public string EnvironmentName { get; set; }

        [JsonPropertyName("linkedEnvironmentMetadata")]
        public LinkedEnvironmentMetadata LinkedEnvironmentMetadata { get; set; }

        [JsonPropertyName("overrideValues")]
        public OverrideValues OverrideValues { get; set; }
    }

    public partial class LinkedEnvironmentMetadata
    {
        [JsonPropertyName("resourceId")]
        public Guid ResourceId { get; set; }

        [JsonPropertyName("friendlyName")]
        public string FriendlyName { get; set; }

        [JsonPropertyName("uniqueName")]
        public string UniqueName { get; set; }

        [JsonPropertyName("domainName")]
        public string DomainName { get; set; }

        [JsonPropertyName("version")]
        public Version Version { get; set; }

        [JsonPropertyName("instanceUrl")]
        public Uri InstanceUrl { get; set; }

        [JsonPropertyName("instanceApiUrl")]
        public Uri InstanceApiUrl { get; set; }

        [JsonPropertyName("baseLanguage")]
        public long BaseLanguage { get; set; }

        [JsonPropertyName("instanceState")]
        public string InstanceState { get; set; }

        [JsonPropertyName("createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [JsonPropertyName("platformSku")]
        public string PlatformSku { get; set; }
    }

    public partial class OverrideValues
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public partial class EmbeddedApp
    {
        [JsonPropertyName("siteId")]
        public Uri SiteId { get; set; }

        [JsonPropertyName("listId")]
        public Guid ListId { get; set; }

        [JsonPropertyName("listUrl")]
        public Uri ListUrl { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("screenWidth")]
        public long ScreenWidth { get; set; }

        [JsonPropertyName("screenHeight")]
        public long ScreenHeight { get; set; }
    }

    public partial class Environment
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }
    }

    public partial class ExecutionRestrictions
    {
        [JsonPropertyName("isTeamsOnly")]
        public bool? IsTeamsOnly { get; set; }

        [JsonPropertyName("dataLossPreventionEvaluationResult")]
        public DataLossPreventionEvaluationResult DataLossPreventionEvaluationResult { get; set; }

        [JsonPropertyName("httpActionRestriction")]
        public HttpActionRestriction HttpActionRestriction { get; set; }
    }

    public partial class DataLossPreventionEvaluationResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("lastEvaluationDate")]
        public DateTimeOffset LastEvaluationDate { get; set; }

        [JsonPropertyName("violationDetails")]
        public object[] ViolationDetails { get; set; }

        [JsonPropertyName("violations")]
        public object[] Violations { get; set; }

        [JsonPropertyName("violationsByPolicy")]
        public object[] ViolationsByPolicy { get; set; }

        [JsonPropertyName("violationErrorMessage")]
        public string ViolationErrorMessage { get; set; }
    }

    public partial class HttpActionRestriction
    {
        [JsonPropertyName("appUsesSharepointHttpAction")]
        public bool? AppUsesSharepointHttpAction { get; set; }

        [JsonPropertyName("enforcementStrategy")]
        public string EnforcementStrategy { get; set; }

        [JsonPropertyName("evaluationTime")]
        public DateTimeOffset EvaluationTime { get; set; }
    }

    public partial class UserAppMetadata
    {
        [JsonPropertyName("favorite")]
        public string Favorite { get; set; }

        [JsonPropertyName("includeInAppsList")]
        public bool? IncludeInAppsList { get; set; }
    }

    public partial class Tags
    {
        [JsonPropertyName("primaryDeviceWidth")]
        public string PrimaryDeviceWidth { get; set; }

        [JsonPropertyName("primaryDeviceHeight")]
        public string PrimaryDeviceHeight { get; set; }

        [JsonPropertyName("sienaVersion")]
        public string SienaVersion { get; set; }

        [JsonPropertyName("deviceCapabilities")]
        public string DeviceCapabilities { get; set; }

        [JsonPropertyName("supportsPortrait")]
        public string SupportsPortrait { get; set; }

        [JsonPropertyName("supportsLandscape")]
        public string SupportsLandscape { get; set; }

        [JsonPropertyName("primaryFormFactor")]
        public string PrimaryFormFactor { get; set; }

        [JsonPropertyName("publisherVersion")]
        public string PublisherVersion { get; set; }

        [JsonPropertyName("minimumRequiredApiVersion")]
        public string MinimumRequiredApiVersion { get; set; }

        [JsonPropertyName("hasComponent")]
        public string HasComponent { get; set; }

        [JsonPropertyName("hasUnlockedComponent")]
        public string HasUnlockedComponent { get; set; }

        [JsonPropertyName("isUnifiedRootApp")]
        public string IsUnifiedRootApp { get; set; }

        [JsonPropertyName("sp-site-id")]
        public Uri SpSiteId { get; set; }

        [JsonPropertyName("sp-list-id")]
        public Guid? SpListId { get; set; }
    }

}
