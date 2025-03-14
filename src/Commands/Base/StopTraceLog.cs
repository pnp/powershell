
using System.Diagnostics;
using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities.Logging;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Stop, "PnPTraceLog")]
    public class StopTraceLog : BasePSCmdlet
    {
        private const string FileListenername = "PNPPOWERSHELLFILETRACELISTENER";
        private const string ConsoleListenername = "PNPPOWERSHELLCONSOLETRACELISTENER";

        protected override void ProcessRecord()
        {
            LogDebug("Flushing log entries");
            Trace.Flush();

            LogDebug("Removing log listeners");
            LoggingUtility.RemoveListener(ConsoleListenername);
            LoggingUtility.RemoveListener(FileListenername);
            LoggingUtility.RemoveListener(LogStreamListener.DefaultListenerName);
        }
    }
}