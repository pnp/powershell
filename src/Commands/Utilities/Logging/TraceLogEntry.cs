using System;

namespace PnP.PowerShell.Commands.Utilities.Logging
{
    /// <summary>
    /// Represents a single entry in the trace log
    /// </summary>
     public class TraceLogEntry(string[] values)
    {
        /// <summary>
        /// The time stamp of the log entry
        /// </summary>
        public DateTime? TimeStamp = DateTime.TryParse(values[0], out var timeStamp) ? timeStamp : null;

        /// <summary>
        /// The category of the log entry
        /// </summary>
        public string Source = values[1].Trim('[', ']');

        /// <summary>
        /// Id of the thread on which the log entry was created
        /// </summary>
        public int? ThreadId { get; set; } = int.TryParse(values[2].Trim('[', ']'), out var threadId) ? threadId : null;

        /// <summary>
        /// The level of the log entry
        /// </summary>
        public Framework.Diagnostics.LogLevel? Level = Enum.TryParse<Framework.Diagnostics.LogLevel>(values[3].Trim('[', ']'), out var logLevel) ? logLevel : null;

        /// <summary>
        /// The logged message
        /// </summary>
        public string Message = values[4];

        /// <summary>
        /// The elapsed Log time in MilliSeconds sicne the previous entry
        /// </summary>
        public long? EllapsedMilliseconds { get; set; } = long.TryParse(values[5][..^2], out var ellapsedMilliseconds) ? ellapsedMilliseconds : null;

        /// <summary>
        /// The CorrelationId representing the log entry
        /// </summary>
        public Guid? CorrelationId { get; set; } = Guid.TryParse(values[6], out var correlationId) ? correlationId : null;
    }
}