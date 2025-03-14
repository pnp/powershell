
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
        public SwitchParameter WriteToConsole;

        [Parameter(Mandatory = false)]
        public Framework.Diagnostics.LogLevel Level = Framework.Diagnostics.LogLevel.Information;

        [Parameter(Mandatory = false)]
        public bool AutoFlush = true;

        private const string FileListenername = "PNPPOWERSHELLFILETRACELISTENER";
        private const string ConsoleListenername = "PNPPOWERSHELLCONSOLETRACELISTENER";

        protected override void ProcessRecord()
        {
            if (WriteToConsole.ToBool())
            {
                RemoveListener(ConsoleListenername);
                ConsoleTraceListener consoleListener = new(false)
                {
                    Name = ConsoleListenername
                };
                Trace.Listeners.Add(consoleListener);
                Framework.Diagnostics.Log.LogLevel = Level;
            }

            if (!string.IsNullOrEmpty(Path))
            {
                RemoveListener(FileListenername);

                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }
                // Create DelimitedListTraceListener in case Delimiter parameter has been specified, if not create TextWritterTraceListener
                TraceListener listener = new TextWriterTraceListener(Path)
                {
                    Name = FileListenername
                };
                Trace.Listeners.Add(listener);
                Framework.Diagnostics.Log.LogLevel = Level;
            }

            Trace.AutoFlush = AutoFlush;
            Trace.IndentSize = 4;
        }

        /// <summary>
        /// Tries to remove the listener with the given name from the Trace.Listeners collection.
        /// If the listener is not found, it will be ignored.
        /// </summary>
        /// <param name="listenerName">Name of the trace listener</param>
        private static void RemoveListener(string listenerName)
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