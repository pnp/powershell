using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.Planner
{
    /// <summary>
    /// Contains the Planner Tenant Configuration
    /// </summary>
    public class PlannerTenantConfig
    {
        /// <summary>
        /// Indicates if Microsoft Planner is enabled
        /// </summary>
        [JsonPropertyName("isPlannerAllowed")]
        public bool? IsPlannerAllowed { get; set; }

        /// <summary>
        /// Indicates whether Outlook calendar sync is enabled
        /// </summary>
        [JsonPropertyName("allowCalendarSharing")]
        public bool? AllowCalendarSharing { get; set; }

        /// <summary>
        /// Indicates whether a tenant move into a new region is currently authorized
        /// </summary>
        [JsonPropertyName("allowTenantMoveWithDataLoss")]
        public bool? AllowTenantMoveWithDataLoss { get; set; }

        /// <summary>
        /// Indicates whether the creation of Roster containers (Planner plans without Microsoft 365 Groups) is allowed
        /// </summary>
        [JsonPropertyName("allowRosterCreation")]
        public bool? AllowRosterCreation { get; set; }

        /// <summary>
        /// Indicates whether the direct push notifications are enabled where contents of the push notification are being sent directly through Apple's or Google's services to get to the iOS or Android client
        /// </summary>
        [JsonPropertyName("allowPlannerMobilePushNotifications")]
        public bool? AllowPlannerMobilePushNotifications { get; set; }
    }
}