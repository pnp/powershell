using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using PnP.PowerShell.Commands.Utilities.JSON;

namespace PnP.PowerShell.Commands.Model.ServiceHealth
{
    /// <summary>
    /// A service health issue
    /// </summary>
    public class ServiceHealthIssue
    {
        /// <summary>
        /// Date and time at which this health issue started
        /// </summary>
        [JsonConverter(typeof(DateTimeISO8601Converter))]
        [JsonPropertyName("startDateTime")]
        public DateTime? StartDateTime { get; set; }
        
        /// <summary>
        /// Date and time at which this health issue got resolved
        /// </summary>
        [JsonConverter(typeof(DateTimeISO8601Converter))]
        [JsonPropertyName("endDateTime")]
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// Date and time at which this health issue last got updated
        /// </summary>
        [JsonConverter(typeof(DateTimeISO8601Converter))]
        [JsonPropertyName("lastModifiedDateTime")]
        public DateTime? LastModifiedDateTime { get; set; }

        /// <summary>
        /// Title of the health issue
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Unique identifier of the health issue
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Description of the impact of this health issue
        /// </summary>
        [JsonPropertyName("impactDescription")]
        public string ImpactDescription { get; set; }

        /// <summary>
        /// Classification of this health issue
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("classification")]
        public Enums.ServiceHealthIssueClassification Classification { get; set; }

        /// <summary>
        /// Origin of the health issue, i.e. Microsoft
        /// </summary>
        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        /// <summary>
        /// Current status of the service health issue
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("status")]
        public Enums.ServiceHealthIssueStatus? Status { get; set; }

        /// <summary>
        /// The service impacted
        /// </summary>
        [JsonPropertyName("service")]
        public string Service { get; set; }

        /// <summary>
        /// Specific feature that has a health issue
        /// </summary>
        [JsonPropertyName("feature")]
        public string Feature { get; set; }

        /// <summary>
        /// The feature group the feature belongs to that is having a health issue
        /// </summary>
        [JsonPropertyName("featureGroup")]
        public string FeatureGroup { get; set; }

        /// <summary>
        /// Boolean indicating if the issue has been resolved
        /// </summary>
        [JsonPropertyName("isResolved")]
        public bool? IsResolved { get; set; }

        /// <summary>
        /// Indicator if this issue has a high impact
        /// </summary>
        [JsonPropertyName("highImpact")]
        public string HighImpact { get; set; }

        /// <summary>
        /// Details on the service health issue
        /// </summary>
        [JsonPropertyName("details")]
        public List<ServiceHealthIssueDetail> Details { get; set; }

        /// <summary>
        /// Communications that have been created to inform about the progress of resolving the service health issue
        /// </summary>
        [JsonPropertyName("posts")]
        public List<ServiceHealthIssuePost> Posts { get; set; }
    }
}


