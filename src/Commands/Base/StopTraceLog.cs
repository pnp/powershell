
using System.Diagnostics;
using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities.Logging;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Stop, "PnPTraceLog")]
    public class StopTraceLog : BasePSCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter StopFileLogging = true;

        [Parameter(Mandatory = false)]
        public SwitchParameter StopConsoleLogging = true;

        [Parameter(Mandatory = false)]
        public SwitchParameter StopLogStreamLogging = true;

        private const string FileListenername = "PNPPOWERSHELLFILETRACELISTENER";
        private const string ConsoleListenername = "PNPPOWERSHELLCONSOLETRACELISTENER";

        protected override void ProcessRecord()
        {
            LogDebug("Flushing log entries");
            Trace.Flush();

            if (StopConsoleLogging.ToBool())
            {
                LogDebug("Stopping logging to console");
                LoggingUtility.RemoveListener(ConsoleListenername);
            }
            if (StopFileLogging.ToBool())
            {
                LogDebug("Stopping logging to file");
                LoggingUtility.RemoveListener(FileListenername);
            }
            if(StopLogStreamLogging.ToBool())
            {
                LogDebug("Stopping logging to in memory log stream");
                LoggingUtility.RemoveListener(LogStreamListener.DefaultListenerName);
            }
        }
    }
}