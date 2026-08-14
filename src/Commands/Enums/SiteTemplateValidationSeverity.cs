namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Severity of an issue found while validating a site template.
    /// </summary>
    public enum SiteTemplateValidationSeverity
    {
        /// <summary>
        /// The template can be processed, but may not behave as intended.
        /// </summary>
        Warning,

        /// <summary>
        /// The template is invalid and should not be applied.
        /// </summary>
        Error
    }
}