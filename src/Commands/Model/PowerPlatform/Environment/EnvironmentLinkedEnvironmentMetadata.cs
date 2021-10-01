using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment
{
    /// <summary>
    /// Details on the Dynamics environment behind a Power Platform environment
    /// </summary>
    public class EnvironmentLinkedEnvironmentMetadata
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Unknown
        /// </summary>
        [JsonPropertyName("resourceId")]
        public string ResourceId { get; set; }

        /// <summary>
        /// Friendly name of the environment
        /// </summary>
        [JsonPropertyName("friendlyName")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Dynamics instance name
        /// </summary>
        [JsonPropertyName("uniqueName")]
        public string UniqueName { get; set; }

        /// <summary>
        /// Dynamics instance name
        /// </summary>
        [JsonPropertyName("domainName")]
        public string DomainName { get; set; }

        /// <summary>
        /// Version of Dynamics deployed
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }

        /// <summary>
        /// Full url to the Dynamics instance
        /// </summary>
        [JsonPropertyName("instanceUrl")]
        public string InstanceUrl { get; set; }

        /// <summary>
        /// Full url to the Dynamics APIs of this instance
        /// </summary>
        [JsonPropertyName("instanceApiUrl")]
        public string InstanceApiUrl { get; set; }

        /// <summary>
        /// Language code for the environment, i.e. 1033 for Englsh
        /// </summary>
        [JsonPropertyName("baseLanguage")]
        public int BaseLanguage { get; set; }

        /// <summary>
        /// Indicator if the environment is ready to be used
        /// </summary>
        [JsonPropertyName("instanceState")]
        public string InstanceState { get; set; }

        /// <summary>
        /// Unique identifier of the security group linked to this environment
        /// </summary>
        [JsonPropertyName("securityGroupId")]
        public string SecurityGroupId { get; set; }

        /// <summary>
        /// Date and time at which this instance has been created
        /// </summary>
        [JsonPropertyName("createdTime")]
        public DateTime? CreatedTime { get; set; }
    }
}