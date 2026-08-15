using System.Collections.Generic;
using System.Linq;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Result of validating one PnP site template.
    /// </summary>
    public sealed class SiteTemplateValidationResult
    {
        /// <summary>
        /// Identifier of the validated template.
        /// </summary>
        public string TemplateId { get; init; }

        /// <summary>
        /// Provisioning schema namespace found in the source artifact, when available.
        /// </summary>
        public string SchemaVersion { get; init; }

        /// <summary>
        /// Indicates whether the template had a file connector, so referenced resources could be checked.
        /// </summary>
        public bool ResourcesChecked { get; init; }

        /// <summary>
        /// Indicates whether the source XML was available, so the schema version and deprecated element checks could run.
        /// </summary>
        public bool SchemaChecked { get; init; }

        /// <summary>
        /// Issues found while validating the template.
        /// </summary>
        public IReadOnlyList<SiteTemplateValidationIssue> Issues { get; init; } = [];

        /// <summary>
        /// Indicates whether no error-severity issues were found.
        /// </summary>
        public bool IsValid => Issues.All(issue => issue.Severity != SiteTemplateValidationSeverity.Error);
    }
}