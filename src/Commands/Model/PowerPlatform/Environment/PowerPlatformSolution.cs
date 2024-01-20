using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.Environment.Solution
{
    public class PowerPlatformSolution
    {
        /// <summary>
        /// Etag for the solution
        /// </summary>
        [JsonPropertyName("@odata.etag")]
        public string? ODataEtag { get; set; }

        /// <summary>
        /// Date and time when the solution was installed
        /// </summary>
        [JsonPropertyName("installedon")]
        public string? InstalledOn { get; set; }

        /// <summary>
        /// Version of the solution package
        /// </summary>
        [JsonPropertyName("solutionpackageversion")]
        public string? SolutionPackageVersion { get; set; }

        /// <summary>
        /// Configuration page ID (null in this case)
        /// </summary>
        [JsonPropertyName("_configurationpageid_value")]
        public string? ConfigurationPageId { get; set; }

        /// <summary>
        /// ID of the solution
        /// </summary>
        [JsonPropertyName("solutionid")]
        public string? SolutionId { get; set; }

        /// <summary>
        /// Date and time when the solution was last modified
        /// </summary>
        [JsonPropertyName("modifiedon")]
        public string? ModifiedOn { get; set; }

        /// <summary>
        /// Unique name of the solution
        /// </summary>
        [JsonPropertyName("uniquename")]
        public string? UniqueName { get; set; }

        /// <summary>
        /// Indicates if the solution is managed by API
        /// </summary>
        [JsonPropertyName("isapimanaged")]
        public bool IsApiManaged { get; set; }

        /// <summary>
        /// Publisher ID (null in this case)
        /// </summary>
        [JsonPropertyName("_publisherid_value")]
        public string? PublisherIdValue { get; set; }

        /// <summary>
        /// Indicates if the solution is managed
        /// </summary>
        [JsonPropertyName("ismanaged")]
        public bool IsManaged { get; set; }

        /// <summary>
        /// Indicates if the solution is visible
        /// </summary>
        [JsonPropertyName("isvisible")]
        public bool IsVisible { get; set; }

        /// <summary>
        /// Thumbprint (null in this case)
        /// </summary>
        [JsonPropertyName("thumbprint")]
        public string? Thumbprint { get; set; }

        /// <summary>
        /// Pinpoint publisher ID (null in this case)
        /// </summary>
        [JsonPropertyName("pinpointpublisherid")]
        public string? PinpointPublisherId { get; set; }

        /// <summary>
        /// Version of the solution
        /// </summary>
        [JsonPropertyName("version")]
        public string? Version { get; set; }

        /// <summary>
        /// Modified on behalf by value (null in this case)
        /// </summary>
        [JsonPropertyName("_modifiedonbehalfby_value")]
        public string? ModifiedOnBehalfByValue { get; set; }

        /// <summary>
        /// Parent solution ID value (null in this case)
        /// </summary>
        [JsonPropertyName("_parentsolutionid_value")]
        public string? ParentSolutionIdValue { get; set; }

        /// <summary>
        /// Pinpoint asset ID (null in this case)
        /// </summary>
        [JsonPropertyName("pinpointassetid")]
        public string? PinpointAssetId { get; set; }

        /// <summary>
        /// Pinpoint solution ID (null in this case)
        /// </summary>
        [JsonPropertyName("pinpointsolutionid")]
        public string? PinpointSolutionId { get; set; }

        /// <summary>
        /// Friendly name of the solution
        /// </summary>
        [JsonPropertyName("friendlyname")]
        public string? FriendlyName { get; set; }

        /// <summary>
        /// Organization ID value
        /// </summary>
        [JsonPropertyName("_organizationid_value")]
        public string? OrganizationIdValue { get; set; }

        /// <summary>
        /// Version number
        /// </summary>
        [JsonPropertyName("versionnumber")]
        public int? VersionNumber { get; set; }

        /// <summary>
        /// Template suffix (null in this case)
        /// </summary>
        [JsonPropertyName("templatesuffix")]
        public string? TemplateSuffix { get; set; }

        /// <summary>
        /// Upgrade information (null in this case)
        /// </summary>
        [JsonPropertyName("upgradeinfo")]
        public string? UpgradeInfo { get; set; }

        /// <summary>
        /// Created on behalf by value (null in this case)
        /// </summary>
        [JsonPropertyName("_createdonbehalfby_value")]
        public string? CreatedOnBehalfByValue { get; set; }

        /// <summary>
        /// Modified by value
        /// </summary>
        [JsonPropertyName("_modifiedby_value")]
        public string? ModifiedByValue { get; set; }

        /// <summary>
        /// Date and time when the solution was created
        /// </summary>
        [JsonPropertyName("createdon")]
        public string? CreatedOn { get; set; }

        /// <summary>
        /// Date and time when the solution was last updated (null in this case)
        /// </summary>
        [JsonPropertyName("updatedon")]
        public string? UpdatedOn { get; set; }

        /// <summary>
        /// Description of the solution (null in this case)
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Solution type (null in this case)
        /// </summary>
        [JsonPropertyName("solutiontype")]
        public int? SolutionType { get; set; }

        /// <summary>
        /// Pinpoint solution default locale (null in this case)
        /// </summary>
        [JsonPropertyName("pinpointsolutiondefaultlocale")]
        public string? PinpointSolutionDefaultLocale { get; set; }

        /// <summary>
        /// Created by value
        /// </summary>
        [JsonPropertyName("_createdby_value")]
        public string? CreatedByValue { get; set; }

        /// <summary>
        /// Publisher information
        /// </summary>
        [JsonPropertyName("publisherid")]
        public PowerPlatformSolutionPublisher PublisherId { get; set; }
    }
}
