using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public class PowerAppProperties
    {
        [JsonPropertyName("appVersion")]
        public DateTimeOffset AppVersion { get; set; }

        [JsonPropertyName("createdByClientVersion")]
        public PowerAppClientVersion CreatedByClientVersion { get; set; }

        [JsonPropertyName("minClientVersion")]
        public PowerAppClientVersion MinClientVersion { get; set; }

        [JsonPropertyName("owner")]
        public PowerAppCreatedBy Owner { get; set; }

        [JsonPropertyName("createdBy")]
        public PowerAppCreatedBy CreatedBy { get; set; }

        [JsonPropertyName("lastModifiedBy")]
        public PowerAppCreatedBy LastModifiedBy { get; set; }

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
        public PowerAppUris AppUris { get; set; }

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
        public System.Collections.Generic.Dictionary<string, PowerAppConnectionReference> ConnectionReferences { get; set; }

        [JsonPropertyName("userAppMetadata")]
        public PowerAppUserAppMetadata UserAppMetadata { get; set; }

        [JsonPropertyName("isFeaturedApp")]
        public bool? IsFeaturedApp { get; set; }

        [JsonPropertyName("bypassConsent")]
        public bool? BypassConsent { get; set; }

        [JsonPropertyName("isHeroApp")]
        public bool? IsHeroApp { get; set; }

        [JsonPropertyName("environment")]
        public PowerAppEnvironment Environment { get; set; }

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
        public PowerAppEmbeddedApp EmbeddedApp { get; set; }

        [JsonPropertyName("publisher")]
        public string Publisher { get; set; }

        [JsonPropertyName("databaseReferences")]
        public DatabaseReferences DatabaseReferences { get; set; }

        [JsonPropertyName("authorizationReferences")]
        public PowerAppAuthorizationReference[] AuthorizationReferences { get; set; }
    }
}
