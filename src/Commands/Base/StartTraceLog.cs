using System.Diagnostics;
using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities.Logging;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Start, "PnPTraceLog")]
    public class StartTraceLog : BasePSCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Path;

        [Parameter(Mandatory = false)]
        public SwitchParameter WriteToConsole;

        [Parameter(Mandatory = false)]
        public SwitchParameter WriteToLogStream;

        [Parameter(Mandatory = false)]
        public Framework.Diagnostics.LogLevel Level = Framework.Diagnostics.LogLevel.Information;

        [Parameter(Mandatory = false)]
        public bool AutoFlush = true;

        private const string FileListenername = "PNPPOWERSHELLFILETRACELISTENER";
        private const string ConsoleListenername = "PNPPOWERSHELLCONSOLETRACELISTENER";

        protected override void ProcessRecord()
        {
            Framework.Diagnostics.Log.LogLevel = Level;

            if (WriteToConsole.ToBool())
            {
                LogDebug($"Adding console listener named {ConsoleListenername}");

                LoggingUtility.RemoveListener(ConsoleListenername);
                ConsoleTraceListener consoleListener = new(false)
                {
                    Name = ConsoleListenername
                };
                Trace.Listeners.Add(consoleListener);
            }

            if (WriteToLogStream.ToBool())
            {
                LogDebug($"Adding log stream listener named {LogStreamListener.DefaultListenerName}");

                LoggingUtility.RemoveListener(LogStreamListener.DefaultListenerName);
                LogStreamListener logStreamListener = new()
                {
                    Name = LogStreamListener.DefaultListenerName
                };
                Trace.Listeners.Add(logStreamListener);
            }

            if (!string.IsNullOrEmpty(Path))
            {
                LoggingUtility.RemoveListener(FileListenername);

                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }

                LogDebug($"Adding file listener named {FileListenername} to {Path}");

                TraceListener listener = new TextWriterTraceListener(Path)
                {
                    Name = FileListenername
                };
                Trace.Listeners.Add(listener);
            }

            Trace.AutoFlush = AutoFlush;
            Trace.IndentSize = 4;
        }
    }
}