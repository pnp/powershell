
using System;
using System.Diagnostics;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Start, "PnPTraceLog")]
    public class StartTraceLog : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Path;

        [Parameter(Mandatory = false)]
        public PnP.Framework.Diagnostics.LogLevel Level = PnP.Framework.Diagnostics.LogLevel.Information;

        [Parameter(Mandatory = false)]
        public bool AutoFlush = true;

        private const string FileListenername = "PNPPOWERSHELLFILETRACELISTENER";
        private const string ConsoleListenername = "PNPPOWERSHELLCONSOLETRACELISTENER";
        protected override void ProcessRecord()
        {

            // Setup Console Listener if Console switch has been specified or No file LogFile parameter has been set
            if (string.IsNullOrEmpty(Path))
            {
                RemoveListener(ConsoleListenername);
                ConsoleTraceListener consoleListener = new ConsoleTraceListener(false);
                consoleListener.Name = ConsoleListenername;
                Trace.Listeners.Add(consoleListener);
                PnP.Framework.Diagnostics.Log.LogLevel = Level;
            }

            // Setup File Listener
            if (!string.IsNullOrEmpty(Path))
            {
                RemoveListener(FileListenername);

                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }
                // Create DelimitedListTraceListener in case Delimiter parameter has been specified, if not create TextWritterTraceListener
                TraceListener listener = new TextWriterTraceListener(Path);

                listener.Name = FileListenername;
                Trace.Listeners.Add(listener);
                PnP.Framework.Diagnostics.Log.LogLevel = Level;
            }

            Trace.AutoFlush = AutoFlush;
            Trace.IndentSize = 4;

        }

        private void RemoveListener(string listenerName)
        {
            try
            {
                var existingListener = Trace.Listeners[listenerName];
                if (existingListener != null)
                {
                    existingListener.Flush();
                    existingListener.Close();
                    Trace.Listeners.Remove(existingListener);
                }
            }
            catch (Exception)
            {
                // ignored
            }

        }
    }
}