using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    /// <summary>
    /// Contains the results of a request to export a Flow package
    /// </summary>
    public class PowerAppPackageWrapper
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
        public Enums.PowerAppExportStatus? Status
        {
            get { return !string.IsNullOrWhiteSpace(StatusRaw) && Enum.TryParse<Enums.PowerAppExportStatus>(StatusRaw, true, out var result) ? result : (Enums.PowerAppExportStatus?)null; }
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
        public Dictionary<string, PowerAppExportPackageResource> Resources { get; set; }

        /// <summary>
        /// Array with errors generated when trying to export the resource
        /// </summary>
        [JsonPropertyName("errors")]
        public PowerAppExportPackageError[] Errors{ get; set; }
    }
}