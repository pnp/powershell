
using System;
using System.Diagnostics;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Start, "PnPTraceLog")]
    public class StartTraceLog : PSCmdlet
    {
        [Parameter(Mandatory = false, ParameterSetName = "On")]
        public string LogFile;

        [Parameter(Mandatory = false, ParameterSetName = "On")]
        public SwitchParameter WriteToConsole;

        [Parameter(Mandatory = false, ParameterSetName = "On")]
        public PnP.Framework.Diagnostics.LogLevel Level = PnP.Framework.Diagnostics.LogLevel.Information;

        [Parameter(Mandatory = false, ParameterSetName = "On")]
        public string Delimiter;

        [Parameter(Mandatory = false, ParameterSetName = "On")]
        public int IndentSize = 4;

        [Parameter(Mandatory = false, ParameterSetName = "On")]
        public bool AutoFlush = true;

        private const string FileListenername = "PNPPOWERSHELLFILETRACELISTENER";
        private const string ConsoleListenername = "PNPPOWERSHELLCONSOLETRACELISTENER";
        protected override void ProcessRecord()
        {

            // Setup Console Listener if Console switch has been specified or No file LogFile parameter has been set
            if (WriteToConsole.IsPresent || string.IsNullOrEmpty(LogFile))
            {
                RemoveListener(ConsoleListenername);
                ConsoleTraceListener consoleListener = new ConsoleTraceListener(false);
                consoleListener.Name = ConsoleListenername;
                Trace.Listeners.Add(consoleListener);
                PnP.Framework.Diagnostics.Log.LogLevel = Level;
            }

            // Setup File Listener
            if (!string.IsNullOrEmpty(LogFile))
            {
                RemoveListener(FileListenername);

                if (!System.IO.Path.IsPathRooted(LogFile))
                {
                    LogFile = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, LogFile);
                }
                // Create DelimitedListTraceListener in case Delimiter parameter has been specified, if not create TextWritterTraceListener
                TraceListener listener = !string.IsNullOrEmpty(Delimiter) ?
                    new DelimitedListTraceListener(LogFile) { Delimiter = Delimiter, TraceOutputOptions = TraceOptions.DateTime } :
                    new TextWriterTraceListener(LogFile);

                listener.Name = FileListenername;
                Trace.Listeners.Add(listener);
                PnP.Framework.Diagnostics.Log.LogLevel = Level;
            }

            Trace.AutoFlush = AutoFlush;
            Trace.IndentSize = IndentSize;

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