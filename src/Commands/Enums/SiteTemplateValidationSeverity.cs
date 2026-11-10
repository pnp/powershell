namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Severity of an issue found while validating a site template.
    /// </summary>
    public enum SiteTemplateValidationSeverity
    {
        /// <summary>
        /// The template is well formed, but depends on something the target site or term store has to provide.
        /// </summary>
        Information = 0,

        /// <summary>
        /// The template can be processed, but may not behave as intended.
        /// </summary>
        Warning = 1,

        /// <summary>
        /// The template is invalid and should not be applied.
        /// </summary>
        Error = 2
    }
}
