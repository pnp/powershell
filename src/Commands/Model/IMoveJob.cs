using System;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Interface for move job operations
    /// </summary>
    public interface IMoveJob
    {
        Guid JobId { get; set; }
        string Status { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? CompletedDate { get; set; }
        DateTime? LastModified { get; set; }
        string ErrorMessage { get; set; }
        double? ProgressPercentage { get; set; }
    }
}