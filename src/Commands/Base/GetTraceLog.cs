using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities.Logging;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPTraceLog", DefaultParameterSetName = ParameterSet_LOGFROMLOGSTREAM)]
    [OutputType(typeof(IEnumerable<TraceLogEntry>))]
    public class GetTraceLog : BasePSCmdlet
    {
        private const string ParameterSet_LOGFROMFILE = "Log from file";
        private const string ParameterSet_LOGFROMLOGSTREAM = "Log from log stream";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_LOGFROMFILE)]
        public string Path;

        protected override void ProcessRecord()
        {
            switch (ParameterSetName)
            {
                case ParameterSet_LOGFROMLOGSTREAM:
                    ProcessLogFromLogStream();
                    break;
                case ParameterSet_LOGFROMFILE:
                    ProcessLogFromFile();
                    break;
            }
        }

        private void ProcessLogFromLogStream()
        {
            WriteVerbose("Retrieving log entries from log stream");
            var logStreamListener = Trace.Listeners[LogStreamListener.DefaultListenerName] as LogStreamListener ?? throw new PSArgumentException($"Log stream listener {LogStreamListener.DefaultListenerName} not found");

            foreach (var entry in logStreamListener.Entries)
            {
                WriteObject(entry, true);
            }
        }

        private void ProcessLogFromFile()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }
            if (File.Exists(Path))
            {
                WriteVerbose($"Retrieving log entries from file {Path}");
                var lines = File.ReadAllLines(Path);
                foreach (var line in lines)
                {
                    var items = line.Split('\t');
                    WriteObject(new TraceLogEntry(items), true);
                }
            }
            else
            {
                throw new PSArgumentException($"File {Path} does not exist");
            }
        }
    }
}