using System.Diagnostics;
using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities.Logging;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Clear, "PnPTraceLog")]
    [OutputType(typeof(void))]
    public class ClearTraceLog : BasePSCmdlet
    {
        protected override void ProcessRecord()
        {
            if (Trace.Listeners[LogStreamListener.DefaultListenerName] is not LogStreamListener logStreamListener)
            {
                LogWarning($"Log stream listener named {LogStreamListener.DefaultListenerName} not found. No entries cleared.");
            }
            else
            {
                LogDebug($"Clearing {(logStreamListener.Entries.Count != 1 ? $"{logStreamListener.Entries.Count} log entries" : "one log entry")} from log stream listener named {LogStreamListener.DefaultListenerName}");
                logStreamListener.Entries.Clear();
            }
        }
    }
}