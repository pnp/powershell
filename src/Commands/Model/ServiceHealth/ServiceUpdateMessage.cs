using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using PnP.PowerShell.Commands.Utilities.JSON;

namespace PnP.PowerShell.Commands.Model.ServiceHealth
{
    /// <summary>
    /// A service update message
    /// </summary>
    public class ServiceUpdateMessage
    {
        /// <summary>
        /// Start of showing this service message
        /// </summary>
        [JsonConverter(typeof(DateTimeISO8601Converter))]
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// End of showing this service message
        /// </summary>
        [JsonConverter(typeof(DateTimeISO8601Converter))]
        public DateTime? EndDateTime { get; set; }
        
        /// <summary>
        /// Date and time at which this service message has last been modified
        /// </summary>
        [JsonConverter(typeof(DateTimeISO8601Converter))]
        public DateTime? LastModifiedDateTime { get; set; }
        
        /// <summary>
        /// Title of the service message
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Unique identifier of the service message, i.e. MC123456
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The category this service message belongs to
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Enums.ServiceUpdateCategory? Category { get; set; }

        /// <summary>
        /// Severity of the change which this service message regards
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Enums.ServiceUpdateSeverity? Severity { get; set; }

        /// <summary>
        /// Tags assigned to this service message
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Boolean indicating if this service message regards a major change (true) or a minor change (false)
        /// </summary>
        public bool? IsMajorChange { get; set; }

        /// <summary>
        /// Date and time by which customer action is require on this service message, if this applies, otherwise NULL will be returned
        /// </summary>
        [JsonConverter(typeof(DateTimeISO8601Converter))]
        public DateTime? ActionRequiredByDateTime { get; set; }

        /// <summary>
        /// The services to which this service message applies
        /// </summary>
        public List<string> Services { get; set; }
        
        /// <summary>
        /// Date and time at which what is described in this service message will expire, if applicable, otherwise NULL will be returned
        /// </summary>
        [JsonConverter(typeof(DateTimeISO8601Converter))]
        public DateTime? ExpiryDateTime { get; set; }

        /// <summary>
        /// Contains information on if this message center announcement has been read, archived and favored
        /// </summary>
        public ServiceUpdateMessageViewPoint ViewPoint { get; set; }

        /// <summary>
        /// Collection with additional information on the service message, i.e. hyperlinks
        /// </summary>
        public List<ServiceUpdateMessageDetail> Details { get; set; }

        /// <summary>
        /// The actual announcement of the service message
        /// </summary>
        public ServiceUpdateMessageBody Body { get; set; }
    }
}