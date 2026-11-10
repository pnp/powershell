using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Describes one issue found while validating a site template.
    /// </summary>
    public sealed class SiteTemplateValidationIssue
    {
        /// <summary>
        /// Stable identifier for the validation rule.
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Severity of the issue.
        /// </summary>
        public SiteTemplateValidationSeverity Severity { get; init; }

        /// <summary>
        /// Description of the issue.
        /// </summary>
        public string Message { get; init; }

        /// <summary>
        /// Location of the issue in the template.
        /// </summary>
        public string Location { get; init; }
    }
}