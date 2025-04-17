using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommunications.Write, "PnPTraceLog")]
    [OutputType(typeof(void))]
    public class WriteTraceLog : BasePSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Message;

        [Parameter(Mandatory = false)]
        public string Source;

        [Parameter(Mandatory = false)]
        public long? EllapsedMilliseconds;

        [Parameter(Mandatory = false)]
        public override Guid? CorrelationId { get; protected set; } = null;

        [Parameter(Mandatory = false)]
        public Framework.Diagnostics.LogLevel Level = Framework.Diagnostics.LogLevel.Information;

        protected override void ProcessRecord()
        {
            switch (Level)
            {
                case Framework.Diagnostics.LogLevel.Debug:
                    Utilities.Logging.LoggingUtility.Debug(this, Message, Source, CorrelationId, EllapsedMilliseconds);
                    break;
                case Framework.Diagnostics.LogLevel.Warning:
                    Utilities.Logging.LoggingUtility.Warning(this, Message, Source, CorrelationId, EllapsedMilliseconds);
                    break;
                case Framework.Diagnostics.LogLevel.Information:
                    Utilities.Logging.LoggingUtility.Info(this, Message, Source, CorrelationId, EllapsedMilliseconds);
                    break;
                case Framework.Diagnostics.LogLevel.Error:
                    Utilities.Logging.LoggingUtility.Error(this, Message, Source, CorrelationId, EllapsedMilliseconds);
                    break;
            }
        }
    }
}