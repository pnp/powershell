using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerAutomate
{
    /// <summary>
    /// Contains the results of a request to export a Flow package
    /// </summary>
    public class FlowExportPackageWrapper
    {
        /// <summary>
        /// Raw state indicating if the Flow export request was successful
        /// </summary>
        [JsonPropertyName("status")]
        public string StatusRaw { get; set; }

        /// <summary>
        /// The status of the export request as an enum
        /// </summary>     
        [JsonIgnore]
        public Enums.FlowExportStatus? Status
        {
            get { return !string.IsNullOrWhiteSpace(StatusRaw) && Enum.TryParse<Enums.FlowExportStatus>(StatusRaw, true, out var result) ? result : (Enums.FlowExportStatus?)null; }
            set { StatusRaw = value.ToString(); }
        }

        /// <summary>
        /// Contains the resource identifiers
        /// </summary>
        [JsonPropertyName("baseResourceIds")]
        public string[] BaseResourceIds { get; set; }

        /// <summary>
        /// List with resources contained in the export
        /// </summary>
        [JsonPropertyName("resources")]
        public Dictionary<string, FlowExportPackageResource> Resources { get; set; }

        /// <summary>
        /// Array with errors generated when trying to export the resource
        /// </summary>
        [JsonPropertyName("errors")]
        public FlowExportPackageError[] Errors{ get; set; }
    }
}