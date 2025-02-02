using System;

namespace PnP.PowerShell.Commands.Model.EnterpriseAppInsights
{
    /// <summary>
    /// Model containing the status of an Enterprise App Insights report
    /// </summary>
    public class ReportMetadata
    {
        /// <summary>
        /// Unique identifier of the report
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// The date and time at which the report has been created
        /// </summary>
        public DateTime? CreatedDateTime { get; set; }

        /// <summary>
        /// The amount of days covered in the report
        /// </summary>
        public short? ReportPeriodInDays { get; set; }

        /// <summary>
        /// The status of the report
        /// </summary>
        public ReportStatus? ReportStatus { get; set; }
    }
}